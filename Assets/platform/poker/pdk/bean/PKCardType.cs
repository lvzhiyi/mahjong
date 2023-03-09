using basef.rule;
using cambrian.common;

namespace platform.poker
{
    public class PKCardType: BytesSerializable
    {
        /// <summary> 错误类型 </summary>
        public const int TYPE_ERROR = 0;

        /// <summary> 单牌 </summary>
        public const int TYPE_CARDS_SINGLE = 1;

        /// <summary> 对子 </summary>
        public const int TYPE_CARDS_DOUBLE = 2;

        /// <summary> 三不带 </summary>
        public const int TYPE_CARDS_THREE = 3;

        /// <summary> 三带一 </summary>
        public const int TYPE_3_1 = 4;

        /// <summary> 三带对 </summary>
        public const int TYPE_3_2 = 5;

        /// <summary> 单顺，普通顺子 </summary>
        public const int TYPE_CARDS_CONNECT = 6;

        /// <summary> 2连顺，连对 </summary>
        public const int TYPE_CARDS_DOUBLE_CONNECT = 7;

        /// <summary> 飞机-三不带，三连顺 </summary>
        public const int TYPE_CARDS_THREE_CONNECT = 8;

        /// <summary> 飞机-三带一 </summary>
        public const int TYPE_FEIJI_1 = 9;

        /// <summary> 飞机-三带对 </summary>
        public const int TYPE_FEIJI_2 = 10;

        /// <summary> 四带一 </summary>
        public const int TYPE_4_1 = 11;

        /// <summary> 四带二 </summary>
        public const int TYPE_4_1_1 = 12;

        /// <summary> 四带两对 </summary>
        public const int TYPE_4_2_2 = 13;

        /// <summary> 4张相同牌 </summary>
        public const int TYPE_CARDS_BOMB = 14;

        /// <summary> 王炸 </summary>
        public const int TYPE_ZHA_WANG = 15;

        /// <summary> 连炸 </summary>
        public const int TYPE_ZHA_LIAN = 16;

        /// <summary> 四带三 </summary>
        public const int TYPE_4_1_1_1 = 17;

        /// <summary> 飞机三带二 </summary>
        public const int TYPE_CARDS_PLANE = 18;

        /// <summary>四张(安岳跑的快)</summary>
        public const int TYPE_CARDS_FOUR = 24;
    }

