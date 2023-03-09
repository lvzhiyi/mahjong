using cambrian.common;
using UnityEngine;

namespace platform.mahjong
{
/**
 * 类说明：麻将牌型消除计算器
 * 
 * <pre>
 *  成牌牌型定义：ABC-任意顺子，AAA-任意刻子，DD-任意对子
 *  胡牌牌型公式：m*ABC+n*AAA+DD （m,n 为自然数）
 *  算法核心逻辑：递归回溯法
 *      尝试消除ABC,AAA,DD形式的一成牌
 *      如果消除一成牌后还有剩牌，则递归消除直到无剩余牌（成功，终止回溯）或剩余牌无法成牌（失败，回溯继续）
 *      一次消除失败并回溯后，尝试消减另一种成牌，直到所有可能的成牌都尝试完成
 *      如果某层递归的所有尝试都失败，则继续往上层回溯，直到第一层的消除尝试
 *      如果第一层所有尝试都失败，则不符合胡牌牌型
 *  
 *  类型和序数：{@linkplain zs.game.rule.mj.Mahjong 麻将牌}中定义
 *  数据结构：int[类型][序数]
 *           {
 *               {4,4,4,4,4,4,4,4,4}, // 1~9筒
 *               {4,4,4,4,4,4,4,4,4}, // 1~9条
 *               {4,4,4,4,4,4,4,4,4}, // 1~9万
 *               {4,4,4,4}, // 风：东南西北
 *               {4,4,4}, // 箭：中发白
 *               {1,1,1,1,1,1,1,1}, // 花：春夏秋冬梅兰竹菊
 *           }
 * </pre>
 * 
 * @author HYZ [huangyz1988@qq.com]
 * @version 2020年3月28日 上午2:14:06
 */
    public class Eliminator
    {
        /** 消减结果：无法消减完成 */
        public const int FAIL = -1;

        /** 消减结果：带将头消减完 */
        public const int WITH_PAIR = 0;

        /** 消减结果：不带将头消减完 */
        public const int WITHOUT_PAIR = 1;

        /**
         * 消减序数牌，按照将牌，顺子，刻子递归消减牌，返回是否消减完
         * 
         * @param arr 单种花色的各个牌数量
         * @return 是否消减后无剩余牌
         */
        public static int eliminate(int[] arr)
        {
            return eliminate(arr, 0, true);
        }

        /**
         * 消减序数牌，按照将牌，顺子，刻子递归消减牌，返回是否消减完
         * 
         * @param arr 单种花色的各个牌数量
         * @param needPair 是否需要扣除将牌
         * @return 是否消减后无剩余牌
         */
        public static int eliminate(int[] arr, bool needPair)
        {
            return eliminate(arr, 0, needPair);
        }

        /**
	 * 消减序数牌且忽略将牌必要性，按照将牌，顺子，刻子递归消减牌，返回是否消减完
	 * 
	 * @param arr 单种花色的各个牌数量
	 * @return 是否消减后无剩余牌
	 */
        public static  bool eliminateIgnorePair(int[] arr)
        {
            return eliminate(arr, 0, true) != FAIL;
        }

        /**
         * 消减序数牌，按照将牌，顺子，刻子递归消减牌，返回是否消减完
         * 
         * @param arr 单种花色的各个牌数量
         * @param i 从指定位置开始
         * @param needPair 是否需要扣除将牌
         * @return 是否消减后无剩余牌
         */
        public static int eliminate(int[] arr, int i, bool needPair)
        {
            int r = WITHOUT_PAIR; // 假定未扣除将牌能消除
            for (; i < arr.Length; ++i)
            {
                if (arr[i] == 0) continue;
                if (arr[i] > 2) // 扣除刻子
                {
                    arr[i] -= 3;
                    r = eliminate(arr, i, needPair);
                    arr[i] += 3;
                    if (r != FAIL) return r;
                }

                if (i < 7 && arr[i + 1] > 0 && arr[i + 2] > 0) // 扣除顺子
                {
                    --arr[i];
                    --arr[i + 1];
                    --arr[i + 2];
                    r = eliminate(arr, i, needPair);
                    ++arr[i];
                    ++arr[i + 1];
                    ++arr[i + 2];
                    if (r != FAIL) return r;
                }

                if (needPair && arr[i] > 1) // 扣除将头
                {
                    needPair = false;
                    arr[i] -= 2;
                    r = eliminate(arr, i, needPair);
                    arr[i] += 2;
                    if (r != FAIL) return WITH_PAIR;
                    needPair = true;
                }

                // 当前索引的牌不能用于组成刻子，顺子，或需要将牌时无法组成将牌
                return FAIL; // 快速失败
            }

            // 走到这里一定以某种方式消减成功，但是如果needPair为true表示实际未扣除将牌
            return (needPair ? WITHOUT_PAIR : r);
        }

