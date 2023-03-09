using basef.rule;

namespace platform.poker
{
    public class DDZCardType
    {
        #region 规则设置

        public static bool Si_Dai_Er_Dui = true,                //四带二对
                           Si_Dai_Er_Dan = true,                //四带二单
                           Si_Dai_Yi_Dan = true,                //四带一单
                           San_Dai_Yi_Dui = true,               //三带一对
                           San_Dai_Yi_Dan = true,               //三带一单
                           San_Bu_Dai = true,                   //三不带
                           Fei_Ji_San_Bu_Dai = true,            //飞机三不带 
                           Fei_Ji_San_Dai_Dan = true,           //飞机三带单
                           Fei_Ji_San_Dai_Dui = true,           //飞机三带对
                           Lian_Zha = true,                     //连炸
                           Shun_Zi = true,                      //顺子
                           Lian_Dui = true,                     //连对
                           Boom = true;                         //炸弹

        public static int Shun_Zi_Count,                 //顺子长度
                          Boom_Count;                    //炸弹长度

        /// <summary> 规则设置 </summary>
        public static void CardRuleSet(Rule rule)
        {
            CardRuleReset();

            Si_Dai_Er_Dui = rule.getRuleAtribute("Si_Dai_Er_Dui");
            Si_Dai_Er_Dan = rule.getRuleAtribute("Si_Dai_Er_Dan");
            Si_Dai_Yi_Dan = rule.getRuleAtribute("Si_Dai_Yi_Dan");
            San_Dai_Yi_Dui = rule.getRuleAtribute("San_Dai_Yi_Dui");
            San_Dai_Yi_Dan = rule.getRuleAtribute("San_Dai_Yi_Dan");
            San_Bu_Dai = rule.getRuleAtribute("San_Bu_Dai");
            Fei_Ji_San_Bu_Dai = rule.getRuleAtribute("Fei_Ji_San_Bu_Dai");
            Fei_Ji_San_Dai_Dan = rule.getRuleAtribute("Fei_Ji_San_Dai_Dan");
            Fei_Ji_San_Dai_Dui = rule.getRuleAtribute("Fei_Ji_San_Dai_Dui");
            Lian_Zha = rule.getRuleAtribute("Lian_Zha");
            Shun_Zi = rule.getRuleAtribute("Shun_Zi");
            Lian_Dui = rule.getRuleAtribute("Lian_Dui");
            Boom = rule.getRuleAtribute("Boom");

            Shun_Zi_Count = rule.getIntAtribute("Shun_Zi_Count");
            Boom_Count = rule.getIntAtribute("Boom_Count");
        }

        /// <summary> 规则重置 </summary>
        public static void CardRuleReset()
        {
            Si_Dai_Er_Dui
                = Si_Dai_Er_Dan
                = Si_Dai_Yi_Dan
                = San_Dai_Yi_Dui
                = San_Dai_Yi_Dan
                = San_Bu_Dai
                = Fei_Ji_San_Bu_Dai
                = Fei_Ji_San_Dai_Dan
                = Fei_Ji_San_Dai_Dui
                = Lian_Zha
                = Shun_Zi
                = Lian_Dui
                = Boom = false;

            Shun_Zi_Count = 5;
            Boom_Count = 4;
        }

        #endregion

        #region 其他方法

        /// <summary> 用于counts数组 </summary>
        public static int getSortValue(int index)
        {
            switch (index)
            {
                case 0: return 16;
                case 1: return 14;
                case 2: return 15;
                case 14: return 17;
                default: return index;
            }
        }

        /// <summary> 用于单张牌 </summary>
        public static int getSortValueByCard(int card)
        {
            int value = Poker.getValue(card);
            if (Poker.getType(card) != Poker.TYPE_JOKER)
            {
                if (value == 1) value += 13;
                if (value == 2) value += 13;
            }
            else value += 15;
            return value;
        }