    public class PDKCardType
    {
        /// <summary> 确定PDKCardInfo的牌型和比较位置 </summary>
        public static void checkCardsType(PDKCardInfo info, Rule rule)
        {
            if (info == null || info.cards == null || info.cards.Length == 0)
            {
                return;
            }
            int index = getCompareIndex(info.counts); // 第一个最多张数的牌位置
            switch (info.cards.Length) // 通过牌组长度分组
            {
                case 1:
                {
                    info.type = PKCardType.TYPE_CARDS_SINGLE;
                    info.index = index;
                    break;
                }
                case 2:
                {
                    if (info.counts[index] == 2) // 33
                    {
                        info.type = PKCardType.TYPE_CARDS_DOUBLE;
                        info.index = index;
                    }
                    break;
                }
                case 3:
                {
                    if (rule.getRuleAtribute("aaaisboom") && info.counts[1] == 3)// AAA
                    {
                        info.type = PKCardType.TYPE_CARDS_BOMB;
                        info.index = 1;
                        break;
                    }
                    if (info.counts[index] == 3)// 333
                    {
                        info.type = PKCardType.TYPE_CARDS_THREE;
                        info.index = index;
                    }
                    break;
                }
                case 4:
                {                                                                                             
                    if (info.counts[index] == rule.getIntAtribute("Boom_Count"))// 3333
                    {                           
                        info.type = PKCardType.TYPE_CARDS_BOMB;
                        info.index = index;
                        break;
                    }
                    if (info.counts[index] == 3 && rule.getRuleAtribute("San_Dai_Yi_Dan"))//三带一张
                    {
                        info.index = index;
                        info.type = PKCardType.TYPE_3_1;
                        break;
                    }
                    if (info.counts[index] == 2)// 3344
                    {
                        if (isConnect(index, info, 2))
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1)
                    {
                        if (rule.getIntAtribute("Shun_Zi_Count") != 4) break;
                        if (isConnect(index, info, 1))
                        {
                            info.type = PKCardType.TYPE_CARDS_CONNECT;
                            info.index = index;
                        }
                    }
                    break;
                }
                case 5:
                {
                    if (info.counts[index] == 3 || info.counts[index] == 4)// 33345或者33334
                    {
                        info.type = PKCardType.TYPE_3_2;
                        info.index = index;
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 6:
                {
                    if (info.counts[index] == 4)
                    {
                        if (rule.getRuleAtribute("Si_Dai_Er_Dan"))// 333344 4带2单
                        {
                            info.type = PKCardType.TYPE_4_1_1;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 3)
                    {
                        if (isConnect(index, info, 3))// 333444
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 2)
                    {
                        if (isConnect(index, info, 2))
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 7:
                {
                    if (info.counts[index] == 4)
                    {
                        if (rule.getRuleAtribute("Si_Dai_San_Dan"))// 3333456              
                        {
                            info.type = PKCardType.TYPE_4_1_1_1;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 8:
                {
                    if (info.counts[index] == 2)
                    {
                        if (isConnect(index, info, 2))
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 9:
                {
                    if (info.counts[index] == 3)
                    {
                        if (isConnect(index, info, 3))
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 10:
                {
                    if (info.counts[index] == 4 || info.counts[index] == 3)
                    {
                        isPlane(index, info);
                        break;
                    }
                    if (info.counts[index] == 2)
                    {
                        if (isConnect(index, info, 2))
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 11:
                {
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 12:
                {
                    if (info.counts[index] == 3)
                    {
                        if (isConnect(index, info, 3))
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 2)
                    {
                        if (isConnect(index, info, 2))
                        {
                            info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                            info.index = index;
                        }
                        break;
                    }
                    if (info.counts[index] == 1 && isConnect(index, info, 1))
                    {
                        info.type = PKCardType.TYPE_CARDS_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 13:
                {
                    break;
                }
                case 14:
                {
                    if (info.counts[index] == 2 && isConnect(index, info, 2))
                    {
                        info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                case 15:
                {
                    if (info.counts[index] == 4)
                    {
                        isPlane(index, info);
                        break;
                    }
                    if (info.counts[index] == 3)
                    {
                        if (isConnect(index, info, 3))
                        {
                            info.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                            info.index = index;
                            break;
                        }
                        isPlane(index, info);
                    }
                    break;
                }
                case 16:
                {
                    if (info.counts[index] == 2 && isConnect(index, info, 2))
                    {
                        info.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                        info.index = index;
                    }
                    break;
                }
                default:
                {
                    break;
                }
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
        public static bool isConnect(int index, PDKCardInfo info, int num)
        {
            if (info.counts[index] != num) return false;
            if (info.cards.Length < 4 || info.cards.Length % num != 0) return false;
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

        /// <summary> 判断是否为飞机 :由于飞机比较位置比较特殊，故方法里确定比较位置 </summary>
        public static bool isPlane(int index, PDKCardInfo info)
        {
            if (info.counts[index] < 3) return false;
            if (info.cards.Length < 10 || info.cards.Length % 5 != 0) return false;
            if (info.cards.Length == 10)
            {
                if (index == 13)
                {
                    if (info.counts[1] == 3 || info.counts[12] == 3)
                    {
                        info.type = PKCardType.TYPE_CARDS_PLANE;
                        info.index = index;
                        return true;
                    }
                    else
                    {
                        int[] counts = info.counts;
                        counts[13] = 0;
                        int address = getCompareIndex(counts);
                        if (address < 3) return false;
                        if (counts[address] < 3) return false;
                        if (address + info.cards.Length / 5 - 1 > 13) return false;
                        if (counts[address] + 1 == 3)
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = address;
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    if (index == 1)
                    {
                        if (info.counts[13] >= 3)
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = index;
                            return true;
                        }
                        int[] counts = info.counts;
                        counts[index] = 0;
                        int address = getCompareIndex(counts);
                        if (address == 2) return false;
                        if (counts[address] < 3) return false;
                        if (address + info.cards.Length / 5 - 1 > 13) return false;
                        if (counts[address] + 1 == 3)
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = address;
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        if ((index > 2 && info.counts[index + 1] >= 3) || (index > 3) && info.counts[index - 1] == 3)
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = index;
                            return true;
                        }
                        else
                        {
                            int[] counts = info.counts;
                            counts[index] = 0;
                            int address = getCompareIndex(counts);
                            if (address == 2) return false;
                            if (counts[address] < 3) return false;
                            if (address + info.cards.Length / 5 - 1 > 13) return false;
                            if (counts[address + 1] == 3)
                            {
                                info.type = PKCardType.TYPE_CARDS_PLANE;
                                info.index = address;
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            if (info.cards.Length == 15)
            {
                if (index == 13)
                {
                    bool a = info.counts[11] == 3 && info.counts[12] == 3;
                    bool b = info.counts[1] == 3 && info.counts[12] == 3;
                    if (a || b)
                    {
                        info.type = PKCardType.TYPE_CARDS_PLANE;
                        info.index = index;
                        return true;
                    }
                    else
                    {
                        int[] counts = info.counts;
                        counts[index] = 0;
                        int address = getCompareIndex(counts);
                        if (address < 3) return false;
                        if (counts[address] < 3) return false;
                        if (address + info.cards.Length / 5 - 1 > 13) return false;
                        if (canPlane(address, counts))
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = address;
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    if (index < 3)
                    {
                        if (index == 1 && info.counts[12] >= 3 && info.counts[13] >= 3)
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = index;
                            return true;
                        }
                        int[] counts = info.counts;
                        counts[index] = 0;
                        int address = getCompareIndex(counts);
                        if (counts[address] < 3) return false;
                        if (address < 3)
                        {
                            if (info.counts[index] == 4 || info.counts[address] == 4) return false;
                            int[] counts2 = counts;
                            counts2[address] = 0;
                            int address2 = getCompareIndex(counts2);
                            if (counts2[address2] != 3) return false;
                            if (address2 < 3) return false;
                            if (address2 + info.cards.Length / 5 - 1 > 13) return false;
                            if (canPlane(address2, counts2))
                            {
                                info.type = PKCardType.TYPE_CARDS_PLANE;
                                info.index = address2;
                                return true;
                            }
                            return false;
                        }
                        if (address + info.cards.Length / 5 - 1 > 13) return false;
                        if (canPlane(address, counts))
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = address;
                            return true;
                        }
                        else
                        {
                            if (info.counts[index] == 4 || info.counts[address] == 4) return false;
                            int[] counts2 = counts;
                            counts2[address] = 0;
                            int address2 = getCompareIndex(counts2);
                            if (counts2[address2] != 3) return false;
                            if (address2 < 3) return false;
                            if (address2 + info.cards.Length / 5 - 1 > 13) return false;
                            if (canPlane(address2, counts2))
                            {
                                info.type = PKCardType.TYPE_CARDS_PLANE;
                                info.index = address2;
                                return true;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        if (canPlane(index, info.counts))
                        {
                            info.type = PKCardType.TYPE_CARDS_PLANE;
                            info.index = index;
                            return true;
                        }
                        else
                        {
                            int[] counts = info.counts;
                            counts[index] = 0;
                            int address = getCompareIndex(counts);
                            if (address < 3) return false;
                            if (counts[address] < 3) return false;
                            if (address + info.cards.Length / 5 - 1 > 13) return false;
                            if (canPlane(address, counts))
                            {
                                info.type = PKCardType.TYPE_CARDS_PLANE;
                                info.index = address;
                                return true;
                            }
                            else
                            {
                                if (info.counts[index] == 4 || info.counts[address] == 4) return false;
                                int[] counts2 = counts;
                                counts2[index] = 0;
                                int address2 = getCompareIndex(counts2);
                                if (address2 < 3) return false;
                                if (counts2[address2] != 3) return false;
                                if (address2 + info.cards.Length / 5 - 1 > 13) return false;
                                if (canPlane(address2, counts2))
                                {
                                    info.type = PKCardType.TYPE_CARDS_PLANE;
                                    info.index = address2;
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
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
