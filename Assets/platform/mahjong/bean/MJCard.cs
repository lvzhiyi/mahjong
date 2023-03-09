using cambrian.common;

namespace platform.mahjong
{

    /**
 * 类说明：麻将牌定义
 * 
 * <pre>
 * 中文====英文====解释
 * 麻将	Mahjongg / Mahjong	　
 * 张	Tile	一张麻将牌的牌面
 * 花	Flower	春、夏、秋、冬、梅、兰、竹、菊
 * 字牌(役牌)	Honor	东、南、西、北、中、发、白
 * 花色(配牌)	Suit	筒、条、万
 * 数字	Rank	一~九
 * 箭牌	Dragon	中、发、白
 * 风牌	Wind	东、南、西、北
 * 筒牌	Dot	一筒~九筒
 * 条牌	Bamboo	一条~九条
 * 万牌	Character	一万~九万
 * 春、夏、秋、冬	Spring / Summer / Autumn / Winter	　
 * 梅、兰、竹、菊	Plum / Orchid / Bamboo / Chrysanthemum	　
 * 中、发、白	Red / Green / White Dragon	　
 * 东、南、西、北	East / South / West / North Wind	　
 * 门风(自风)	Dealer's Wind	　
 * 场风(圈风)	Prevailing Wind	　
 * 庄家	Dealer / Banker	　
 * 发牌	Deal	　
 * 骰子	Die (pl. Dice)	　
 * 洗牌	Shuffle	　
 * 牌墙	Wall	　
 * 牌墩	Stack	牌墙中的上下两张牌组成一墩
 * 牌桌	Board	　
 * 和(胡)牌	Win	　
 * 和(胡)张	Winning Tile	　
 * 荒牌(流局)	Draw	　
 * 摸牌	Draw	　
 * 打牌	Discard	　
 * 听牌	Ready Hand	　
 * 混牌(百搭)	Joker	　
 * 玩家	Player	　
 * 一组	Meld	一个顺子、刻子或杠子
 * 将牌(雀头、对子)	Eyes / Pair	　
 * 刻子	Triplet	　
 * 顺子	Sequence	　
 * 明杠、暗杠	Exposed / Concealed Kong	　
 * 碰、杠、吃	Pong / Kong / Chow	　
 * 数番	Scoring	　
 * 番数	Points
 * </pre>
 * 
 * @author HYZ (huangyz1988@qq.com)
 * @create 2016年11月14日 下午5:49:42
 * @version 1.0
 */

    public class MJCard
    {
        /// <summary>被胡走牌标记</summary>
        public const int SIGN_HU = 1 << 12;

        /// <summary>躺牌标记</summary>
        public const int SIGN_TANG = 1 << 13;

        /// <summary> 冲锋鸡牌标记</summary>
        public const int SIGN_CHICKEN = 1 << 14;

        /// <summary> 不可杠的牌</summary>
        public const int SIGN_NO_KONG = 1 << 15;

        /** 牌：1筒 */
        public const int DOT_1 = 0x0100;

        /** 牌：2筒 */
        public const int DOT_2 = 0x0101;

        /** 牌：3筒 */
        public const int DOT_3 = 0x0102;

        /** 牌：4筒 */
        public const int DOT_4 = 0x0103;

        /** 牌：5筒 */
        public const int DOT_5 = 0x0104;

        /** 牌：6筒 */
        public const int DOT_6 = 0x0105;

        /** 牌：7筒 */
        public const int DOT_7 = 0x0106;

        /** 牌：8筒 */
        public const int DOT_8 = 0x0107;

        /** 牌：9筒 */
        public const int DOT_9 = 0x0108;

        /** 牌：1条 */
        public const int BAM_1 = 0x0200;

        /** 牌：2条 */
        public const int BAM_2 = 0x0201;

        /** 牌：3条 */
        public const int BAM_3 = 0x0202;

        /** 牌：4条 */
        public const int BAM_4 = 0x0203;

        /** 牌：5条 */
        public const int BAM_5 = 0x0204;

        /** 牌：6条 */
        public const int BAM_6 = 0x0205;

        /** 牌：7条 */
        public const int BAM_7 = 0x0206;

        /** 牌：8条 */
        public const int BAM_8 = 0x0207;

        /** 牌：9条 */
        public const int BAM_9 = 0x0208;

        /** 牌：1萬 */
        public const int CHA_1 = 0x0300;

        /** 牌：2萬 */
        public const int CHA_2 = 0x0301;

        /** 牌：3萬 */
        public const int CHA_3 = 0x0302;

        /** 牌：4萬 */
        public const int CHA_4 = 0x0303;

        /** 牌：5萬 */
        public const int CHA_5 = 0x0304;

        /** 牌：6萬 */
        public const int CHA_6 = 0x0305;

        /** 牌：7萬 */
        public const int CHA_7 = 0x0306;

        /** 牌：8萬 */
        public const int CHA_8 = 0x0307;

        /** 牌：9萬 */
        public const int CHA_9 = 0x0308;

        /** 风牌：东风 */
        public const int WIND_E = 0x0400;