        /**
         * 消减风牌和箭牌，按照将牌,刻子递归消减牌，返回是否消减完
         * 
         * @param arr 风牌或箭牌各个牌数量
         * @return 是否消减后无剩余牌
         */
        public static int eliminateWD(int[] arr)
        {
            return eliminate(arr, 0, true);
        }

        /**
         * 消减风牌和箭牌，按照将牌,刻子递归消减牌，返回是否消减完
         * 
         * @param arr 风牌或箭牌各个牌数量
         * @param needPair 是否需要扣除将牌
         * @return 是否消减后无剩余牌
         */
        public static int eliminateWD(int[] arr, bool needPair)
        {
            return eliminate(arr, 0, needPair);
        }

        /**
         * 消减风牌和箭牌，按照将牌,刻子递归消减牌，返回是否消减完
         * 
         * @param arr 风牌或箭牌各个牌数量
         * @param i 从指定位置开始
         * @param needPair 是否需要扣除将牌
         * @return 是否消减后无剩余牌
         */
        public static int eliminateWD(int[] arr, int i, bool needPair)
        {
            int r = WITHOUT_PAIR; // 假定未扣除将牌能消除
            for (; i < arr.Length; ++i)
            {
                if (arr[i] == 0) continue;
                if (arr[i] == 3) // 扣除刻子
                {
                    arr[i] -= 3;
                    r = eliminateWD(arr, i, needPair);
                    arr[i] += 3;
                    if (r != FAIL) return r;
                }

                if (needPair && arr[i] == 2) // 扣除将头
                {
                    needPair = false;
                    arr[i] -= 2;
                    r = eliminateWD(arr, i, false);
                    arr[i] += 2;
                    if (r != FAIL) return WITH_PAIR; // 这里实际扣除将牌操作
                    needPair = true;
                }

                // 当前索引的牌不能用于组成刻子，或需要将牌时无法组成将牌
                return FAIL; // 快速失败
            }

            // 走到这里一定以某种方式消减成功，但是如果needPair为true表示实际未扣除将牌
            return (needPair ? WITHOUT_PAIR : r);
        }

        /**
         * 是否能消减完
         * 
         * @param arr 待消减数组
         * @param needPair 是否需要消减将牌
         * @return true能消减完
         */
        public static bool canEliminate(int[][] arr, int bits,bool needPair)
        {
            for (int i = 0, r; i < arr.Length; i++)
            {
                if ((bits&MJCard.BITS[i])==0||i == MJCard.TYPE_FLOWER) continue;
                if (i == MJCard.TYPE_WIND || i == MJCard.TYPE_DRAGON)
                    r = eliminateWD(arr[i], 0, needPair);
                else
                    r = eliminate(arr[i], 0, needPair);
                if (r == FAIL) return false; // 无法消减完
                if (r == WITH_PAIR) needPair = false;
            }

            return true;
        }

        /**
         * 检查原始牌分别加入听牌列表中每个牌后是否都能消减完
         * 
         * @param arr 待消减数组
         * @param listens 听牌列表
         * @param needPair 是否需要消减将牌
         * @return true能消减完
         */
        public static bool canEliminate(int[][] arr, int[] listens,int bits, bool needPair)
        {
            for (int k = 0, type, index; k < listens.Length; k++)
            {
                type = MJCard.getType(listens[k]);
                index = MJCard.getIndex(listens[k]);
                ++arr[type][index]; // 加入听牌
                bool ok = canEliminate(arr, bits,needPair);
                --arr[type][index]; // 回溯，移除听牌
                if (!ok) return false; // 无法消减完
            }

            return true;
        }