        /// <summary> 获取牌型名称 </summary>
        public static string getCardTypeName(int type)
        {
            switch (type)
            {
                case PKCardType.TYPE_CARDS_SINGLE: return "单牌";
                case PKCardType.TYPE_CARDS_DOUBLE: return "对子";
                case PKCardType.TYPE_CARDS_THREE: return "三不带";
                case PKCardType.TYPE_3_1: return "三带一单";
                case PKCardType.TYPE_3_2: return "三带一对";
                case PKCardType.TYPE_CARDS_CONNECT: return "顺子";
                case PKCardType.TYPE_CARDS_DOUBLE_CONNECT: return "连对";
                case PKCardType.TYPE_CARDS_THREE_CONNECT: return "飞机-三不带";
                case PKCardType.TYPE_FEIJI_1: return "飞机-三带一";
                case PKCardType.TYPE_FEIJI_2: return "飞机-三带对";
                case PKCardType.TYPE_4_1: return "四带一单";
                case PKCardType.TYPE_4_1_1: return "四带二单";
                case PKCardType.TYPE_4_2_2: return "四带两对";
                case PKCardType.TYPE_CARDS_BOMB: return "炸弹";
                case PKCardType.TYPE_ZHA_LIAN: return "连炸";
                case PKCardType.TYPE_ZHA_WANG: return "王炸";
                case PKCardType.TYPE_ERROR:
                default: return "不成型";
            }
        }

        /// <summary> 递归顺序消减牌，当到达边界或牌数消减完时终止，返回剩余未消减的牌数 </summary>
        private static int eliminate(int[] counts, int index, int size, int value, bool isFromRight)
        {
            if (isFromRight)
            {
                if (counts[index] != value) return size;// 快速失败
                size -= value;
                if (size == 0 || index == 1) return size;// A边界，没有比A大的
                if (index == 13)//
                    size = eliminate(counts, 1, size, value, isFromRight);
                else
                    size = eliminate(counts, index + 1, size, value, isFromRight);
            }
            else
            {
                if (counts[index] != value) return size;// 快速失败
                size -= value;
                if (size == 0 || index == 3) return size;// 3边界，没有比3小的
                if (index == 1) // A的下一个是K
                    size = eliminate(counts, 13, size, value, isFromRight);
                else
                    size = eliminate(counts, index - 1, size, value, isFromRight);
            }
            return size;
        }

        #endregion

        #region 获得一组指定牌的类型

        /// <summary> 获得一组指定牌的类型 </summary>
        public static DDZCardInfo checkTypeAndLevel(DDZCardInfo info)
        {
            switch (info.getCards().Length)
            {
                case 1:// 单张
                {
                    return checkType_Size_1(info);
                }

                case 2:// 对子，王炸
                {
                    return checkType_Size_2(info);
                }

                case 3:// 三不带
                {
                    return checkType_Size_3(info);
                }

                case 4:// 炸弹 三带一
                {
                    return checkType_Size_4(info);
                }

                case 5:// 三带对 顺子 四带一单
                {
                    return checkType_Size_5(info);
                }

                case 6:// 顺子 连对 三不带飞机 四带二
                {
                    return checkType_Size_6(info);
                }

                case 7:// 顺子
                {
                    return checkType_Size_7(info);
                }

                case 8:// 顺  四带二对  连对  飞机三带一  连炸
                {
                    return checkType_Size_8(info);
                }

                case 9:// 顺子 飞机三不带
                {
                    return checkType_Size_9(info);
                }

                case 10:// 顺子 飞机三带对 连对
                {
                    return checkType_Size_10(info);
                }

                case 11:// 顺子
                {
                    return checkType_Size_11(info);
                }

                case 12:// 顺子,连对，飞机三不带，飞机三带一，连炸
                {
                    return checkType_Size_12(info);
                }

                case 14:// 连对
                {
                    return checkType_Size_14(info);
                }

                case 15:// 飞机三不带 飞机三带对
                {
                    return checkType_Size_15(info);
                }

                case 16:// 连对 飞机三带一,连炸
                {
                    return checkType_Size_16(info);
                }

                case 18:// 连对  飞机三不带
                {
                    return checkType_Size_18(info);
                }

                case 20:// 连对  飞机三带一  飞机三带二  连炸
                {
                    return checkType_Size_20(info);
                }

                case 13: // 3-A 只有12张, 连对为偶数, 13为质数.
                {
                    return checkType_Size_13(info);
                }

                case 17: // 3-A 只有12张, 连对为偶数, 17为质数.
                {
                    return checkType_Size_17(info);
                }

                case 19: // 3-A 只有12张, 连对为偶数, 19为质数.
                {
                    return checkType_Size_19(info);
                }

                default: info.type = PKCardType.TYPE_ERROR; break;
            }
            return info;
        }

        #region 根据牌的数量判断牌型