        /** 风牌：南风 */
        public const int WIND_S = 0x0401;

        /** 风牌：西风 */
        public const int WIND_W = 0x0402;

        /** 风牌：北风 */
        public const int WIND_N = 0x0403;

        /** 箭牌：红中 */
        public const int DRAGON_Z = 0x0500;

        /** 箭牌：发财 */
        public const int DRAGON_F = 0x0501;

        /** 箭牌：白板 */
        public const int DRAGON_B = 0x0502;

        /** 花牌：春 */
        public const int Spr = 0x0600;

        /** 花牌：夏 */
        public const int Sum = 0x0601;

        /** 花牌：秋 */
        public const int Aut = 0x0602;

        /** 花牌：冬 */
        public const int Win = 0x0603;

        /** 花牌：梅 */
        public const int Mei = 0x0604;

        /** 花牌：兰 */
        public const int Lan = 0x0605;

        /** 花牌：竹 */
        public const int Zhu = 0x0606;

        /** 花牌：菊 */
        public const int Ju = 0x0607;

        /// <summary>
        /// 没牌
        /// </summary>
        public const int CNO = -1;

        /// <summary>
        /// 别人的牌
        /// </summary>
        public const int CIN = 0xFFFF;

        /** 牌级别：0最小值，8最大值 */
        public const int MIN_INDEX = 0, MAX_INDEX = 8;

        /** 牌花色类型：1-筒，2-条，3-万，4-风，5-箭，6-花 */
        public const int TYPE_DOT = 0, TYPE_BAM = 1, TYPE_CHA = 2, TYPE_WIND = 3, TYPE_DRAGON = 4, TYPE_FLOWER = 5;

        /** 牌类型位值：用于标记类型占位，判定一个组合值包含那些类型，1筒，2条，4万，8风，16箭，32花 */
        public const int DOT = 1, BAM = 2, CHA = 4, WIND = 8, DRAGON = 16, FLOWER = 32;

        /** 类型值数组 */
        public static int[] BITS = {DOT, BAM, CHA, WIND, DRAGON, FLOWER};

        public static int ALL_BITS = DOT | BAM | CHA | WIND | DRAGON | FLOWER;

        /** 定义麻将牌ID */
        public static int[][] CARDS =
        {
            // 分别对应下面的牌
            new int[] {DOT_1, DOT_2, DOT_3, DOT_4, DOT_5, DOT_6, DOT_7, DOT_8, DOT_9}, //
            new int[] {BAM_1, BAM_2, BAM_3, BAM_4, BAM_5, BAM_6, BAM_7, BAM_8, BAM_9}, //
            new int[] {CHA_1, CHA_2, CHA_3, CHA_4, CHA_5, CHA_6, CHA_7, CHA_8, CHA_9}, //
            new int[] {WIND_E, WIND_S, WIND_W, WIND_N}, //
            new int[] {DRAGON_Z, DRAGON_F, DRAGON_B}, //
            new int[] {Spr, Sum, Aut, Win, Mei, Lan, Zhu, Ju}, //

        };

        /** 定义麻将牌名字 */
        private static string[][] NAMES =
        {
            // 12箭牌 + 16风牌 + 8花 + 36筒 + 36条 + 36万 = 共144张
            new string[] {"1筒", "2筒", "3筒", "4筒", "5筒", "6筒", "7筒", "8筒", "9筒"}, // 每个4张*9=共36张
            new string[] {"1条", "2条", "3条", "4条", "5条", "6条", "7条", "8条", "9条"}, // 每个4张*9=共36张
            new string[] {"1萬", "2萬", "3萬", "4萬", "5萬", "6萬", "7萬", "8萬", "9萬"}, // 每个4张*9=共36张
            new string[] {"~東", "~南", "~西", "~北"}, // 每个4张*4=共16张
            new string[] {"~中", "~發", "~囗"}, // 每个4张*3=共12张
            new string[] {"~春", "~夏", "~秋", "~冬", "~梅", "~蘭", "~竹", "~菊"} // 每个1张*8=共8张
        };

        /** 获得指定编号牌的名字 */
        public static string getName(int card)
        {
            if (card == CNO) return "~空";
            if (card == CIN) return "~背";
            return NAMES[getType(card)][getIndex(card)];
        }

        public static string getTypeName(int type)
        {
            switch (type)
            {
                case TYPE_DOT:
                    return "筒";
                case TYPE_BAM:
                    return "条";
                case TYPE_CHA:
                    return "萬";
                case TYPE_WIND:
                    return "風";
                case TYPE_DRAGON:
                    return "箭";
                case TYPE_FLOWER:
                    return "花";
                default:
                    return "　";
            }
        }


        /** 获得牌的索引 */
        public static int getIndex(int card)
        {
            if (card == CIN || card == CNO) return -1;
            return card & 0xFF;
        }

        /** 获得牌的类型：筒，条，万，风，箭，花 */
        public static int getType(int card)
        {
            if (card == CIN || card == CNO) return -1;
            return ((card >> 8) & 0xF) - 1;
        }

