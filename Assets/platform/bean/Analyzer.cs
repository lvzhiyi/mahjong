using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public abstract class Analyzer:BytesSerializable
    {
        // 特殊牌型具体规则对应分析器子类具体定义
        /** 牌型：不成牌型 */
        public const int MALFORMED = 0;
        /** 牌型：普通牌型 */
        public const int NORMAL = 1;
        /** 牌型：齐对牌型 */
        public const int NEAT_PAIR = 2;

        /** 规则 */
        public Rule rule;
        /** 手牌数：不包含摸牌区 */
        protected int handCardSize;
        /** 手牌区各牌数量 int[type][index] */
        protected int[][] counts;
        /** 固定区各牌数量 int[type][index] */
        protected int[][] fixcounts;

        protected ArrayList<int> listens;

        private int paidui;
        /**  */
        protected Analyzer(Rule rule,int paidui)
        {
            this.rule = rule;
            this.listens = new ArrayList<int>();
            this.paidui = paidui;
        }

        public void init(ArrayList<int> hand, ArrayList<FixedCards> fix)
        {
            counts = new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            int card = -1;
            for (int i = 0; i < hand.count; i++)
            {
                card = hand.get(i);
                if (MJCard.isSigned(card, MJCard.SIGN_NO_KONG))
                    card = MJCard.cancelSign(card,MJCard.SIGN_NO_KONG);
                counts[MJCard.getType(card)][MJCard.getIndex(card)]++;
            }

            fixcounts = new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            FixedCards fixcard = null;
            int fixc = 0;
            for (int i = 0; i < fix.count; i++)
            {
                fixcard = fix.get(i);
                for (int j = 0; j < fixcard.cards.Length; j++)
                {
                    fixc = fixcard.cards[j];
                    if (MJCard.isSigned(fixc, MJCard.SIGN_HU))
                        fixc = MJCard.cancelSign(fixc, MJCard.SIGN_HU);
                    fixcounts[MJCard.getType(fixcard.cards[j])][MJCard.getIndex(fixc)]++;
                }
            }
        }

        /** 设置手牌数量（不包含摸牌区） */
        public  void setHandCardSize(int size)
        {
            this.handCardSize = size;
        }

        public void delHandCard(int card, int count)
        {
            counts[MJCard.getType(card)][MJCard.getIndex(card)] -= count;
        }

        public void addHandCard(int card, int count)
        {
            counts[MJCard.getType(card)][MJCard.getIndex(card)] += count;
        }

        /** 是否是海底 */
        public  bool isHaidi()
        {
            return paidui==0;
        }

        /** 获取带根的数量：这里返回的已经计算杠牌了 */
        public virtual  int getRootCount()
        {
            int count = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (counts[i][j] == 4 || fixcounts[i][j] == 4 || (counts[i][j] + fixcounts[i][j]) == 4) count++;
                }
            }
            return count;
        }

        /** 获取手牌区，和固定牌区所包含得牌类型位值（只计算筒条万） */
        public  int getTypeBits()
        {
            int bits = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (counts[i][j] == 0 && fixcounts[i][j] == 0) continue;
                    // 只要手牌区和固定牌区包含这个类型得牌，就增加该类型位值
                    bits |= MJCard.BITS[i];
                    break; // 后面就不用检查了
                }
            }
            return bits;
        }

        /** 加番判断：是否是清一色 */
        public virtual bool isQing(int bits)
        {
            return bits == MJCard.DOT || bits == MJCard.BAM || bits == MJCard.CHA;
        }

        /** 加番判断：是否是金钩钩 */
        public virtual bool isSingle()
        {
            return  handCardSize == 1;
        }

        /** 加番判断：是否是混一色（清一色算特殊的混一色） */
        public  bool isHunQing(int bits)
        {
            bits = bits & (~(MJCard.WIND | MJCard.DRAGON | MJCard.FLOWER));// 过滤风箭花后
            return bits == MJCard.DOT || bits == MJCard.BAM || bits == MJCard.CHA;// 只有一种序数牌
        }

        /** 检查是否符合胡牌前提条件：默认无条件 */
        protected virtual bool checkHuPremise(int bits)
        {
            return true;
        }

        /** 检查是否是特殊胡牌牌型：不同规则特殊胡牌牌型 */
        protected virtual bool isSpecial(int huCard,ArrayList<FixedCards> fixeds)
        {
            return false;
        }

        /** 是否是齐对（如：暗七对） */
        protected virtual bool isNeatPair(int bits)
        {
            return Eliminator.checkHuNeatPairs(counts, fixcounts,bits);
        }

        /** 检查是否是常规胡牌牌型：不同规则标准胡牌牌型 */
        protected virtual bool isCommon(int bits)
        {
            bool needPair = true;

            for (int i = 0, r; i < counts.Length; i++)
            {
                int type = rule.getCountsTypeFromIndex(i);
                if (type == MJCard.TYPE_FLOWER|| !StatusKit.hasStatus(bits, MJCard.BITS[type]) ) continue;
                if (type == MJCard.TYPE_WIND || type == MJCard.TYPE_DRAGON)
                    r = Eliminator.eliminateWD(counts[i], 0, needPair);
                else
                {
                    r = Eliminator.eliminate(counts[i], 0, needPair);
                }
                    
                if (r == Eliminator.FAIL) return false;
                needPair = (r == Eliminator.WITHOUT_PAIR);
            }
            return true;
        }

        ///** 验证是否可以胡牌 */
        //public  bool canHu(ArrayList<FixedCards> fixeds)
        //{
        //    int bits = getTypeBits();
        //    if (!checkHuPremise(bits)) return false;
        //    return isNeatPair(bits) || isCommon(bits) || isSpecial(fixeds);
        //}

        /** 检出所有可听的牌 */
        public virtual int[] checkOutListens(ArrayList<FixedCards> fixeds)
        {
            int bits = getTypeBits();
            if (!checkHuPremise(bits)) return Single.EMPTY_INT_ARRAY;
            listens.clear();
            for (int i = 0; i < counts.Length; ++i)
            {
                int type = rule.getCountsTypeFromIndex(i);
                if ((bits & MJCard.BITS[type]) == 0) continue;// 不包含当前类型得牌
                int[] arr = counts[i];
                for (int j = 0, len = arr.Length; j < len; ++j)
                {
                    if (j == 0)
                    {
                        if (arr[j] == 0 && arr[j + 1] == 0) continue;
                    }
                    else if (j == len - 1)
                    {
                        if (arr[j] == 0 && arr[j - 1] == 0) continue;
                    }
                    else
                    {
                        if (arr[j] == 0 && arr[j + 1] == 0 && arr[j - 1] == 0) continue;
                    }
                    ++arr[j];// 尝试添加一张牌
                    int card=MJCard.getCard(i, j);
                    if (isSpecial(card,fixeds) || isNeatPair(bits) || isCommon(bits))
                        listens.add(card);
                    --arr[j];// 判定完成后移除这张牌
                }
            }
            if (listens.count > 0) return listens.toArray();
            return Single.EMPTY_INT_ARRAY;
        }
    }
}
