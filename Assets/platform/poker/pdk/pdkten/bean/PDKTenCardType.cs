using basef.rule;

namespace platform.poker
{


    public class PDKTenCardType
    {
        /// <summary> 确定PDKCardInfo的牌型和比较位置 </summary>
        public static void checkCardsType(PDKTenCardInfo info, Rule rule)
        {
            if (info == null || info.cards == null || info.cards.Length == 0)
            {
                return;
            }
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
                        else if (length >= 3 && isConnect(index, info, 1))
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
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                case 3:
                    {
                        if (length == 3)
                        {
                            if (index == 1)
                            {
                                info.type = PKCardType.TYPE_CARDS_BOMB;
                                info.index = 1;
                            }
                            else
                            {
                                info.type = PKCardType.TYPE_CARDS_THREE;
                                info.index = index;
                            }
                        }
                        else if (length % 3 == 0 && isConnect(index, info, 3))
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                case 4:
                    {
                        if (length == 4)
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
                    // 有4张的情况只有炸弹、四带二、四带三、飞机，相对总体较少
                    // if(counts[temp]==4) break;
                }
            }
            return temp;
        }

        /// <summary> 判断是否成顺：顺子，连对，三顺 </summary>
        public static bool isConnect(int index, PDKTenCardInfo info, int num)
        {
            if (info.counts[index] != num) return false;
            if (info.cards.Length <3 || info.cards.Length % num != 0) return false;
            if (info.counts[2] != 0) return false;
            if (index + info.cards.Length / num - 1 > 13 || (num == 1 && info.cards.Length > 12)) return false;
            if (index == 1)
            {
                for (int i = 0; i < info.cards.Length / num - 1; i++)
                {
                    if (info.counts[13 - i] != num) return false;
                }
            }
            else
            {
                for (int i = 1; i < info.cards.Length / num; i++)
                {
                    if (info.counts[index + i] != num) return false;
                }
            }
            return true;
        }

        /// <summary> 检测是否前后三组可以三顺 </summary>
        public static bool canPlane(int index, int[] counts)
        {
            bool a = counts[index - 2] == 3 && counts[index - 1] == 3;
            if (index < 5) a = false;
            bool b = counts[index - 1] == 3 && counts[index + 1] >= 3;
            if (index < 4) b = false;
            bool c = counts[index + 1] >= 3 && counts[index + 2] >= 3;
            if (a || b || c) return true;
            return false;
        }

        /// <summary> 判断后者牌型能否大过前者牌型(初步筛选) </summary>
        public static bool canPass(int type1, int type2)
        {
            if (type2 == PKCardType.TYPE_ERROR) return false;
            if (type1 == PKCardType.TYPE_ERROR) return true;
            if (type1 == PKCardType.TYPE_CARDS_BOMB && type2 != PKCardType.TYPE_CARDS_BOMB) return false;
            if (type2 == PKCardType.TYPE_CARDS_BOMB && type1 != PKCardType.TYPE_CARDS_BOMB) return true;
            if (type1 == type2) return true;
            return false;
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