        protected static DDZCardInfo checkType_Size_1(DDZCardInfo cards)
        {
            cards.level = getSortValueByCard(cards.getCards()[0]);
            cards.type = PKCardType.TYPE_CARDS_SINGLE;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_2(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;

            cards.level = isDuizi(counts, size);
            if (cards.level != -1)
            {
                cards.type = PKCardType.TYPE_CARDS_DOUBLE;
                return cards;
            }
            cards.level = isWangzha(counts, size);
            if (cards.level != -1)
            {
                cards.type = PKCardType.TYPE_ZHA_WANG;
                return cards;
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_3(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;
            if (San_Bu_Dai)
            {
                cards.level = isSanbudai(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_4(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;
            if (Boom)
            {
                cards.level = isZhadan(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_BOMB;
                    return cards;
                }
            }
            if (San_Dai_Yi_Dan)
            {
                cards.level = isSandaiyi(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_3_1;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_5(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;

            if (San_Dai_Yi_Dui)
            {
                cards.level = isSandaidui(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_3_2;
                    return cards;
                }
            }
            int index = Poker.getValue(cards.getCards()[0]);
            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Si_Dai_Yi_Dan)
            {
                cards.level = is41(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_4_1;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_6(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;
            if (Si_Dai_Er_Dui)
            {// 四带二
                cards.level = is411(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_4_1_1;
                    return cards;
                }
            }
            int index = Poker.getValue(cards.getCards()[0]);
            if (Fei_Ji_San_Bu_Dai)
            {// 飞机三不带
                cards.level = isFeiji_0(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                    return cards;
                }
            }
            if (Shun_Zi)
            {// 顺子
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Lian_Dui)
            {// 连对
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_7(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_8(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;

            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Lian_Zha)
            {
                cards.level = isLianzha(cards.getCards());
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_ZHA_LIAN;
                    return cards;
                }
            }
            if (Si_Dai_Er_Dui)
            {
                cards.level = is422(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_4_2_2;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dan)
            {
                cards.level = isFeiji_1(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_1;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_9(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Bu_Dai)
            {
                cards.level = isFeiji_0(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_10(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dui)
            {
                cards.level = isFeiji_2(cards.getCards());
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_2;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_11(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]); ;
            int size = cards.getCards().Length;
            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_12(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;

            if (Shun_Zi)
            {
                cards.level = isShunzi(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_CONNECT;
                    return cards;
                }
            }
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Bu_Dai)
            {
                cards.level = isFeiji_0(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                    return cards;
                }
            }
            if (Lian_Zha)
            {
                cards.level = isLianzha(cards.getCards());
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_ZHA_LIAN;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dan)
            {
                cards.level = isFeiji_1(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_1;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_13(DDZCardInfo cards)
        {
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_14(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_15(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int size = cards.getCards().Length;
            // int index = Poker.getValue(cards[0]);
            if (Fei_Ji_San_Bu_Dai)
            {
                cards.level = isFeiji_0(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dui)
            {
                cards.level = isFeiji_2(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_2;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_16(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dan)
            {
                cards.level = isFeiji_1(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_1;
                    return cards;
                }
            }
            if (Lian_Zha)
            {
                cards.level = isLianzha(cards.getCards());
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_ZHA_LIAN;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_17(DDZCardInfo cards)
        {
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_18(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Fei_Ji_San_Bu_Dai)
            {
                cards.level = isFeiji_0(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_THREE_CONNECT;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_19(DDZCardInfo cards)
        {
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        protected static DDZCardInfo checkType_Size_20(DDZCardInfo cards)
        {
            int[] counts = Poker.getCounts(cards.getCards());
            int index = Poker.getValue(cards.getCards()[0]);
            int size = cards.getCards().Length;
            if (Lian_Dui)
            {
                cards.level = isLiandui(counts, index, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_CARDS_DOUBLE_CONNECT;
                    return cards;
                }
            }
            if (Lian_Zha)
            {
                cards.level = isLianzha(cards.getCards());
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_ZHA_LIAN;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dan)
            {
                cards.level = isFeiji_1(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_1;
                    return cards;
                }
            }
            if (Fei_Ji_San_Dai_Dui)
            {
                cards.level = isFeiji_2(counts, size);
                if (cards.level != -1)
                {
                    cards.type = PKCardType.TYPE_FEIJI_2;
                    return cards;
                }
            }
            cards.type = PKCardType.TYPE_ERROR;
            return cards;
        }

        #endregion

        #endregion

        #region 单顺,顺子

        /// <summary> 是否是顺子 </summary>
        public static int isShunzi(int[] cards)
        {
            if (cards.Length < 5) return -1;
            int[] counts = Poker.getCounts(cards);
            int index = Poker.getValue(cards[0]);
            return isShunzi(counts, index, cards.Length);
        }

        /// <summary> 是否是顺子： 每个牌的数量，牌数 </summary>
        public static int isShunzi(int[] counts, int size)
        {
            for (int i = 3; i < counts.Length; i++)
            {
                if (counts[i] != 0) return isShunzi(counts, i, size);
            }
            return -1;
        }

        /// <summary> 是否是顺子： 每个牌的数量，某个牌数不为零的索引，牌数 </summary>
        public static int isShunzi(int[] counts, int startIndex, int size)
        {
            int level = -1;
            for (int i = 3; i < counts.Length - 1; i++)
            {
                if (counts[i] != 0)
                {
                    level = i;
                    break;
                }
            }
            if (size < 5 || startIndex == 0 || startIndex == 2) return -1;
            int value = 1;
            if (startIndex == 1) return eliminate(counts, startIndex, size, value, false) == 0 ? level : -1;// 从A开始消减
            if (startIndex == 3) return eliminate(counts, startIndex, size, value, true) == 0 ? level : -1;// 从3开始消减
            size = eliminate(counts, startIndex, size, value, true);
            if (size > 0)
                return size = eliminate(counts, startIndex - 1, size, value, false) == 0 ? level : -1;
            else if (size == 0)
            {
                return level;
            }
            else return -1;
        }

        #endregion

        #region 二连顺,连对

        /// <summary> 是否是连对 </summary>
        public static int isLiandui(int[] cards)
        {
            int size = cards.Length;
            if ((size & 1) == 1 || size < 6) return -1;
            int[] counts = Poker.getCounts(cards);
            int index = Poker.getValue(cards[0]);
            return isLiandui(counts, index, cards.Length);
        }

        /// <summary> 是否是顺子： 每个牌的数量，牌数 </summary>
        public static int isLiandui(int[] counts, int size)
        {
            for (int i = 3; i < counts.Length; i++)
            {
                if (counts[i] != 0) return isLiandui(counts, i, size);
            }
            return -1;
        }

        /// <summary> 是否是连对 </summary>
        public static int isLiandui(int[] counts, int startIndex, int size)
        {
            int level = -1;
            for (int i = 3; i < counts.Length - 1; i++)
            {
                if (counts[i] != 0)
                {
                    level = i;
                    break;
                }
            }
            if ((size & 1) == 1 || size < 6 || startIndex == 0 || startIndex == 2) return -1;
            int value = 2;
            if (startIndex == 1) return eliminate(counts, startIndex, size, value, false) == 0 ? level : -1;// 从A开始消减
            if (startIndex == 3) return eliminate(counts, startIndex, size, value, true) == 0 ? level : -1;// 从3开始消减
            size = eliminate(counts, startIndex, size, value, true);
            if (size > 0)
                return eliminate(counts, startIndex - 1, size, value, false) == 0 ? level : -1;
            else if (size == 0)
            {
                return level;
            }
            else
                return -1;
        }

        #endregion 

        #region 四连顺，连炸

        /// <summary> 是否是四连顺（连炸） </summary>
        public static int isLianzha(int[] cards)
        {
            int size = cards.Length;
            if ((size & 1) == 1 || size < 8) return -1;
            int[] counts = Poker.getCounts(cards);
            int index = Poker.getValue(cards[0]);
            return isLianzha(counts, index, cards.Length);
        }

        /// <summary> 是否是四连顺（连炸） </summary>
        public static int isLianzha(int[] counts, int size)
        {
            for (int i = 3; i < counts.Length; i++)
            {
                if (counts[i] != 0) return isLianzha(counts, i, size);
            }
            return -1;
        }

        /// <summary> 是否是四连顺（连炸） </summary>
        public static int isLianzha(int[] counts, int startIndex, int size)
        {
            int level = -1;
            for (int i = 3; i < counts.Length - 1; i++)
            {
                if (counts[i] != 0)
                {
                    level = i;
                    break;
                }
            }
            if ((size & 1) == 1 || size < 6 || startIndex == 0 || startIndex == 2) return -1;
            int value = 4;
            if (startIndex == 1) return eliminate(counts, startIndex, size, value, false) == 0 ? level : -1;// 从A开始消减
            if (startIndex == 3) return eliminate(counts, startIndex, size, value, true) == 0 ? level : -1;// 从3开始消减
            size = eliminate(counts, startIndex, size, value, true);
            if (size > 0)
                return eliminate(counts, startIndex - 1, size, value, false) == 0 ? level : -1;
            else if (size == 0)
            {
                return level;
            }
            else
                return -1;
        }

        #endregion 

        #region 对子

        /// <summary> 是否是对子 </summary>
        public static int isDuizi(int[] cards)
        {
            if (cards.Length == 2 && getSortValueByCard(cards[0]) == getSortValueByCard(cards[1])) return getSortValueByCard(cards[0]);
            return -1;
        }

        /// <summary> 是否是对子 </summary>
        public static int isDuizi(int[] counts, int size)
        {
            if (size != 2) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 2) return getSortValue(i);
            }
            return -1;
        }

        #endregion

        #region 王炸

        /// <summary> 是否是王炸 </summary>
        public static int isWangzha(int[] cards)
        {
            if (cards.Length != 2 || cards[0] == cards[1]) return -1;
            int type = Poker.getType(cards[0]);
            if (type == Poker.TYPE_JOKER && type == Poker.getType(cards[1])) return 17;// 大王排序值为17
            return -1;
        }

        /// <summary> 是否是王炸 </summary>
        public static int isWangzha(int[] counts, int size)
        {
            if (size != 2) return -1;
            if (counts[0] == 1 && counts[14] == 1) return 17;// 大王排序值为17
            return -1;
        }

        #endregion

        #region 三不带

        /// <summary> 是否是三不带 </summary>
        public static int isSanbudai(int[] cards)
        {
            if (cards.Length != 3) return -1;
            if (getSortValueByCard(cards[0]) == getSortValueByCard(cards[1])
                && getSortValueByCard(cards[0]) == getSortValueByCard(cards[2])) return getSortValueByCard(cards[0]);
            return -1;
        }

        /// <summary> 是否是三不带 </summary>
        public static int isSanbudai(int[] counts, int size)
        {
            if (size != 3) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 3) return getSortValue(i);
                if (counts[i] != 0) return -1;
            }
            return -1;
        }

        #endregion   

        #region 炸弹

        /// <summary> 是否是炸弹 </summary>
        public static int isZhadan(int[] cards)
        {
            if (cards.Length != 4) return -1;
            if (getSortValueByCard(cards[0]) == getSortValueByCard(cards[1])
                && getSortValueByCard(cards[0]) == getSortValueByCard(cards[2])
                && getSortValueByCard(cards[0]) == getSortValueByCard(cards[3])) return getSortValueByCard(cards[0]);
            return -1;
        }

        /// <summary> 是否是炸弹 </summary>
        public static int isZhadan(int[] counts, int size)
        {
            if (size != 4) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 4) return getSortValue(i);
            }
            return -1;
        }

        #endregion

        #region 三带一

        /// <summary> 是否是三带一 </summary>
        public static int isSandaiyi(int[] cards)
        {
            if (cards.Length != 4) return -1;
            int level = int.MaxValue;
            int a = Poker.CNO, b = Poker.CNO;
            int ac = 0, bc = 0;
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                if (cards[i] == a)
                {
                    ac += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == b)
                {
                    bc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (a == Poker.CNO)
                {
                    a = cards[i];
                    ac = 1;
                }
                else if (b == Poker.CNO)
                {
                    b = cards[i];
                    bc = 1;
                }
                else
                    return -1;
            }
            if (ac == 3 || bc == 3) return level;// 有一个三
            return -1;
        }

        /// <summary> 是否是三带一 </summary>
        public static int isSandaiyi(int[] counts, int size)
        {
            if (size != 4) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 3) return getSortValue(i);
            }
            return -1;
        }

        #endregion

        #region 三带对

        /// <summary> 是否是三带对 </summary>
        public static int isSandaidui(int[] cards)
        {
            if (cards.Length != 5) return -1;
            int level = int.MaxValue;
            int ac = 0, bc = 0;
            int a = Poker.CNO, b = Poker.CNO;
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                if (cards[i] == a)
                {
                    ac += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == b)
                {
                    bc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (a == Poker.CNO)
                {
                    a = cards[i];
                    ac = 1;
                }
                else if (b == Poker.CNO)
                {
                    b = cards[i];
                    bc = 1;
                }
                else
                    return -1;
            }
            if (ac == 3 || bc == 3) return level;// 有一个三
            return -1;
        }

        /// <summary> 是否是三带对 </summary>
        public static int isSandaidui(int[] counts, int size)
        {
            if (size != 5) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 3)
                {
                    for (int j = 0; j < counts.Length; j++)
                    {
                        if (counts[j] == 2) return getSortValue(i);
                    }
                }
            }
            return -1;
        }

        #endregion

        #region 四带单

        /// <summary> 是否是四带单  </summary>
        private static int is41(int[] counts, int size)
        {
            if (size != 5) return -1;
            for (int i = 1; i < counts.Length - 1; i++)
            {
                if (counts[i] == 4) return getSortValue(i);
            }
            return -1;
        }

        #endregion

        #region 四带二单

        /// <summary> 是否是4带2单 </summary>
        public static int is411(int[] cards)
        {
            if (cards.Length != 6) return -1;
            int a = Poker.CNO, b = Poker.CNO, c = Poker.CNO;
            int level = int.MaxValue;
            int ac = 0, bc = 0, cc = 0;
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                if (a == Poker.CNO)
                {
                    a = cards[i];
                    ac = 1;
                }
                else if (b == Poker.CNO)
                {
                    b = cards[i];
                    bc = 1;
                }
                else if (c == Poker.CNO)
                {
                    c = cards[i];
                    cc = 1;
                }
                else if (cards[i] == a)
                {
                    ac += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == b)
                {
                    bc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == c)
                {
                    cc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else
                    return -1;// 超过三种牌
            }
            // 考虑是否可以带对子，也就是不能出现2
            if (ac == 4 || bc == 4 || cc == 4) return level;// 有一个4
            return -1;
        }

        /// <summary> 是否是4带2单 </summary>
        public static int is411(int[] counts, int size)
        {
            if (size != 6) return -1;
            for (int i = 1; i < counts.Length; i++)
            {
                if (counts[i] == 3) return -1;
                if (counts[i] == 4) return getSortValue(i);
            }
            return -1;
        }

        #endregion

        #region 四带二对

        /// <summary> 是否是4带2对 </summary>
        public static int is422(int[] cards)
        {
            if (cards.Length != 8) return -1;
            int a = Poker.CNO, b = Poker.CNO, c = Poker.CNO;
            int level = int.MaxValue;
            int ac = 0, bc = 0, cc = 0;
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                if (cards[i] == a)
                {
                    ac += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == b)
                {
                    bc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (cards[i] == c)
                {
                    cc += 1;
                    v = getSortValueByCard(cards[i]);
                    if (v < level) level = v;
                }
                else if (a == Poker.CNO)
                {
                    a = cards[i];
                    ac = 1;
                }
                else if (b == Poker.CNO)
                {
                    b = cards[i];
                    bc = 1;
                }
                else if (c == Poker.CNO)
                {
                    c = cards[i];
                    cc = 1;
                }
                else
                    return -1;
            }
            if ((ac & bc & cc & 1) == 1) return -1;// 不能出现3和1
                                                   // 422
            if (ac == 4 || bc == 4 || cc == 4) return level;// 有一个4
            return -1;
        }

        /// <summary> 是否是四带二对 </summary>
        public static int is422(int[] counts, int size)
        {
            if (size != 8) return -1;
            int level = -1;
            int dui = 0, si = 0;
            for (int i = 1; i < counts.Length; i++)
            {
                switch (counts[i])
                {
                    case 2:
                        if (dui == 2) return -1;
                        if (dui == 1 && si == 1)
                        {
                            return level;
                        }
                        dui += 1;
                        break;

                    case 4:
                        if (si == 1)
                        {
                            int v = getSortValue(i);
                            return (v > level) ? v : level;
                        }
                        level = getSortValue(i);
                        if (dui == 2) return level;
                        si = 1;
                        break;

                    case 1:
                    case 3:
                        return -1;// 三带对不应该出现单张
                }
            }
            return -1;
        }

        #endregion

        #region 三连顺，飞机三不带

        /// <summary> 是否是三连顺（飞机三不带） </summary>
        public static int isFeiji_0(int[] cards)
        {
            int size = cards.Length;
            if ((size % 3) != 0 || size < 6) return -1;
            int[] counts = Poker.getCounts(cards);
            int index = Poker.getValue(cards[0]);
            return isFeiji_0(counts, index, cards.Length);
        }

        /// <summary> 是否是三连顺（飞机三不带） </summary>
        public static int isFeiji_0(int[] counts, int size)
        {
            for (int i = 3; i < counts.Length; i++)
            {
                if (counts[i] != 0) return isFeiji_0(counts, i, size);
            }
            return -1;
        }

        /// <summary> 是否是三连顺（飞机三不带） </summary>
        public static int isFeiji_0(int[] counts, int startIndex, int size)
        {
            int level = -1;
            for (int i = 3; i < counts.Length - 1; i++)
            {
                if (counts[i] != 0)
                {
                    level = i;
                    break;
                }
            }
            if ((size % 3) != 0 || size < 6 || startIndex == 0 || startIndex == 2) return -1;
            int value = 3;
            if (startIndex == 1) return eliminate(counts, startIndex, size, value, false) == 0 ? level : -1;// 从A开始消减
            if (startIndex == 3) return eliminate(counts, startIndex, size, value, true) == 0 ? level : -1;// 从3开始消减
            size = eliminate(counts, startIndex, size, value, true);
            if (size > 0)
                return eliminate(counts, startIndex - 1, size, value, false) == 0 ? level : -1;
            else if (size == 0)
            {
                return level;
            }
            else
                return -1;
        }

        #endregion

        #region 飞机三带一

        /// <summary> 是否是飞机三带一 </summary>
        public static int isFeiji_1(int[] cards)
        {
            int size = cards.Length;
            if (size < 8 || (size & 3) != 0) return -1;
            int[] counts = Poker.getCounts(cards);
            return isFeiji_1(counts, cards.Length);
        }

        /// <summary> 是否是飞机三带一 </summary>
        public static int isFeiji_1(int[] counts, int size)
        {
            if (size < 8 || (size & 3) != 0) return -1;
            int count = size / 4;
            int j = 0, k = 0;
            for (int i = 3; i < counts.Length; i++)
            {
                if (counts[i] > 2)
                {
                    if (k == 0)
                    {
                        k = i;
                        if (count > 2) continue;
                        else break;
                    }
                    else
                    {
                        if (k == i - 1) break;
                        if (i > k + 1 && counts[k + 1] < 3)
                        {
                            k = i; break;
                        }
                    }
                }
            }
            if (k == 0) return -1;

            int level = getSortValue(k);

            j = k + count;
            if (j > counts.Length) return -1;
            if (j == counts.Length)
            {
                if (counts[1] < 3) return -1;
                j -= 1;
            }
            for (k += 1; k < j; k++)
            {
                if (counts[k] < 3) return -1;
            }
            return level;
        }

        #endregion

        #region 飞机三带对

        /// <summary> 是否是飞机三带二 </summary>
        public static int isFeiji_2(int[] cards)
        {
            int size = cards.Length;
            if (size < 10 || size % 5 != 0) return -1;
            int[] counts = Poker.getCounts(cards);
            return isFeiji_2(counts, size);
        }

        /// <summary> 是否是飞机三带二 </summary>
        public static int isFeiji_2(int[] counts, int size)
        {
            if (size < 10 || size % 5 != 0) return -1;
            int level = -1;
            int san = size / 5, dui = 0; // 三连数量，对子数量
            for (int i = 0, j = -1; i < counts.Length; i++)
            {
                switch (counts[i])
                {
                    case 2:
                        dui += 1;// 对子计数
                        break;

                    case 3:
                        if (i == 1) break;// 3张A暂时忽略
                        if (i == 2) return -1;// 2不能作为飞机中的非带牌
                        if (j == -1)
                        {
                            level = getSortValue(i);
                            j = i + san;// 结束
                            if (j > counts.Length) return -1;
                            if (j == counts.Length)// 三连包含A
                            {
                                if (counts[1] != 3) return -1;// A不为3
                                j -= 1;// 回退一个
                            }
                        }
                        if (i >= j) return -1;// 在三连范围外还有3张相同牌
                        if (counts[i] != 3) return -1;// 在三连范围内，牌数不为3
                        break;

                    case 4:
                        dui += 2;// 4张可能拆分为2个对子
                        break;

                    case 0:
                        break;
                    case 1:
                    default:
                        return -1;// 三带对不应该出现单张
                }
            }
            if (dui == san) return level;
            return -1;
        }

        #endregion  
    }
}
