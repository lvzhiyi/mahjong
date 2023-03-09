using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    /// <summary>
    /// 牌型计算器
    /// </summary>
    public class MJCards2r1fCounter : MJCardsCounter
    {
        /// <summary></summary>
        public MJCards2r1fCounter(Rule rule, ArrayList<int> cards, ArrayList<FixedCards> fixs, int paidui):base(rule, cards, fixs,paidui)
        {
            
        }

        /// <summary> 验证是否可以胡牌 </summary>
        public override MJSettle getHuSettle(int tile, int huType, ArrayList<FixedCards> fixeds)
        {
            return base.getHuSettle(tile,huType,fixeds);
        }


        /// <summary> 牌型判断：是否是齐对（7对，4对） </summary>
        protected override bool isNeatPair(int bits)
        {
            if (rule.getRuleAtribute("enable4NeatPair"))
                return base.isNeatPair(bits);
            return false;
        }

        /// <summary>
        /// 牌型判断：是否是平胡牌型（由刻子和顺子组成）
        /// </summary>
        /// <param name="bits">花色的掩码</param>
        /// <returns></returns>
        protected override bool isCommon(int bits)
        {
            return base.isCommon(bits);
        }

        /// <summary> 按照将牌，顺子，刻子消减牌，是否消减完 </summary>
        bool eliminate(int[] counts)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == 0) continue;
                if (counts[i] > 1 && !pair) // 无将头则先提出将牌
                {
                    counts[i] -= 2;
                    pair = true;
                    if (!eliminate(counts)) // 如果递归回来依旧没有减完，则把将加回去
                    {
                        counts[i] += 2;
                        pair = false;
                    }
                }

                if (i < 7 && counts[i] > 0 && counts[i + 1] > 0 && counts[i + 2] > 0)
                {
                    --counts[i];
                    --counts[i + 1];
                    --counts[i + 2];
                    if (!eliminate(counts))
                    {
                        ++counts[i];
                        ++counts[i + 1];
                        ++counts[i + 2];
                    }
                }

                if (counts[i] > 2)
                {
                    counts[i] -= 3;
                    if (!eliminate(counts)) counts[i] += 3;
                }
            }


            // 最后判断减没减完
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0) return false;
            }

            return true;
        }

       

        /// <summary> 牌型判断：是否是门清 </summary>
        public override bool isMenqing(ArrayList<FixedCards> fixeds)
        {
            return base.isMenqing(fixeds);
        }


        /// <summary> 是否只包含258 </summary>
        public override bool isOnly258()
        {
            if (!rule.getRuleAtribute("enable258or19")) return false;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (j == 1 || j == 4 || j == 7) continue;
                    if (counts[i][j] > 0 || fixcounts[i][j] > 0) return false;
                }
            }

            return true;
        }

        /// <summary> 是否不包含幺九牌 </summary>
        public override bool isNo19()
        {
            if (!rule.getRuleAtribute("enableNoFixOr19")) return false;
            for (int i = 0; i < counts.Length; i++)
            {
                if (i == MJCard.TYPE_WIND || i == MJCard.TYPE_DRAGON || i == MJCard.TYPE_FLOWER) continue;
                if (counts[i][0] > 0 || counts[i][8] > 0) return false;
                if (fixcounts[i][0] > 0 || fixcounts[i][8] > 0) return false;
            }

            return true;
        }

        /// <summary> 是否是全幺九 </summary>
        public override bool isOnly19()
        {
            if (!rule.getRuleAtribute("enable258or19")) return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (this.counts[i][j] > 0 || this.fixcounts[i][j] > 0) return false;
                }
            }

            return true;
        }

        /// <summary> 是否是带幺九 </summary>
        public override bool isWith19()
        {
            if (!rule.getRuleAtribute("enable258or19")) return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (this.fixcounts[i][j] > 0) return false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (this.counts[i][j] > 0) return false;
                }

                if (this.counts[i][1] != this.counts[i][2] || this.counts[i][7] != this.counts[i][6] ||
                    this.counts[i][1] > this.counts[i][0] || this.counts[i][7] > this.counts[i][8])
                    return false;
            }

            return true;
        }

        /// <summary> 是否是对对胡（碰碰胡，大对子） </summary>
        public override bool isTriplet()
        {
            int pairs = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < this.counts[i].Length; j++)
                {
                    if (counts[i][j] == 1 || this.counts[i][j] == 4) return false;
                    if (counts[i][j] == 2 && (++pairs) > 1) return false;
                }
            }

            return true;
        }

        /// <summary> 加番判断：是否是清一色 </summary>
        public override bool isQing(int value)
        {
            if (rule.getRuleAtribute("enableQing"))
                return base.isQing(value);
            return false;
        }

        /** 四川血战胡牌前提是缺一门，如果启用定缺，那么在调用该方法前应该判断缺的是否是定缺牌 */
        protected override bool checkHuPremise(int bits)
        {
            return !isMultipleSuit(bits);
        }


        /// <summary> 获得胡牌账目信息 </summary>
        public override MJSettle getHuAcco(int huType, ArrayList<FixedCards> fixeds)
        {
            // 所有牌型都是按照先检查大牌型的顺序
            int bits = getTypeBits();
            int root = getRootCount();
            int point = root;
            int ptype = MJSettle.P_PING_HU; //牌型

            int atype = getATypeFromHuType(huType); //额外加番

            pTypePoint(root, bits, ref point, ref atype, ref ptype);

            aTypePoint(bits, ref point, ref atype,fixeds);

            int score = 0;
            if (StatusKit.hasStatus(atype, MJSettle.A_SELF)) // 自摸
            {
                int v = Room.room.getRule().getIntAtribute("zimoAddtion");
                if (v == 2) // 自摸加番
                    point += MJRulePointType.getTypePoint(MJRulePointType.A_SELF);
                else if (v == 1) score += 1; //自摸加底
            }

            MJSettle settle = new MJSettle(MJSettle.SETTLE_HU, ptype, atype, point);
            return settle;
        }

        /// <summary>
        /// 获取牌型的番数
        /// </summary>
        protected override void pTypePoint(int root, int bits, ref int point, ref int atype, ref int ptype)
        {
            if (isCommon(bits))
            {
                if (isWith19())
                {
                    point += MJRulePointType.getTypePoint(MJRulePointType.A_WITH_19);
                    atype |= MJSettle.A_WITH_19;
                    atype |= getATypeFromRoot(root);
                }
                else if (isNeatPair(bits))
                {
                    ptype = getPTypeFromNeatPairs(root);
                    point += MJRulePointType.getTypePoint(MJRulePointType.P_NEAT); // ，七对番数高，所以额外检查是否是七对
                }
                else
                {
                    atype |= getATypeFromRoot(root);
                    if (isTriplet())
                    {
                        ptype = MJSettle.P_TRIPLET;

                        if (rule.getRuleAtribute("enableTriplet2"))
                        {
                            point += MJRulePointType.getTypePoint(MJRulePointType.P_TRIPLET_2);
                        }
                        else
                            point += MJRulePointType.getTypePoint(MJRulePointType.P_TRIPLET);
                        if (isOnly258())
                        {
                            point += MJRulePointType.getTypePoint(MJRulePointType.A_ONLY_258);
                            atype |= MJSettle.A_ONLY_258;
                        }
                    }
                }
            }
            else if (isNeatPair(bits)) // 七对，七对和带幺九有牌型并集
            {
                ptype = getPTypeFromNeatPairs(root);
                point += MJRulePointType.getTypePoint(MJRulePointType.P_NEAT);
            }
        }

        /// <summary>
        /// 额外加番项
        /// </summary>
        protected override void aTypePoint(int bits, ref int point, ref int atype, ArrayList<FixedCards> fixeds)
        {
            if (isNo19()) // 中张（断幺九）
            {
                point += MJRulePointType.getTypePoint(MJRulePointType.A_NO_19);
                atype |= MJSettle.A_NO_19;
            }

            if (isMenqing(fixeds)) // 门清
            {
                point += MJRulePointType.getTypePoint(MJRulePointType.A_NO_FIX);
                atype |= MJSettle.A_NO_FIX;
            }

            if (isQing(bits)) // 清一色
            {
                point += 1;
            }
        }

        /** 获取胡牌类型得附加类型 */
        public override int getATypeFromHuType(int huType)
        {
            switch (huType)
            {
                case MJMatchHuOperate.HU_HAIDI:
                    return rule.getRuleAtribute("enableHaidi") ? MJSettle.A_HAIDI : 0;
                case MJMatchHuOperate.HU_HAIDI| MJMatchHuOperate.HU_SELF:
                    return rule.getRuleAtribute("enableHaidi") ? MJSettle.A_HAIDI | MJSettle.A_SELF : 0;
                case MJMatchHuOperate.HU_TIAN:
                    return (rule.getRuleAtribute("enableTiandiHu")) ? MJSettle.A_TIAN_HU | MJSettle.A_SELF : MJSettle.A_SELF;
                case MJMatchHuOperate.HU_DI:
                    return (rule.getRuleAtribute("enableTiandiHu")) ? MJSettle.A_DI_HU : 0;
                case MJMatchHuOperate.HU_ROB:
                    return MJSettle.A_ROB;
                case MJMatchHuOperate.HU_ROB | MJMatchHuOperate.HU_HAIDI:
                    return MJSettle.A_ROB | MJSettle.A_HAIDI;
                case MJMatchHuOperate.HU_KONG:
                    return MJSettle.A_KONG_PAO;
                case MJMatchHuOperate.HU_KONG_SELF:
                    return MJSettle.A_KONG_HU | MJSettle.A_SELF;
                case MJMatchHuOperate.HU_KONG_OTHER:
                    bool b = rule.getRuleAtribute("konghuasself");
                    return b ? MJSettle.A_KONG_HU | MJSettle.A_SELF : MJSettle.A_KONG_HU;
                case MJMatchHuOperate.HU_SELF:
                    return MJSettle.A_SELF;
                default:
                    return 0;
            }
        }

        /** 获取根数得附加类型 */
        protected override int getATypeFromRoot(int root)
        {
            switch (root)
            {
                case 1:
                    return MJSettle.A_ROOT_1;
                case 2:
                    return MJSettle.A_ROOT_2;
                case 3:
                    return MJSettle.A_ROOT_3;
                case 4:
                    return MJSettle.A_ROOT_4;
                default:
                    return 0;
            }
        }

        /** 获取齐对的牌型类型 */
        protected override int getPTypeFromNeatPairs(int root)
        {
            switch (root)
            {
                case 1:
                    return MJSettle.P_NEAT_R;
                case 2:
                    return MJSettle.P_NEAT_R1;
                case 3:
                    return MJSettle.P_NEAT_R2;
                default:
                    return MJSettle.P_NEAT;
            }
        }

        /**
	 * 是否是花猪: 同时包含筒条万
	 * 
	 * @param bits 手牌包含得所有类型的位值组合
	 * @return
	 */
        public override bool isMultipleSuit(int bits)
        {
            return (bits & MULTIPLE_SUIT) == MULTIPLE_SUIT; // 同时包含筒条万
        }
    }
}

