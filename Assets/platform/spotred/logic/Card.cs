using cambrian.common;

namespace platform.spotred
{
    public class Card
    {

        /** 财神 */
        public const  int C01 = 0x1001;
        /** 地牌 */
        public const int C11 = 0x1011;
        /** 丁丁 */
        public const int C12 = 0x1012;
        /** 和牌 */
        public const int C13 = 0x1013;
        /** 长二 */
        public const int C22 = 0x0122;
        /** 幺四 */
        public const int C14 = 0x1014;
        /** 拐子 */
        public const int C23 = 0x0123;
        /** 幺五（猫猫） */
        public const int C15 = 0x1015;
        /** 二四（二红） */
        public const int C24 = 0x1124;
        /** 长三 */
        public const int C33 = 0x0233;
        /** 幺六（独六） */
        public const int C16 = 0x1016;
        /** 二五（黒七） */
        public const int C25 = 0x0125;
        /** 三四（ 红七） */
        public const int C34 = 0x1234;
        /** 二六（平八） */
        public const int C26 = 0x0026;
        /** 三五 */
        public const  int C35 = 0x0135;
        /** 人牌 */
        public const  int C44 = 0x1244;
        /** 黑九 */
        public const  int C36 = 0x0036;
        /** 红九 */
        public const  int C45 = 0x1145;
        /** 四六（苕十） */
        public const  int C46 = 0x1046;
        /** 梅子 */
        public const  int C55 = 0x0155;
        /** 虎头 */
        public const  int C56 = 0x0056;
        /** 天牌 */
        public const  int C66 = 0x1066;
        /** 听用 */
        public const  int C0D = 0x000D;


        /** 没有这张牌 */
        public const int NO_CARD = -1;
        /** 不可见的牌，牌背面 */
        public const int INVISIBLE = 0xFFFF;
        /** 颜色标记位 */
        public const int SIGN_COLOR = 0x1000;
        /** 数字 */

        public static int[][] CARDS =
        {
//
            // 格式：用16进制的四个位分别表示（颜色，索引，左数，右数）占用两个字节
            new int[] {0x1001}, // 财神01(财神红色)
            new int[] {0x1011}, // 地牌11
            new int[] {0x1012}, // 丁丁12
            new int[] {0x1013, 0x0122}, // 和牌13，板凳22
            new int[] {0x1014, 0x0123}, // 幺四14，拐子23
            new int[] {0x1015, 0x1124, 0x0233}, // 猫猫15，二红（二四）24，长三33
            new int[] {0x1016, 0x0125, 0x1234}, // 高脚（膏药）16，二五25，三四34
            new int[] {0x0026, 0x0135, 0x1244}, // 平八26，三五35，人牌44
            new int[] {0x0036, 0x1145}, // 黑九36，红九45
            new int[] {0x1046, 0x0155}, // 苕十46，梅十55
            new int[] {0x0056}, // 斧头56
            new int[] {0x1066}, // 天牌66
            new int[] {0x000D}, // 听用0D(
        };

        /// <summary>
        /// 牌所对应的福数(这个是牌的红点点，如果是全红，那么就是两头点数之和)
        /// </summary>
        public static int[][] CARDS_READ_COUNT =
        {
            new int[] {0}, // 财神01(财神红色)
            new int[] {4}, // 地牌11
            new int[] {1}, // 丁丁12
            new int[] {1, 0}, // 和牌13，板凳22
            new int[] {10, 0}, // 幺四14，拐子23
            new int[] {1, 4, 0}, // 猫猫15，二红（二四）24，长三33
            new int[] {1, 0, 4}, // 高脚（膏药）16，二五25，三四34
            new int[] {0, 0, 16}, // 平八26，三五35，人牌44
            new int[] {0, 4}, // 黑九36，红九45
            new int[] {4, 0}, // 苕十46，梅十55
            new int[] {0}, // 斧头56
            new int[] {6}, // 天牌66
            new int[] {0}, // 听用0D(
        };

        public static string[][] CARD_NAMES =
        {
//
            new string[] {"财神"}, // 01
            new string[] {"地牌"}, // 11
            new string[] {"丁丁"}, // 12
            new string[] {"和牌", "板凳"}, // 13，22
            new string[] {"幺四", "拐子"}, // 14，23
            new string[] {"猫猫", "二红", "长三"}, // 15，24，33
            new string[] {"高脚", "二五", "三四"}, // 16，25，34
            new string[] {"平八", "三五", "人牌"}, // 26，35，44
            new string[] {"黑九", "红九"}, // 36，45
            new string[] {"苕十", "梅十"}, // 46，55
            new string[] {"斧头"}, // 56
            new string[] {"天牌"}, // 66
            new string[] {"听用"}, // 0D
        };