        /**
         * 消减序数牌的每组刻子，顺子（不管将牌），返回是否能消减完
         * <p>
         * 该方法假定外部调用前，已经对将牌做出处理，所以这里不消减将牌
         * 
         * @param arr 单种花色的各个牌数量
         * @param i 从指定位置开始
         * @return true能消减完
         */
        public static bool eliminateGroup(int[] arr, int i)
        {
            for (bool ok; i < arr.Length; ++i)
            {
                if (arr[i] == 0) continue;
                if (i < 7 && arr[i + 1] > 0 && arr[i + 2] > 0) // 扣除顺子
                {
                    --arr[i];
                    --arr[i + 1];
                    --arr[i + 2];
                    ok = eliminateGroup(arr, i);
                    ++arr[i];
                    ++arr[i + 1];
                    ++arr[i + 2];
                    if (ok) return true;
                }

                if (arr[i] > 2) // 扣除刻子
                {
                    arr[i] -= 3;
                    ok = eliminateGroup(arr, i);
                    arr[i] += 3;
                    if (ok) return true;
                }

                return false;
            }

            return true;
        }

        /**
         * 消减字牌的每组刻子（不管将牌），返回是否能消减完
         * <p>
         * 该方法假定外部调用前，已经对将牌做出处理，所以这里不消减将牌
         * 
         * @param arr 单种花色的各个牌数量
         * @param i 从指定位置开始
         * @return true能消减完
         */
        public static bool eliminateGroupWD(int[] arr, int i)
        {
            for (bool ok; i < arr.Length; ++i)
            {
                if (arr[i] == 0) continue;
                if (arr[i] == 3) // 扣除刻子
                {
                    arr[i] -= 3;
                    ok = eliminateGroupWD(arr, i + 1);
                    arr[i] += 3;
                    if (ok) return true;
                }

                return false;
            }

            return true;
        }

        /**
         * 消减每组刻子和顺子，返回是否消减完
         * 
         * @param arr 各种花色的各个牌数量
         * @return true能消减完
         */
        public static bool eliminateGroup(int[][] arr,int bits)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if ((bits& MJCard.BITS[i])==0||i == MJCard.TYPE_FLOWER) continue;
                bool ok = true;
                if (i == MJCard.TYPE_WIND || i == MJCard.TYPE_DRAGON)
                    ok = eliminateGroupWD(arr[i], 0);
                else
                    ok = eliminateGroup(arr[i], 0);
                if (!ok) return ok; // i类型的牌消减失败直接终止
            }

