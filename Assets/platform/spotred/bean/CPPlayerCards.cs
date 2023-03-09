using cambrian;
using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 单个玩家的牌
    /// </summary>
    public class CPPlayerCards : PlayerCards
    {
        /// <summary>
        /// 状态：报牌（只有巴中打大在用）
        /// </summary>
        public const int STATUS_BAOPAI = 1 << 0;
        /// <summary>
        /// 状态：飘
        /// </summary>
        public const int STATUS_PIAO = 1 << 1;
        /// <summary>
        /// 状态：已胡牌
        /// </summary>
        public const int STATUS_HU = 1 << 2;

        /// <summary>
        /// 报的牌，就是报牌后能够打的牌
        /// </summary>
        private int[] canplayCards;
        /// <summary>
        /// 不可出的牌，目前广元市区，针对于破对
        /// </summary>
        private ArrayList<int> noCanPlayCard;
        /// <summary>
        /// 可以出的牌，目前广元市区，针对于破对
        /// </summary>
        private ArrayList<int> canpdPlayCard;

        public CPPlayerCards():base()
        {
            this.noCanPlayCard=new ArrayList<int>();
            this.canpdPlayCard = new ArrayList<int>();
        }

        /// <summary>
        /// 增加不可出的牌
        /// </summary>
        public void addNoCanPlayCard(int card)
        {
            this.noCanPlayCard.add(card);
        }

        public void addNoCanPlayCards(int[] cards)
        {
            this.noCanPlayCard.add(cards);
        }

        /// <summary>
        /// 获取不能打的牌
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getNoCanPlayCards()
        {
            return this.noCanPlayCard;
        }

        /// <summary>
        /// 增加不可出的牌
        /// </summary>
        public void addCanpdPlayCard(int card)
        {
            this.canpdPlayCard.add(card);
        }

        public void addCanpdPlayCards(int[] cards)
        {
            this.canpdPlayCard.add(cards);
        }

        /// <summary>
        /// 获取可以破对的牌
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getCanpdPlayCard()
        {
            return this.canpdPlayCard;
        }

        /// <summary>
        /// 设置可以报的牌
        /// </summary>
        /// <param name="canplaycards"></param>
        public void setCanplayCards(int[] canplaycards)
        {
            this.canplayCards = canplaycards;
        }

        /// <summary>
        /// 获取可以报的牌
        /// </summary>
        /// <returns></returns>
        public int[] getCanPlayCards()
        {
            return this.canplayCards;
        }

        /// <summary>
        /// 获取可以巴的手牌
        /// </summary>
        /// <returns></returns>
        public int[] getCanKongCards()
        {
            ArrayList<int> cards=new ArrayList<int>();

            for (int i = 0; i < handcards.count; i++)
            {
                int card= handcards.get(i);
                for (int j = 0; j < fixCards.count; j++)
                {
                    FixedCards fixeds= fixCards.get(j);

                    if (fixeds.isSameCard(card, 3))
                    {
                        cards.add(card);
                        break;
                    }
                }
            }

            return cards.toArray();
        }
        /// <summary>
        /// 获取非小家最后一组固定牌的值
        /// </summary>
        /// <returns></returns>
        public int getCanKongCard()
        {
            return fixCards.getLast().cards[0];
        }

        /// <summary>
        /// 获取可以划牌和数量{{card,count}}
        /// </summary>
        /// <returns></returns>
        public int[][] getSlipCards()
        {
            int[][] counts = Card.getEmptyArray();
            for (int i = 0, card = 0; i < handcards.count; i++)
            {
                card = handcards.get(i);
                counts[Card.getType(card)][Card.getIndex(card)]++;
            }
            ArrayList<int[]> list=new ArrayList<int[]>();

            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == null) continue;
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (counts[i][j] > 2)
                    {
                        if(Room.room.roomRule.rule.sid==1017)// 金堂考考只能偷3张
                            list.add(new int[] { Card.CARDS[i][j], 3 });
                        else
                            list.add(new int[] { Card.CARDS[i][j], counts[i][j] });
                    }
                }
            }
            return list.toArray();
        }


        /// <summary>
        /// 获取固定牌区的福数
        /// </summary>
        /// <param name="isXiaojia"></param>
        /// <returns></returns>
        public int getFixedCardsFushu(bool isXiaojia,int rulesid)
        {
            return getFixedCardsFushu(isXiaojia);
        }

        private int getFixedCardRedCount()
        {
            int fushu = 0;
            int fs = 0;
            int card = 0;
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                {
                    card = fix.cards[0];
                    if (card==Card.INVISIBLE)
                        continue;
                    fs = Card.getCardRedCount(card) * 4;
                    fushu += (fs < 10 ? 10 : fs);
                }
                else if (fix.type == FixedCards.PONG)
                {
                    card = fix.cards[0];
                    if (card == Card.INVISIBLE)
                        continue;
                    fs = Card.getCardRedCount(card) * 3;
                    fushu += (fs < 10 ? 10 : fs);
                }
                else if (fix.type == FixedCards.SINGLE)
                {
                    card = fix.cards[0];
                    if (card == Card.INVISIBLE)
                        continue;
                    fushu += Card.getCardRedCount(card);
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        card = fix.cards[j];
                        if (card == Card.INVISIBLE)
                            continue;
                        fushu += Card.getCardRedCount(card);
                    }
                }
            }
            return fushu;
        }

        private int getFixedCardsFushu(bool isXiaojia)
        {
            int[][] counts = Card.getEmptyArray();
            int fushu = Card.getFushu(counts, isXiaojia);
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                    fushu += fix.isAllRed() ? 10 : 6;
                else if (fix.type == FixedCards.PONG)
                    fushu += fix.isAllRed() ? 8 : 4;
                else if (fix.type == FixedCards.SINGLE)
                {
                    fushu += 2;
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        if (Card.isRed(fix.cards[j])) fushu += 1;
                    }

                }
            }
            return fushu;
        }

        /// <summary>
        /// 获取福数
        /// </summary>
        /// <param name="isXiaojia"></param>
        /// <param name="rulesid"></param>
        /// <param name="is5read5black">是否是5红5黑</param>
        /// <returns></returns>
        public int getFushu(bool isXiaojia, int rulesid,bool is5black)
        {
            return getNormalFuShu(isXiaojia, is5black);
        }

        /// <summary>
        /// 获取自己的总福数
        /// </summary>
        /// <param name="isXiaojia"></param>
        /// <returns></returns>
        private int getNormalFuShu(bool isXiaojia,bool is5black)
        {
            int fixfushu = 0;
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                    fixfushu += fix.isAllRed() ? 10 : 6;
                else if (fix.type == FixedCards.PONG)
                    fixfushu += fix.isAllRed() ? 8 : 4;
                else if (fix.type == FixedCards.SINGLE)
                {
                    fixfushu += 2;
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        if (Card.isRed(fix.cards[j])) fixfushu += 1;
                    }
                }
            }

            int[][] counts = Card.getEmptyArray();
            int card = 0;
            
            for (int i = 0; i < handcards.count; i++)
            {
                card = handcards.get(i);
                counts[Card.getType(card)][Card.getIndex(card)]++;
            }
            
            int dingCount = counts[2][0];
            int futouCount = counts[10][0];
            counts[2][0] = 0;
            counts[10][0] = 0;

            int fushu = Card.getFushu(counts,isXiaojia);

            counts[2][0] = dingCount;
            counts[10][0] = futouCount;

            if(isXiaojia)
            {
                int limit = is5black ? 1 : 0;
                if (fixfushu > 0)
                    return fushu + fixfushu + dingCount;
                else
                {
                    if (fushu > limit || dingCount > 1) // 两个以上丁丁，必定只能走红路
                        return fushu + dingCount + (futouCount > 0 ? 1 : 0);
                    else
                        return fushu;
                }
            }
            else
                return fushu+ fixfushu+dingCount;
        }

        /// <summary>
        /// 获取牌的红点点福数
        /// </summary>
        /// <returns></returns>
        private int getCardRedCount()
        {
            int fushu = 0;
            int card = 0;
            for (int i = 0; i < handcards.count; i++)
            {
                card = handcards.get(i);
                fushu += Card.getCardRedCount(card);
            }

            int fs = 0;
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                {
                    fs = Card.getCardRedCount(fix.cards[0]) * 4;
                    fushu += (fs<10?10:fs);
                }
                else if (fix.type == FixedCards.PONG)
                {
                    fs = Card.getCardRedCount(fix.cards[0]) * 3;
                    fushu += (fs < 10 ? 10 : fs);
                }
                else if (fix.type == FixedCards.SINGLE)
                {
                    fushu += Card.getCardRedCount(fix.cards[0]);
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        fushu += Card.getCardRedCount(fix.cards[j]);
                    }
                }
            }
            return fushu;
        }

        /// <summary>
        /// 获取巴中打大固定牌区的福数
        /// </summary>
        /// <param name="isXiaojia"></param>
        /// <returns></returns>
        public int getFixedCardsFuShuDaDa(bool isXiaojia)
        {
            int[][] counts = Card.getEmptyArray();
            
            int fushu = Card.getFushu(counts, isXiaojia);
            for (int i = 0; i < fixCards.count; i++)
            {

                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                {
                    if (fix.cards[0] == 0x1014 || fix.cards[0] == 0x1011 || fix.cards[0] == 0x1244)
                        fushu += 24;
                    else
                        fushu += fix.isAllRed() ? 12 : 8;
                }
                else if (fix.type == FixedCards.PONG)
                {
                    if (fix.cards[0] == 0x1014 || fix.cards[0] == 0x1011 || fix.cards[0] == 0x1244)
                        fushu += 16;
                    else
                        fushu += fix.isAllRed() ? 8 : 4;
                }
                else if (fix.type == FixedCards.SINGLE)
                {
                    fushu += 2;
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        if (Card.isRed(fix.cards[j])) fushu += 1;
                    }
                }
            }
            return fushu;
        }

        /// <summary>
        /// 获取巴中打大的福数
        /// </summary>
        /// <param name="isXiaojia"></param>
        /// <returns></returns>
        public int getFuShuDaDa(bool isXiaojia)
        {
            int[][] counts = Card.getEmptyArray();
            int card = 0;
            for (int i = 0; i < handcards.count; i++)
            {
                card = handcards.get(i);
                counts[Card.getType(card)][Card.getIndex(card)]++;
            }
            int fushu = Card.getFushu(counts, isXiaojia);
            for (int i = 0; i < fixCards.count; i++)
            {

                FixedCards fix = fixCards.get(i);
                if (fix.type == FixedCards.KONG)
                {
                    if (fix.cards[0] == 0x1014 || fix.cards[0] == 0x1011 || fix.cards[0] == 0x1244)
                        fushu += 24;
                    else
                        fushu += fix.isAllRed() ? 12 : 8;
                }
                else if (fix.type == FixedCards.PONG)
                {
                    if (fix.cards[0] == 0x1014 || fix.cards[0] == 0x1011 || fix.cards[0] == 0x1244)
                        fushu += 16;
                    else
                        fushu += fix.isAllRed() ? 8 : 4;
                }
                else if (fix.type == FixedCards.SINGLE)
                {
                    fushu += 2;
                }
                else
                {
                    for (int j = 0; j < fix.cards.Length; j++)
                    {
                        if (Card.isRed(fix.cards[j])) fushu += 1;
                    }
                }
            }
            return fushu;
        }

        /// <summary>
        /// 获取能够吃的牌(对7不能拆开来吃,小家不能吃牌)
        /// </summary>
        /// <param name="card"></param>
        /// <param name="canChowDouble7">能不能拆对7吃</param>
        /// <returns></returns>
        public ArrayList<int> getCanChowCard(int card,int rulesid,bool canChowDouble7)
        {
            ArrayList<int> list = new ArrayList<int>();
            int[] canChow=Card.getChowCards(card);

          
            
                if (Card.getValue(card) == 7)
                {
                    for (int i = 0; i < canChow.Length; i++)
                    {
                        int count = getCardCount(canChow[i]);
                        if (count == 0 || (count > 1 && !canChowDouble7)) continue;// 对7点不能拆开吃或数量为0 
                        list.add(canChow[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < canChow.Length; i++)
                    {
                        int count = getCardCount(canChow[i]);
                        if (count > 0) list.add(canChow[i]);
                    }
                }
            

            if (rulesid == RuleSid.AN_YUE_CP && Room.room.getRule().getRuleAtribute("is25single"))
                list.remove(Card.C25); 
            return list;
        }

        /// <summary>
        /// 获取不能吃的牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public ArrayList<int> getDisAbleChowCard(int card,bool canChow7,int rulesid)
        {
            int[] canChow = Card.getChowCards(card);
            ArrayList<int> list = new ArrayList<int>();

            for (int i = 0; i < handcards.count; i++)
            {
                int value = handcards.get(i);
                bool b = false;
                for (int j = 0; j < canChow.Length; j++)
                {
                    if (value == canChow[j])
                    {
                        b = true;
                        break;
                    }
                }
                if (!b) list.add(value);
            }
            if (rulesid == RuleSid.AN_YUE_CP&&Room.room.getRule().getRuleAtribute("is25single"))
            {
                if (getCardCount(Card.C25) > 0)
                {
                    list.add(Card.C25);
                }
            }

            return list;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.status = data.readInt();
            this.hu_type = data.readLong();
            this.fanshu = data.readInt();
            this.root_count = data.readInt();
            this.fu_count = data.readInt();
            this.count = new SpotRedCount();
            this.count.bytesRead(data);
            this.point = data.readLong();

            int len = data.readInt();
            this.fixCards=new ArrayList<FixedCards>();
            for (int i = 0; i < len; i++)
            {
                FixedCards cd=new FixedCards();
                cd.bytesRead(data);
                this.fixCards.add(cd);
            }

            len = data.readInt();
            this.handcards=new ArrayList<int>();
            for (int i = 0; i < len; i++)
            {
                this.handcards.add(data.readInt());
            }
        }

        public override PlayerCards clone(PlayerCards pc)
        {
            pc.point = this.point;
            pc.status = this.status;
            for (int i = 0; i < this.handcards.count; i++)
            {
                pc.handcards.add(this.handcards.get(i));
            }
            for (int i = 0; i < this.fixCards.count; i++)
            {
                pc.fixCards.add(this.fixCards.get(i));
            }
            for (int i = 0; i < this.disableCards.count; i++)
            {
                pc.disableCards.add(this.disableCards.get(i));
            }
            if (this.count != null)
            {
                pc.count = new SpotRedCount();
                pc.count.copy(this.count);
            }
            return pc;
        }

        public override void addFixCard(int type, int[] card)
        {
            FixedCards cards = new FixedCards(type, card);
            this.fixCards.add(cards);
        }

        public override FixedCards getSameFixedCards(int card, int count)
        {
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fixs = fixCards.get(i);
                if (fixs.isSameCard(card, count)) return fixs;
            }
            return null;
        }
    }
}