        public static string getName(int card)
        {
            if (card == NO_CARD) return "无";
            if (card == INVISIBLE) return "-1";
            return HexKit.toHex((short)card);
        }

        /** 是否是红牌 */

        public static bool isRed(int card)
        {
            return (card & SIGN_COLOR) > 0;
        }

        /** 是否是黑牌 */

        public static bool isBlack(int card)
        {
            return (card & SIGN_COLOR) == 0;
        }

        /// <summary>
        /// 获取牌的福数(这个是牌的红点点，如果是全红，那么就是两头点数之和)
        /// </summary>
        /// <returns></returns>
        public static int getCardRedCount(int card)
        {
            return CARDS_READ_COUNT[getType(card)][getIndex(card)];
        }

        /// <summary>
        /// 获取牌面值
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int getValue(int card)
        {
            return ((card >> 4) & 0xF) + (card & 0xF);
        }

        /// <summary>
        /// 获取牌的类型
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int getType(int card)
        {
            return getValue(card) - 1;
        }

        /** 获得指定牌在牌表中的索引 */

        public static int getIndex(int card)
        {
            return (card >> 8) & 0xF;
        }

        /** 返回能和指定牌相吃的牌列表 */

        public static int[] getChowCards(int card)
        {
            int value = 14 - getValue(card);
            return CARDS[value - 1];
        }

        /** 是否是全红 */

        public static bool isAllRed(int[][] counts)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == null) continue;
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (i == 2 || i == 10) continue; // 丁斧两边倒
                    if (counts[i][j] > 0 && isBlack(CARDS[i][j])) return false;
                }
            }
            return true;
        }

        /** 是否是全黑 */

        public static bool isAllBlack(int[][] counts)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == null) continue;
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (i == 2 || i == 10) continue; // 丁斧两边倒
                    if (counts[i][j] > 0 && isRed(CARDS[i][j])) return false;
                }
            }
            return true;
        }

        /**
         * 按照两两组合14点消减牌，返回是否消减完
         * 
         * @param counts 各个数值牌以及各个类型牌的数量 count[数值][类型];
         * @return
         */
        protected static bool eliminate14(int[][] counts)
        {
            // 1和11不能改，左右游标，向中间移动，左右游标对应的牌值和为14
            int left = 1, right = 11;
            while (left <= right)
            {
                if (left == right) // 都是6 也就是数值为7的牌，
                {
                    if (total(counts[left++])%2 != 0) return false; // 7不是双数则不可胡
                }
                else
                {
                    if (total(counts[left++]) - total(counts[right--]) != 0) return false; // 求数量差,数量不相等则不可胡
                }
            }
            return true;
        }

        /** 计算数组和 */

        public static int total(int[] array)
        {
            int total = 0;
            for (int i = 0; i < array.Length; i++)
            {
                total += array[i];
            }
            return total;
        }

        /** 获取福数 */
        public static int getFushu(int[][] counts, bool isXiaojia)
        {
            int fushu = 0;
            if (isXiaojia)
            {
                int dingCount = counts[2][0];
                int futouCount = counts[10][0];
                counts[2][0] = 0;
                counts[10][0] = 0;
                for (int i = 0; i < counts.Length; i++)
                {
                    if (counts[i] == null) continue;
                    for (int j = 0; j < counts[i].Length; j++)
                    {
                        if (counts[i][j] > 0 && isRed(CARDS[i][j])) fushu += counts[i][j];
                    }
                }
                counts[2][0] = dingCount;
                counts[10][0] = futouCount;
                if (fushu != 0) fushu += dingCount + futouCount;// 有红，则丁斧头全算红，否则全算黑
            }
            else
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    if (counts[i] == null) continue;
                    for (int j = 0; j < counts[i].Length; j++)
                    {
                        if (counts[i][j] > 0 && isRed(CARDS[i][j])) fushu += counts[i][j];
                    }
                }
            }
            return fushu;
        }


        public static int[][] getEmptyArray()
        {
            return new int[][]
            {
                new int[1], // 1
                new int[1], // 2
                new int[1], // 3
                new int[2], // 4
                new int[2], // 5
                new int[3], // 6
                new int[3], // 7
                new int[3], // 8
                new int[2], // 9
                new int[2], // 10
                new int[1], // 11
                new int[1], // 12
                new int[1] // 13
            };
        }
    }
}
