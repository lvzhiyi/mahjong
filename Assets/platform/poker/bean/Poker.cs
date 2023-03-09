namespace platform.poker
{
    /// <summary> 扑克 </summary>
    public class Poker
    {
        /// <summary> 不可见的牌，牌背面 </summary>
        public const int INVISIBLE = 0xFFFF;

        /// <summary> 沒有这张牌 </summary>
        public const int CNO = -1;

        /// <summary> 扑克牌类型：方块 </summary>
        public const int TYPE_FANG = 0;

        /// <summary> 扑克牌类型：梅花 </summary>
        public const int TYPE_MEI = 1;

        /// <summary> 扑克牌类型：红桃 </summary>
        public const int TYPE_HONG = 2;

        /// <summary> 扑克牌类型：黑桃 </summary>
        public const int TYPE_HEI = 3;

        /// <summary> 扑克牌类型：JOKER </summary>
        public const int TYPE_JOKER = 4;

        // A（4黑，3红，2梅，1方）
        public const int C1A = 0x11, C2A = 0x21, C3A = 0x31, C4A = 0x41;
        // 2（4黑，3红，2梅，1方）
        public const int C12 = 0x12, C22 = 0x22, C32 = 0x32, C42 = 0x42;
        // 3（4黑，3红，2梅，1方）
        public const int C13 = 0x13, C23 = 0x23, C33 = 0x33, C43 = 0x43;
        // 4（4黑，3红，2梅，1方）
        public const int C14 = 0x14, C24 = 0x24, C34 = 0x34, C44 = 0x44;
        // 5（4黑，3红，2梅，1方）
        public const int C15 = 0x15, C25 = 0x25, C35 = 0x35, C45 = 0x45;
        // 6（4黑，3红，2梅，1方）
        public const int C16 = 0x16, C26 = 0x26, C36 = 0x36, C46 = 0x46;
        // 7（4黑，3红，2梅，1方）
        public const int C17 = 0x17, C27 = 0x27, C37 = 0x37, C47 = 0x47;
        // 8（4黑，3红，2梅，1方）
        public const int C18 = 0x18, C28 = 0x28, C38 = 0x38, C48 = 0x48;
        // 9（4黑，3红，2梅，1方）
        public const int C19 = 0x19, C29 = 0x29, C39 = 0x39, C49 = 0x49;
        // 10(X)（4黑，3红，2梅，1方）
        public const int C1X = 0x1A, C2X = 0x2A, C3X = 0x3A, C4X = 0x4A;
        // J（4黑，3红，2梅，1方）
        public const int C1J = 0x1B, C2J = 0x2B, C3J = 0x3B, C4J = 0x4B;
        // Q（4黑，3红，2梅，1方）
        public const int C1Q = 0x1C, C2Q = 0x2C, C3Q = 0x3C, C4Q = 0x4C;
        // K（4黑，3红，2梅，1方）
        public const int C1K = 0x1D, C2K = 0x2D, C3K = 0x3D, C4K = 0x4D;
        // Joker（小王，大王）
        public const int Cjoker = 0x51, CJOKER = 0x52;

        public const int
        PK_joker = 0,
        PK_JOKER = 14,
        PK_A = 1,
        PK_2 = 2,
        PK_3 = 3,
        PK_4 = 4,
        PK_5 = 5,
        PK_6 = 6,
        PK_7 = 7,
        PK_8 = 8,
        PK_9 = 9,
        PK_10 = 10,
        PK_J = 11,
        PK_Q = 12,
        PK_K = 13;

        public static int[][] CARDS =
        {
            // 方块牌：♢A,2,3,4,5,6,7,8,9,10,J,Q,K
            new int[] { 0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18,0x19,0x1A,0x1B,0x1C,0x1D },
            // 梅花牌：♧ A,2,3,4,5,6,7,8,9,10,J,Q,K
            new int[] { 0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28,0x29,0x2A,0x2B,0x2C,0x2D },
            // 红桃牌：♥ A,2,3,4,5,6,7,8,9,10,J,Q,K
            new int[] { 0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38,0x39,0x3A,0x3B,0x3C,0x3D },
            // 黑桃牌：♤ A,2,3,4,5,6,7,8,9,10,J,Q,K
            new int[] { 0x41,0x42,0x43,0x44,0x45,0x46,0x47,0x48,0x49,0x4A,0x4B,0x4C,0x4D },
            // JOKER牌：小王，大王
            new int[] { 0x51,0x52 }
        };

        /// <summary> 牌的名字 </summary>
        public static string[][] NAMES ={
		new string[]{"♢ A","♢ 2","♢ 3","♢ 4","♢ 5","♢ 6","♢ 7","♢ 8","♢ 9","♢ 10","♢ J","♢ Q","♢ K"},// 方块
		new string[]{"♧ A","♧ 2","♧ 3","♧ 4","♧ 5","♧ 6","♧ 7","♧ 8","♧ 9","♧ 10","♧ J","♧ Q","♧ K"},// 梅花
		new string[]{"♥ A","♥ 2","♥ 3","♥ 4","♥ 5","♥ 6","♥ 7","♥ 8","♥ 9","♥ 10","♥ J","♥ Q","♥ K"},// 红桃
		new string[]{"♤ A","♤ 2","♤ 3","♤ 4","♤ 5","♤ 6","♤ 7","♤ 8","♤ 9","♤ 10","♤ J","♤ Q","♤ K"},// 黑桃
		new string[]{"小王","大王"}};

        public static int getCard(int type, int index)
        {
            return CARDS[type][index];
        }

        public static int getType(int id)
        {
            return (id >> 4) - 1;
        }

        public static int getValue(int id)
        {
            return id & 0xF;
        }

        public static int getIndex(int id)
        {
            return (id & 0xF) - 1;
        }

        public static string getName(int id)
        {
            if (id == INVISIBLE) { return "B"; }
            return NAMES[getType(id)][getIndex(id)];
        }

        public static int[] getCounts(int[] cards)
        {
            int[] array = new int[15];
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                v = getValue(cards[i]);
                if (getType(cards[i]) == TYPE_JOKER)
                {
                    if (v == 1) array[0] += 1;
                    else array[14] += 1;
                }
                else array[v] += 1;
            }
            return array;
        }

        public static int[] getEmptyCounts()
        {
            return new int[15];
        }

        public static int getSortValue(int card)
        {
            int value = getValue(card);
            if (getType(card) != TYPE_JOKER)
            {
                if (value == 1) value += 13;
                if (value == 2) value += 13;
            }
            else value += 15;
            value = (value << 6) | getType(card);
            return value;
        }

        public static int getCountIndex(int card)
        {
            if (card == 0x51) return 0;
            if (card == 0x52) return 14;
            return getValue(card);
        }

        /// <summary> 斗地主 用于单张牌 </summary>
        public static int DDZ_GetSortValueByCard(int card)
        {
            int value = getValue(card);
            if (getType(card) !=TYPE_JOKER)
            {
                if (value == 1) value += 13;
                if (value == 2) value += 13;
            }
            else value += 15;
            return value;
        }

        /// <summary> 跑得快 用于单张牌 </summary>
        public static int PDK_GetSortValueByCard(int card)
        {
            int value = getValue(card);
            if (getType(card) != TYPE_JOKER)
            {
                if (value == 1) value += 13;
                if (value == 2) value += 13;
            }
            else value += 15;
            return value;
        }
    }
}
