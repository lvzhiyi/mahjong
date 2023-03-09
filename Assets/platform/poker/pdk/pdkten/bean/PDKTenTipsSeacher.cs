namespace platform.poker
{
    public class PDKTenTipsSeacher
    {
        private static int[] SPECIALS = { 1, 2, 0, 14 };
        private PDKTenCardInfo cards;
        private int[] handcards;

        /// <summary> 查找位置 </summary>
        private int start;

        /// <summary> 查找长度 </summary>
        private int size;

        /// <summary> 每个数量 </summary>
        private int width;

        /// <summary> 手牌数数组 </summary>
        private int[] counts;

        /// <summary> 第几个提示 </summary>
        private int tipIndex;

        public PDKTenTipsSeacher()
        {
        }

         /// <summary> 通过一组牌来获取这组牌中的顺子() </summary>
        public static int[] getCardsByTip(int[] cards)
        {
            if (cards == null || cards.Length == 0) return null;
            PDKTenCardInfo info = new PDKTenCardInfo(0, cards);
            if (info.type != PKCardType.TYPE_ERROR) return null;
            int start = Poker.PDK_GetSortValueByCard(cards[cards.Length - 1]);// 取得最小牌值
            int end = Poker.PDK_GetSortValueByCard(cards[0]);// 取得最大牌值
            if (end > 14) return null;
            int size = end - start + 1;// 取得长度
            if (size > 12 || size < 3 || cards.Length < size) return null;// 验证长度
            int point = end - 1;// 定义一个指针，指向值为最大牌-1的值
            int[] outCards = new int[size];// 定义返回数组
            for (int i = 1, j = 1; i < cards.Length - 1 && j < size - 1; i++)
            {
                int value = Poker.PDK_GetSortValueByCard(cards[i]);
                if (value == point)// 如果当前牌的值和指针的值相等，那么获取这张牌并且指针指向下一个值
                {
                    outCards[j++] = cards[i];
                    point--;
                }
            }
            if (point == start)
            {
                outCards[0] = cards[0];
                outCards[size - 1] = cards[cards.Length - 1];
                return outCards;// 如果指针已经指向最小值，那么返回数组
            }
            return null;// 否则返回null;
        }

        public void reset(PDKTenCardInfo cards, int[] handcarads)
        {
            PDKTenHintShowHandProcess.num = 1;
            this.handcards = handcarads;
            this.cards = cards;
            this.start = cards.level + 1;
            this.counts = Poker.getEmptyCounts();
            for (int i = 0, v = 0; i < handcarads.Length; i++)
            {
                v = Poker.getValue(handcarads[i]);
                if (Poker.getType(handcarads[i]) == Poker.TYPE_JOKER)
                {
                    if (v == 1)
                        counts[0] += 1;
                    else
                        counts[14] += 1;
                }
                else
                    counts[v] += 1;
            }
            switch (cards.type)
            {
                case PKCardType.TYPE_CARDS_SINGLE:
                    this.width = 1;
                    this.size = 1;
                    break;
                case PKCardType.TYPE_CARDS_DOUBLE:
                    this.width = 2;
                    this.size = 1;
                    break;
                case PKCardType.TYPE_CARDS_THREE:
                    this.width = 3;
                    this.size = 1;
                    break;
                case PKCardType.TYPE_CARDS_BOMB:
                    this.width = 4;
                    this.size = 1;
                    break;
                case PKCardType.TYPE_CARDS_CONNECT:
                    this.width = 1;
                    this.size = cards.cards.Length;
                    break;
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT:
                    this.width = 2;
                    this.size = cards.cards.Length / width;
                    break;
                case PKCardType.TYPE_CARDS_THREE_CONNECT:
                    this.width = 3;
                    this.size = cards.cards.Length / (width);
                    break;
                default:
                    break;

            }
        }

        public int[] getTip(int index)
        {
            this.tipIndex = index;
            if (handcards.Length < cards.cards.Length)
            {
                int _start = start, _width = width;
                width = 4;
                start = 3;
                int[] arr = checkOutZha();
                start = _start;
                width = _width;
                return arr;
            }
            switch (cards.type)
            {
                case PKCardType.TYPE_CARDS_SINGLE:
                case PKCardType.TYPE_CARDS_DOUBLE:
                case PKCardType.TYPE_CARDS_THREE:
                    {
                        index = checkOutSingle();
                        int[] arr = null;
                        if (index != -1)
                        {
                            int[] temp = (int[])handcards.Clone();
                            arr = new int[width];
                            int j = 0;
                            for (int i = 0; i < temp.Length && j < width; i++)
                            {
                                if (index == Poker.getCountIndex(temp[i]))
                                {
                                    arr[j++] = temp[i];
                                    temp[i] = -1;
                                }
                            }

                            return arr;
                        }
                        int _start = start, _width = width;
                        // 找普通炸弹
                        start = 3;
                        width = 4;
                        arr = checkOutZha();
                        start = _start;
                        width = _width;
                        return arr;
                    }
                case PKCardType.TYPE_CARDS_BOMB:
                    return checkOutZha();

                case PKCardType.TYPE_CARDS_CONNECT:
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT:
                case PKCardType.TYPE_CARDS_THREE_CONNECT:
                    {
                        index = checkOutSeries();
                        int[] arr = null;
                        if (index != -1)
                        {
                            int[] temp = (int[])handcards.Clone();
                            int len = (width) * size;
                            arr = new int[len];
                            int k = 0;
                            for (int i = 0; i < size; i++, index++)
                            {
                                index = index == 14 ? 1 : index;
                                for (int j = 0, f = 0; j < temp.Length && f < width; j++)
                                {
                                    if (Poker.getCountIndex(temp[j]) == index)
                                    {
                                        arr[k++] = temp[j];
                                        temp[j] = -1;
                                        f++;
                                    }
                                }
                            }
                            return arr;
                        }
                        int _start = start, _width = width;
                        width = 4;
                        start = 3;
                        arr = checkOutZha();
                        start = _start;
                        width = _width;
                        return arr;
                    }

                default:
                    return null;
            }
        }
        /// <summary> 找炸弹(所有炸弹) </summary>
        private int[] checkOutZha()
        {
            int index = checkOutSingle();
            int[] arr = null;
            if (index != -1)
            {
                index = index == 14 ? 0 : index - 1;
                arr = new int[4];
                arr[0] = Poker.getCard(0, index);
                arr[1] = Poker.getCard(1, index);
                arr[2] = Poker.getCard(2, index);
                arr[3] = Poker.getCard(3, index);
                return arr;
            }
            if (Room.room.getRule().getRuleAtribute("aaaisboom")) arr = checkOutAAA();// 找三个A
            return arr;
        }

        /** 找最大炸弹 */
        private int[] checkOutAAA()
        {
            int t_width = width, t_start = start;
            width = 3;
            start = 14;
            int index = checkOutSingle();
            int[] arr = null;
            if (index != -1)
            {
                int[] temp = (int[])handcards.Clone();
                arr = new int[width];
                int j = 0;
                for (int i = 0; i < temp.Length && j < width; i++)
                {
                    if (index == Poker.getCountIndex(temp[i]))
                    {
                        arr[j++] = temp[i];
                        temp[i] = -1;
                    }
                }
            }
            width = t_width;
            start = t_start;
            return arr;
        }

        /// <summary> 检查单种主体牌：例如单张，对子，三不带，三带一，三带二，四带二，四带二对 </summary>
        private int checkOutSingle()
        {
            int m = start;
            if (m > 17 && m < 3) return -1;
            for (int w = width; w < 5; w++)
            {
                if (m > 2 && m < 14)// 3~K
                {
                    for (; m < 14; m++)
                    {
                        if (counts[m] == w && --tipIndex == 0) goto search;
                    }
                }
                if (tipIndex != 0)
                {
                    m -= 14;
                    int i = 0;
                    for (; m < 2; m++)// A,2,joker,JOKER
                    {
                        i = SPECIALS[m];
                        if (counts[i] == w && --tipIndex == 0)
                        {
                            m = i;
                            goto search;
                        }
                    }
                    i = SPECIALS[2];
                    int j = SPECIALS[3];
                    if (width == 1)
                    {
                        if (w == 1)
                        {
                            m = -1;
                            if (counts[i] > 0 && counts[j] == 0 && --tipIndex == 0)
                                m = i;
                            else if (counts[i] == 0 && counts[j] > 0 && --tipIndex == 0) m = j;
                            if (m != -1) goto search;
                        }
                        else if (w == 4)
                        {
                            m = -1;
                            if (counts[i] > 0 && --tipIndex == 0)
                                m = i;
                            else if (counts[j] > 0 && --tipIndex == 0) m = j;
                            if (m != -1) goto search;
                        }
                    }
                }
                m = start;// 重置m
            }
        search:
            if (tipIndex == 0) return m;
            return -1;
        }
        /// <summary> 检查多个连续主牌：顺子，连对，飞机，连炸 </summary>
        private int checkOutSeries()
        {
            int end = (14 - size) + 2;// size>1
            int m = start, n = start + size - 1;
            for (; m < end; m++, n++)
            {
                {
                    for (int j = n; j >= m; --j)
                    {
                        int index = j == 14 ? 1 : j;
                        if (counts[index] < width)
                        {
                            m = j;
                            n = m + size - 1;
                            goto check;
                        }
                    }
                    if (--tipIndex == 0) break;
                }
            check:;
            }
            if (tipIndex == 0) return m;
            return -1;
        }
    }
}