        /** 获取牌面值 */
        public static int getValue(int card)
        {
            if (card == CIN || card == CNO) return -1;
            return (card & 0xFF) + 1;
        }

        /** 获得牌ID */
        public static int getCard(int type, int index)
        {
            return ((type + 1) << 8) | index;
        }

        /** 获取牌的类型值 */
        public static int getTypeBit(int card)
        {
            switch (getType(card))
            {
                case TYPE_DOT:
                    return DOT;
                case TYPE_BAM:
                    return BAM;
                case TYPE_CHA:
                    return CHA;
                case TYPE_WIND:
                    return WIND;
                case TYPE_DRAGON:
                    return DRAGON;
                case TYPE_FLOWER:
                    return FLOWER;
            }

            return 0;
        }


        public static bool isSameCard(int c1, int c2)
        {
            return ((c1^c2) & 0xFFF) == 0;
        }

        /** 根据类型值获取包含的类型数量 */
        public static int getCardTypeCount(int bits)
        {
            int count = 0, bit;
            for (int i = 0; i < 3; ++i)
            {
                bit = BITS[i];
                if ((bits & bit) == bit) ++count;
            }

            return count;
        }

        /** 给指定牌标记指定值 */
        public static int sign(int card, int sign)
        {
            return card | sign;
        }

        public static int cancelSign(int card, int sign)
        {
            return card &= (~sign);
        }

        /** 指定牌是否被标记指定标记 */
        public static bool isSigned(int card, int sign)
        {
            return (card & sign) == sign;
        }

        /** 根据类型值获取牌数组 */
        public static int[] getCards(int value)
        {
            ArrayList<int> list = new ArrayList<int>(144);
            for (int i = 0; i < CARDS.Length; i++)
            {
                int v = getValue(i);
                if ((v & value) != v) continue;

                if (i == TYPE_FLOWER)
                {
                    for (int j = 0; j < CARDS[i].Length; ++j)
                    {
                        list.add(CARDS[i][j]);
                    }
                }
                else
                {
                    for (int j = 0; j < CARDS[i].Length; ++j)
                    {
                        list.add(CARDS[i][j]);
                        list.add(CARDS[i][j]);
                        list.add(CARDS[i][j]);
                        list.add(CARDS[i][j]);
                    }
                }
            }

            return list.toArray();
        }

        /**
         * 获得可以和指定牌吃的牌
         * 
         * <pre>
         * 例如
         *  1      return  [2,3]
         *  2      return  [1,3],[3,4]
         *  x(3~7) return  [x-2,x-1],[x-1,x+1],[x+1,x+2]
         *  8      return  [6,7],[7,9]
         *  9      return  [7,8]
         * </pre>
         * 
         * @param card
         * @return
         */
        public static int[][] getChowArray(int card)
        {
            int t = getType(card);
            if (t == TYPE_DOT || t == TYPE_BAM || t == TYPE_CHA)
            {
                t <<= 8;
                int v = getIndex(card);
                switch (v)
                {
                    case MAX_INDEX:
                        return new int[][] {new int[] {t & (v - 1), t & (v - 2)}};
                    case MAX_INDEX - 1:
                        return new int[][] {new int[] {t & (v - 1), t & (v + 1)}, new int[] {t & (v - 1), t & (v - 2)}};
                    case MIN_INDEX:
                        return new int[][] {new int[] {t & (v + 1), t & (v + 2)}};
                    case MIN_INDEX + 1:
                        return new int[][] {new int[] {t & (v - 1), t & (v + 1)}, new int[] {t & (v + 1), t & (v + 2)}};
                    default:
                        return new int[][]
                        {
                            new int[] {t + v - 1, t + v + 1}, new int[] {t & (v - 1), t & (v - 2)},
                            new int[] {t & (v + 1), t & (v + 2)}
                        };
                }
            }

            return null;
        }

        /** 获得以指定牌为卡张的牌（AXB） ，如果不能，返回空 */
        public static int[] getAsMid(int tile)
        {
            int t = getType(tile);
            if (t == TYPE_DOT || t == TYPE_BAM || t == TYPE_CHA)
            {
                t <<= 8;
                int v = getIndex(tile); // 牌面值
                if (v == MAX_INDEX || v == MIN_INDEX) return Single.EMPTY_INT_ARRAY;
                return new int[] {t & (v - 1), t & (v + 1)};
            }

            return Single.EMPTY_INT_ARRAY;
        }

        /** 获得以指定牌为边张的牌（12X,X89），如果不能，返回空 */
        public static int[] getAsSide(int card)
        {
            int t = getType(card);
            if (t == TYPE_DOT || t == TYPE_BAM || t == TYPE_CHA)
            {
                t <<= 8;
                int v = getIndex(card); // 牌索引
                if (v == MAX_INDEX - 2) return new int[] {t & (v + 1), t & MAX_INDEX};
                if (v == MIN_INDEX + 2) return new int[] {t & MIN_INDEX, t & (v - 1)};
                return Single.EMPTY_INT_ARRAY;
            }

            return Single.EMPTY_INT_ARRAY;
        }
    }
}
