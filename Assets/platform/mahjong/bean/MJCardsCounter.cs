using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    /// <summary>
    /// 牌型计算器
    /// </summary>
    public class MJCardsCounter : Analyzer
    {
        /** 花猪判定位值：筒条万三种标记位组合 */
        public const int MULTIPLE_SUIT = MJCard.DOT | MJCard.BAM | MJCard.CHA;

        // -----------临时数据缓存---------------
        /// <summary> 是否有将头 </summary>
        protected bool pair = false;

        /// <summary> 消减牌后剩牌列表 </summary>
        protected ArrayList<int> ints = new ArrayList<int>();



        /// <summary></summary>
        public MJCardsCounter(Rule rule, ArrayList<int> cards, ArrayList<FixedCards> fixs, int paidui):base(rule,paidui)
        {
            init(cards, fixs);
        }

        /// <summary>
        /// 剩余的手牌能否消减完，用于判断躺牌时，选择的躺的牌是否符合要求
        /// </summary>
        /// <returns></returns>
        public bool isEliminate()
        {
            int value = getTypeBits();
            int count = 0;
            for (int i = 0; i < MJCard.BITS.Length; i++)
            {
                if ((value & MJCard.BITS[i]) == MJCard.BITS[i]) count++;
            }

            if (count == 3) return false;
            return isNeatPair(getTypeBits()) || isCommon(value);
        }

        /** 尝试胡指定牌，并返回胡牌颗数：-1表示未满足胡牌牌型 */
        public int getHuPoint(int huType, int huCard, ArrayList<FixedCards> fixeds)
        {
            return getHuSettle(huCard, huType, fixeds).point;
        }

        /// <summary> 验证是否可以胡牌 </summary>
        public virtual MJSettle getHuSettle(int huCard, int huTypes, ArrayList<FixedCards> fixeds)
        {
            MJSettle acc = getHuAcco(huTypes, fixeds);

            if (rule.getRuleAtribute("enableMID5"))
            {
                int card_type = MJCard.getType(huCard);
                int index = MJCard.getIndex(huCard);
                counts[card_type][index]--;
                int[] lsns = checkOutListens(fixeds);
                counts[card_type][index]++;
                int[] arr = counts[card_type];
                if (lsns.Length == 1)// 听单张（单钓，胡边张，胡卡张）
                {
                    if (index == 4)// 单钓5或卡心5
                    {
                        if (arr[index] == 1 || (arr[index] == 2 && arr[index - 1] >= 2 && arr[index + 1] >= 2))
                            acc.atype |= MJSettle.A_LSN_MID_5;
                    }
                }
            }

            int atpye = (int)acc.atype;
            //自摸在gethuAcco已经获取
            if (StatusKit.hasStatus(atpye, MJSettle.A_ROB))
                acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_ROB);
            if (StatusKit.hasStatus(atpye, MJSettle.A_KONG_HU))
                acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_KONG_HU);
            if (StatusKit.hasStatus(atpye, MJSettle.A_KONG_PAO))
                acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_KONG_PAO);
            if (StatusKit.hasStatus(atpye, MJSettle.A_TIAN_HU))
            {
                if (rule.sid == 2010)
                {
                    acc.point += rule.maxPoint;
                }
                else
                {
                    acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_TIAN_HU);
                }
            }

            if (StatusKit.hasStatus(atpye, MJSettle.A_DI_HU))
            {
                if (rule.sid == 2010)
                {
                    acc.point += rule.maxPoint;
                }
                else
                {
                    acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_DI_HU);
                }
            }
            if (StatusKit.hasStatus(atpye, MJSettle.A_HAIDI))
                acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_HAIDI);
            if (StatusKit.hasStatus(atpye, MJSettle.A_LSN_MID_5))
                acc.point += MJRulePointType.getTypePoint(MJRulePointType.A_LSN_MID_5);

            if (rule.getRuleAtribute("enableJustOne") && isSingle())
                acc.point++;
            return acc;
        }

        /// <summary> 牌型判断：是否是齐对（7对，4对） </summary>
        protected override bool isNeatPair(int bits)
        {
            return base.isNeatPair(bits);
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

        /// <summary> 牌型判断：是否是花猪: </summary>
        public bool isMultipleSuit()
        {
            int value = this.getTypeBits();
            int count = 0;
            for (int i = 0; i < MJCard.BITS.Length; i++)
            {
                if ((value & MJCard.BITS[i]) == MJCard.BITS[i]) count++;
            }

            return count == 3;
        }

        /// <summary>
        /// 加番判断：获取带根的数量：这里返回的已经计算杠牌了
        /// </summary>
        /// <returns></returns>
        public override int getRootCount()
        {
            byte count = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (counts[i][j] > 3 || fixcounts[i][j] > 3 || counts[i][j] + fixcounts[i][j] > 3) count++;
                }
            }

            return count;
        }

        public virtual bool isMenqing(ArrayList<FixedCards> fixeds)
        {
            if (!rule.getRuleAtribute("enableNoFixOr19")) return false;
            for (int i = fixeds.count - 1; i > -1; --i)
            {
                if (!fixeds.get(i).isKongPri()) return false; // 只有存在暗杠
            }

            return true;
        }


        /// <summary> 是否只包含258 </summary>
        public virtual bool isOnly258()
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
        public virtual bool isNo19()
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
        public virtual bool isOnly19()
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
        public virtual bool isWith19()
        {
            if (!rule.getRuleAtribute("enable258or19")) return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (fixcounts[i][j] > 0) return false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (counts[i][j] > 0) return false;
                }

                if (this.counts[i][1] != this.counts[i][2] || this.counts[i][7] != this.counts[i][6] ||
                    this.counts[i][1] > this.counts[i][0] || this.counts[i][7] > this.counts[i][8])
                    return false;
            }

            return true;
        }

        /// <summary> 是否是对对胡（碰碰胡，大对子） </summary>
        public virtual bool isTriplet()
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
            return value == MJCard.DOT || value == MJCard.BAM || value == MJCard.CHA;
        }

        /** 四川血战胡牌前提是缺一门，如果启用定缺，那么在调用该方法前应该判断缺的是否是定缺牌 */
        protected override bool checkHuPremise(int bits)
        {
            return !isMultipleSuit(bits);
        }


        /// <summary> 获得胡牌账目信息 </summary>
        public virtual MJSettle getHuAcco(int huType, ArrayList<FixedCards> fixeds)
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
            MJSettle settle =new MJSettle(MJSettle.SETTLE_HU, ptype, atype, point);
            if (rule.sid == 2003)//绵阳麻将
                settle = new MYMJSettle(MJSettle.SETTLE_HU, ptype, atype, point);

            return settle;
        }

        /// <summary>
        /// 获取牌型的番数
        /// </summary>
        protected virtual void pTypePoint(int root, int bits, ref int point, ref int atype, ref int ptype)
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
        protected virtual void aTypePoint(int bits, ref int point, ref int atype, ArrayList<FixedCards> fixeds)
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
                point += MJRulePointType.getTypePoint(MJRulePointType.A_QING);
                atype |= MJSettle.A_QING;
            }
        }
      

        /** 获取胡牌类型得附加类型 */
        public virtual int getATypeFromHuType(int huType)
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
        protected virtual int getATypeFromRoot(int root)
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
        protected virtual int getPTypeFromNeatPairs(int root)
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
        public virtual bool isMultipleSuit(int bits)
        {
            return (bits & MULTIPLE_SUIT) == MULTIPLE_SUIT; // 同时包含筒条万
        }
    }
}