            return true;
        }

        /**
         * 牌型判断：是否是齐对（例如：7对，龙对）,默认允许固定牌补花
         * 
         * @param arr 手牌各个类型每个牌数量
         * @param fixs 固定牌各个类型每个牌数量
         * @return true符合齐对
         */
        public static bool checkHuNeatPairs(int[][] arr, int[][] fixs,int bits)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                if ((bits& MJCard.BITS[i])==0||i == MJCard.TYPE_FLOWER) continue;
                for (int j = 0; j < arr[i].Length; ++j)
                {
                    // 除花牌外，某张牌数量为奇数（0算偶数），或者出现固定牌，则一定不是齐对
				if(fixs[i][j]>0||(arr[i][j]&1)==1)
				    return false;
			}
		}
		return true;
	}

        /**
         * 检查是否可以胡（忽略将牌必要性，有无将牌都可以）
         * <p>
         * 该方法在消减过程中，仍然会尝试扣除将牌，不管扣与不扣，最终能消减完即返回true
         * 
         * @param arr 每个类型各个牌的数量
         * @return true可胡
         */
        public static bool checkHuIgnorePair(int[][] arr,int bits)
        {
            for (int i = 0, r; i < arr.Length; i++)
            {
                if ((bits& MJCard.BITS[i])==0||i == MJCard.TYPE_FLOWER) continue;
                if (i == MJCard.TYPE_WIND || i == MJCard.TYPE_DRAGON)
                    r = eliminateWD(arr[i], 0, true);
                else
                    r = eliminate(arr[i], 0, true);
                if (r == FAIL) return false; // 无法消减完
            }

            return true;
	}

	/**
	 * 检查是否可以胡（用于自摸判定）
	 * 
	 * @param arr 每个类型各个牌的数量
	 * @return true可胡
	 */
	public static  bool checkHu(int[][] arr,int bits)
	{
		bool pair=true;// 需要将头
		for(int i=0,r;i<arr.Length;i++)
		{
			if((bits& MJCard.BITS[i])==0||i== MJCard.TYPE_FLOWER) continue;
			if(i== MJCard.TYPE_WIND ||i== MJCard.TYPE_DRAGON)
				r=eliminateWD(arr[i],0,true);
			else
				r=eliminate(arr[i],0,true);
			if(r==FAIL) return false;// 要不要将头都无法消减完
			if(r==WITH_PAIR) pair=false;
		}
		// 走到这里一定消减完了，但是需要判定将牌是否扣除
		return !pair;
	}

        /**
         * 检查是否可以胡指定牌（用于点炮判定）
         * 
         * @param arr 每个类型各个牌的数量
         * @param card 待胡的牌
         * @return true可胡
         */
        public static bool checkHu(int[][] arr, int bits,int card)
        {
            int type = MJCard.getType(card);
            int index = MJCard.getIndex(card);
            ++arr[type][index];
            bool ok = checkHu(arr,bits);
            --arr[type][index];
            return ok;
        }

        /**
         * 检查原始牌分别加入听牌列表中的每张牌后是否都可以胡
         * 
         * @param arr 每个类型各个牌的数量
         * @param listens 听牌列表
         * @return true列表内牌都可胡
         */
        public static bool checkHu(int[][] arr, int[] listens,int bits)
        {
            bool ok = true; // 假定所有叫牌都可胡
            for (int k = 0, type, index; k < listens.Length; k++)
            {
                type = MJCard.getType(listens[k]);
                index = MJCard.getIndex(listens[k]);
                ++arr[type][index]; // 加入听牌
                ok = checkHu(arr,bits);
                --arr[type][index]; // 回溯，移除听牌
                if (!ok) break; // 只要一个不能胡就中断本轮检查
            }

            return ok;
        }

        /**
         * 检出所有可以胡的牌（用于胡牌提示）
         * 
         * @param arr 每个类型各个牌的数量
         * @return 听牌列表
         */
        public static int[] checkOutListens(int[][] arr,int bits)
        {
            ArrayList<int> sets = new ArrayList<int>();
            checkOutListens(arr, sets,bits);
            return sets.toArray();
        }

        /**
         * 检出所有可以胡的牌（用于胡牌提示）
         * 
         * @param arr 每个类型各个牌的数量
         * @param sets 可听牌装载容器
         */
        public static void checkOutListens(int[][] arr, ArrayList<int> sets, int bits)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                //if ((bits & MJCard.BITS[i]) == 0) continue;
                for (int j = 0, len = arr[i].Length; j < len; j++)
                {
                    //if (arr[i][j] == 4) continue;
                    // 忽略检查条件，若 不存在该牌，且相邻的牌也不存在，无法组成对子刻子或顺子
                    if (j == 0)
                    {
                        if (arr[i][j] == 0 && arr[i][j + 1] == 0) continue;
                    }
                    else if (j == len - 1)
                    {
                        if (arr[i][j] == 0 && arr[i][j - 1] == 0) continue;
                    }
                    else
                    {
                        if (arr[i][j] == 0 && arr[i][j + 1] == 0 && arr[i][j - 1] == 0) continue;
                    }

                    // 可组成对子，刻子，顺子
                    arr[i][j]++;
                    if (checkHu(arr, bits))
                    {
                        sets.add(MJCard.getCard(i, j));
                    }
                    arr[i][j]--;
                }
            }
        }

        /**
         * 检出所有可以胡的牌（用于躺牌胡牌提示）
         * 
         * @param arr 每个类型各个牌的数量
         * @return 听牌列表
         */
        public static int[] checkOutListensIgnorePair(int[][] arr,int bits)
        {
            ArrayList<int> sets = new ArrayList<int>();
            checkOutListensIgnorePair(arr, sets,bits);
            return sets.toArray();
        }

        /**
         * 检出所有可以胡的牌（用于躺牌胡牌提示）
         * 
         * @param arr 每个类型各个牌的数量
         * @param sets 可听牌装载容器
         */
        public static void checkOutListensIgnorePair(int[][] arr, ArrayList<int> sets,int bits)
        {
            for (int i = 0; i < arr.Length; i++)
            {
			if((bits& MJCard.BITS[i])==0) continue;
                for (int j = 0, len = arr[i].Length; j < len; j++)
                {
                    //if (arr[i][j] == 4) continue;
                    if (j == 0)
                    {
                        if (arr[i][j] == 0 && arr[i][j + 1] == 0) continue;
                    }
                    else if (j == len - 1)
                    {
                        if (arr[i][j] == 0 && arr[i][j - 1] == 0) continue;
                    }
                    else
                    {
                        if (arr[i][j] == 0 && arr[i][j + 1] == 0 && arr[i][j - 1] == 0) continue;
                    }

                    arr[i][j]++;
                    if (checkHuIgnorePair(arr,bits)) sets.add(MJCard.getCard(i, j));
                    arr[i][j]--;
                }
            }
        }

        /**
         * 简化一组牌，并保证简化后可胡listens中的所有牌（用于一键摆叫）
         * 
         * <pre>
         *  注意：
         *      1. 该方法会改变arr数组内数据，若arr在方法外部被共享，那么需要将数组克隆后传入
         *      2. arr中不应该包含花类型得牌，否则花牌将被保留
         * </pre>
         * 
         * @param arr 各种花色的每个牌数量，
         * @param listens 精简前可胡的牌
         */
        public static void simplify(int[][] arr, int[] listens,int bits)
        {
            simplify(arr, listens, bits, true);
        }

        /**
         * 简化一组牌，并保证简化后可胡listens中的所有牌（用于一键摆叫）
         * 
         * <pre>
         *  注意：
         *      1. 该方法会改变arr数组内数据，若arr在方法外部被共享，那么需要将数组克隆后传入
         *      2. arr中不应该包含花类型得牌，否则花牌将被保留
         * </pre>
         * 
         * @param arr 各种花色的每个牌数量，
         * @param listens 精简前可胡的牌
         * @param neatPairAll 七对多加不精简
         */
        public static void simplify(int[][] arr, int[] listens, int bits, bool neatPairAll)
        {
            int c = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if ((bits & MJCard.BITS[i]) == 0 || i == MJCard.TYPE_FLOWER) continue;
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] < 2) continue;
                    c += arr[i][j] >> 1;
                }
            }
            if (c == 6) // 7对
            {
                if ( listens.Length > 1) return; // 符合7对，但多个叫，只能全部躺出
               
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if ((bits & MJCard.BITS[i]) == 0 || i == MJCard.TYPE_FLOWER) continue;
                        for (int j = 0; j < arr[i].Length; j++)
                        {
                            arr[i][j] = ((arr[i][j] & 1) == 1) ? 1 : 0; // 消除成对的牌，剩下单张
                        }
                    }
                    return;
                
            }
            simplifyGroup(arr, listens, bits, true);
            for (int i = 0; i < arr.Length; i++)
            {
                if ((bits & MJCard.BITS[i]) == 0 || i == MJCard.TYPE_FLOWER) continue;
                for (int j = 0; j < arr[i].Length; ++j)
                {
                    if (arr[i][j] < 2) continue;
                    // 扣除将头，看是否所有叫牌仍然可胡
                    arr[i][j] -= 2;
                    bool ok = canEliminate(arr, listens, bits, false);
                    if (ok)
                    {
                        simplifyGroup(arr, listens, bits, false);
                        return;
                    }
                    arr[i][j] += 2;
                }
            }
        }

        /** 消减组牌，并保证剩余的牌可胡listens中的所有牌 */
        static void simplifyGroup(int[][] arr, int[] listens,int bits,bool pair)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if ((bits& MJCard.BITS[i])==0||i == MJCard.TYPE_FLOWER) continue;
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == 0) continue;
                    if (arr[i][j] > 2) // 扣除刻子
                    {
                        arr[i][j] -= 3;
                        if (canEliminate(arr, listens, bits, pair))
                        {
                            simplifyGroup(arr, listens, bits, pair);
                            return;
                        }
                        else
                            arr[i][j] += 3;
                    }
                    if (i != MJCard.TYPE_WIND && i != MJCard.TYPE_DRAGON)
                    {
                        if (j < 7 && arr[i][j + 1] > 0 && arr[i][j + 2] > 0) // 扣除顺子
                        {
                            --arr[i][j];
                            --arr[i][j + 1];
                            --arr[i][j + 2];
                            if (canEliminate(arr, listens, bits,pair))
                            {
                                simplifyGroup(arr, listens,bits, pair);
                                return;
                            }
                            else
                            {
                                ++arr[i][j];
                                ++arr[i][j + 1];
                                ++arr[i][j + 2];
                            }
                        }
                    }
                }
            }
        }


        public static int[][] getCounts(int[] cards)
        {
            int[][] counts = new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            int card = -1;
            for (int i = 0; i < cards.Length; i++)
            {
                card = cards[i];
                counts[MJCard.getType(card)][MJCard.getIndex(card)]++;
            }
            return counts;
        }

        public static int[] getTang(int[][] arr)
        {
            ArrayList<int> list = new ArrayList<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0, len = arr[i].Length; j < len; j++)
                {
                    if (arr[i][j] > 0)
                    {
                        list.add(MJCard.getCard(i, j), arr[i][j]);
                    }
                }
            }
            return list.toArray();
        }
    }
}
