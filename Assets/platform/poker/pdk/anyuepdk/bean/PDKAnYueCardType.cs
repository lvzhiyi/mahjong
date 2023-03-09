using basef.rule;

namespace platform.poker
{
    public class PDKAnYueCardType
    {
        /// <summary> 确定PDKCardInfo的牌型和比较位置 </summary>
        public static void checkCardsType(PDKAnYueCardInfo info, Rule rule)
        {
            if (info == null || info.cards == null || info.cards.Length == 0) return;
            int index = getCompareIndex(info.counts); // 第一个最多张数的牌位置
            int count = info.counts[index];// 最多的张数
            int length = info.cards.Length;// 牌组长度
            switch (count)
            {
                case 1:
                    {
                        if (length == 1)
                        {
                            info.type = PKCardType.TYPE_CARDS_SINGLE;
                            info.index = index;
                        }
                        else if (length >=rule.getIntAtribute("Shun_Zi_Count") && isConnect(index, info, 1))
                        {
                            info.type = PKCardType.TYPE_CARDS_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                case 2:
                    {
                        if (length == 2)
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE;
                            info.index = index;
                        }
                        else if ((length & 1) == 0 && isConnect(index, info, 2))
                        {
                            if (!rule.getRuleAtribute("canDoubleConnect")) break;
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                case 3:
                    {
                        if (length == 3)
                        {
                            if (index == 1&&rule.getRuleAtribute("aaaisboom"))
                            {
                                info.type = PKCardType.TYPE_CARDS_BOMB;
                                info.index = index;
                            }
                                
                        }
                        else if (length == 4)
                        {
                            if (!rule.getRuleAtribute("can31")) break;
                            info.type = PKCardType.TYPE_3_1;
                            info.index = index;
                        }
                        else if (length == 5)
                        {
                            if (!rule.getRuleAtribute("can32")) break;
                            info.type = PKCardType.TYPE_3_2;
                            info.index = index;
                        }
                        else if (length % 3 == 0 && isConnect(index, info, 3))
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                        }
                        else if (length % 4 == 0 || length % 5 == 0)
                        {
                            checkPlane(info, index);
                        }
                        break;
                    }
                case 4:
                    {
                        if (length == 4)
                        {
                            info.type = PKCardType.TYPE_CARDS_FOUR;
                            info.index = index;
                        }
                        if (length == 5)
                        {
                            info.type = PKCardType.TYPE_CARDS_BOMB;
                            info.index = index;
                        }
                        break;
                    }
                default:
                    break;
            }

        }

        /// <summary> 获取counts数组中最大值的位置(第一个最大值) </summary>
        public static int getCompareIndex(int[] counts)
        {
            int temp = 0;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] > counts[temp])
                {
                    temp = i;
                }
            }
            return temp;
        }

        /// <summary> 判断是否成顺：顺子，连对，三顺 </summary>
        public static bool isConnect(int index, PDKAnYueCardInfo info, int num)
        {
            if (info.counts[index] != num) return false;
            if (info.cards.Length % num != 0) return false;
            if (info.counts[2] != 0) return false;
            if (index + info.cards.Length / num - 1 > 13) return false;
            if (index == 1)
            {
                int length = 14 - info.cards.Length / num;
                for (int i = 13; i > length; i--)
                {
                    if (info.counts[i] != num) return false;
                }
            }
            else
            {
                int length = index + info.cards.Length / num;
                for (int i = index + 1; i < length; i++)
                {
                    if (info.counts[i] != num) return false;
                }
            }
            return true;
        }

        /** 判断飞机 */
        public static void checkPlane(PDKAnYueCardInfo info, int index)
        {
            int length = info.cards.Length;
            if (length % 4 != 0 && length % 5 != 0) return;
            if (info.counts[index] < 3) return;
            int len = length % 4 == 0 ? 4 : 5;
            if (checkPlaneA(info, length / len))// 单独判断A
            {
                info.index = 1;
                info.type = PKCardType.TYPE_CARDS_PLANE;
                return;
            }
            int temp = checkPlaneNomal(info, 3, length / len);// 从3开始找，长度为牌组长度/len
            if (temp != -1)
            {
                info.index = temp;
                info.type = PKCardType.TYPE_CARDS_PLANE;
            }
        }
        /** 判断飞机(包含A) */
        public static bool checkPlaneA(PDKAnYueCardInfo info, int length)
        {
            if (info.counts[1] >= 3)
            {
                bool b = true;
                int len = 14 - length;
                for (int i = 13; i > len; i--)
                {
                    if (info.counts[i] < 3)
                    {
                        b = false;
                        break;
                    }
                }
                return b;
            }
            return false;
        }

        /** 判断飞机(3-k) */
        public static int checkPlaneNomal(PDKAnYueCardInfo info, int index, int length)
        {
            if (index + length - 1 > 13) return -1;
            int len = index + length;
            for (int i = index; i < len; i++)
            {
                if (info.counts[i] < 3) return checkPlaneNomal(info, i + 1, length);
            }
            return index;
        }

        /** 判断后者牌型能否大过前者牌型(初步筛选) */
        public static bool canPass(int type1, int type2)
        {
            if (type2 == PKCardType.TYPE_ERROR) return false;
            if (type1 == PKCardType.TYPE_ERROR) return true;
            if (type1 == PKCardType.TYPE_CARDS_BOMB && type2 != PKCardType.TYPE_CARDS_BOMB) return false;
            if (type2 == PKCardType.TYPE_CARDS_BOMB && type1 != PKCardType.TYPE_CARDS_BOMB) return true;
            if (type1 == type2) return true;
            return false;
        }

        /** 用于单张牌 */
        public static int getSortValueByCard(int card)
        {
            int value = Poker.getValue(card);
            if (Poker.getType(card) == Poker.TYPE_JOKER)
            {
                value += 15;
            }
            else
            {
                if (value == 1) value += 13;
                if (value == 2) value += 13;
            }
            return value;
        }

        /// <summary> 获取牌型名字 </summary>
        public static string getTypeName(int type)
        {
            switch (type)
            {
                case PKCardType.TYPE_ERROR:
                    return "无";
                case PKCardType.TYPE_CARDS_SINGLE:
                    return "单张";
                case PKCardType.TYPE_CARDS_DOUBLE:
                    return "对子";
                case PKCardType.TYPE_CARDS_THREE:
                    return "三不带";
                case PKCardType.TYPE_CARDS_CONNECT:
                    return "顺子";
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT:
                    return "连对";
                case PKCardType.TYPE_CARDS_THREE_CONNECT:
                    return "三顺";
                case PKCardType.TYPE_3_1:
                    return "三带一";
                case PKCardType.TYPE_3_2:
                    return "三带二";
                case PKCardType.TYPE_4_1_1:
                    return "四带二";
                case PKCardType.TYPE_4_1_1_1:
                    return "四带三";
                case PKCardType.TYPE_CARDS_PLANE:
                    return "飞机";
                case PKCardType.TYPE_CARDS_BOMB:
                    return "炸弹";
                default:
                    return "无法识别";
            }
        }
    }
}
