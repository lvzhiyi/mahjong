using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    /// <summary>
    /// 牌型计算器
    /// </summary>
    public class AYMJCardsCounter : Analyzer
    {
        /** 花猪判定位值：筒条万三种标记位组合 */
        public const int MULTIPLE_SUIT = MJCard.DOT | MJCard.BAM | MJCard.CHA;

        // -----------临时数据缓存---------------
        /// <summary> 是否有将头 </summary>
        protected bool pair = false;

        /// <summary> 消减牌后剩牌列表 </summary>
        protected ArrayList<int> ints = new ArrayList<int>();


        /// <summary></summary>
        public AYMJCardsCounter(Rule rule, ArrayList<int> cards, ArrayList<FixedCards> fixs, int paidui):base(rule,paidui)
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
        public virtual AYMJSettle getHuSettle(int huCard, int huTypes, ArrayList<FixedCards> fixeds)
        {
            AYMJSettle acc = getHuAcco(huTypes,huCard, fixeds);


            int atpye = (int)acc.atype;
           
            if (StatusKit.hasStatus(atpye, MJSettle.A_TIAN_HU))
            {
                acc.point += 3;
            }

            if (StatusKit.hasStatus(atpye, MJSettle.A_DI_HU))
            {
                    acc.point += 2;
            }

            if (rule.getRuleAtribute("enableJustOne") && isSingle())
                acc.point += 1;
            return acc;
        }

        /** 是否听卡心五 */
        public bool isLsnMid5(int huCard, ArrayList<FixedCards> fixeds)
        {
            if (!rule.getRuleAtribute("enableMID5")) return false;
            int index = MJCard.getIndex(huCard);
            if (index != 4) return false;// 不是胡5
            int type = MJCard.getType(huCard);
            counts[type][index]--;
            int[] lsns = checkOutListens(fixeds);
            counts[type][index]++;
            if (lsns.Length == 1)// 听单张（单钓，胡边张，胡卡张）
            {
                int[] arr = counts[type];
                // if(arr[index]==1) return true;
                // if(arr[index]==2&&arr[index-1]>=2&&arr[index+1]>=2) return
                // true;
                if (arr[index - 1] == 0 || arr[index + 1] == 0) return false;
                arr[index - 1] -= 1;
                arr[index] -= 1;
                arr[index + 1] -= 1;
                int t = Eliminator.eliminate(arr);
                arr[index - 1] += 1;
                arr[index] += 1;
                arr[index + 1] += 1;
                if (t != Eliminator.FAIL) return true;
            }
            return false;
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
        public virtual AYMJSettle getHuAcco(int huType,int huCard, ArrayList<FixedCards> fixeds)
        {
            // 所有牌型都是按照先检查大牌型的顺序
            int bits = getTypeBits();
            int root = getRootCount();
           
            int ptype = AYMJSettle.P_PING_HU; //牌型

            int atype = getATypeFromHuType(huType); //额外加番

            pTypePoint(root, bits, ref atype, ref ptype);

            aTypePoint(bits, ref atype,fixeds);

            if (ptype== MJSettle.P_PING_HU &&isLsnMid5(huCard, fixeds))
            {
                atype |= AYMJSettle.A_LSN_MID_5;
            }

            if (ptype == MJSettle.P_PING_HU&&isShun19(bits))
            {
                atype |= AYMJSettle.A_SHUN_19;
            }

            atype |= isBBG(bits, ptype);

            int point = AYMJSettle.getPointFromAType(ptype, atype, rule);
            AYMJSettle settle = new AYMJSettle(AYMJSettle.SETTLE_HU, ptype, atype, point);
            return settle;
        }

        /// <summary>
        /// 获取牌型的番数
        /// </summary>
        protected virtual void pTypePoint(int root, int bits, ref int atype, ref int ptype)
        {
            if (isCommon(bits))
            {
                if (isWith19())
                {
                    atype |= MJSettle.A_WITH_19;
                    atype |= getATypeFromRoot(root);
                }
                else if (isNeatPair(bits))
                {
                    ptype = getPTypeFromNeatPairs(root);
                }
                else
                {
                    atype |= getATypeFromRoot(root);
                    if (isTriplet())
                    {
                        ptype = MJSettle.P_TRIPLET;
                        if (isOnly258())
                        {
                            atype |= MJSettle.A_ONLY_258;
                        }
                    }
                }
            }
            else if (isNeatPair(bits)) // 七对，七对和带幺九有牌型并集
            {
                ptype = getPTypeFromNeatPairs(root);
            }
        }

        /// <summary>
        /// 额外加番项
        /// </summary>
        protected virtual void aTypePoint(int bits, ref int atype, ArrayList<FixedCards> fixeds)
        {
            if (isNo19()) // 中张（断幺九）
            {
                atype |= AYMJSettle.A_NO_19;
            }

            if (isMenqing(fixeds)) // 门清
            {
                atype |= AYMJSettle.A_NO_FIX;
            }

            if (isQing(bits)) // 清一色
            {
                atype |= AYMJSettle.A_QING;
            }
        }
      

        /** 获取胡牌类型得附加类型 */
        public virtual int getATypeFromHuType(int huType)
        {
            switch (huType)
            {
                case MJMatchHuOperate.HU_HAIDI:
                    return rule.getRuleAtribute("enableHaidi") ? AYMJSettle.A_HAIDI : 0;
                case MJMatchHuOperate.HU_HAIDI| MJMatchHuOperate.HU_SELF:
                    return rule.getRuleAtribute("enableHaidi") ? AYMJSettle.A_HAIDI | AYMJSettle.A_SELF : 0;
                case MJMatchHuOperate.HU_TIAN:
                    return (rule.getRuleAtribute("enableTiandiHu")) ? AYMJSettle.A_TIAN_HU | AYMJSettle.A_SELF : AYMJSettle.A_SELF;
                case MJMatchHuOperate.HU_DI:
                    return (rule.getRuleAtribute("enableTiandiHu")) ? AYMJSettle.A_DI_HU : 0;
                case MJMatchHuOperate.HU_DI| MJMatchHuOperate.HU_KONG:
                    return (rule.getRuleAtribute("enableTiandiHu")) ? AYMJSettle.A_DI_HU| MJMatchHuOperate.HU_KONG : 0;
                case MJMatchHuOperate.HU_ROB:
                    return AYMJSettle.A_ROB;
                case MJMatchHuOperate.HU_ROB | MJMatchHuOperate.HU_HAIDI:
                    return AYMJSettle.A_ROB | AYMJSettle.A_HAIDI;
                case MJMatchHuOperate.HU_KONG:
                    return AYMJSettle.A_KONG_PAO;
                case MJMatchHuOperate.HU_KONG_SELF:
                    return AYMJSettle.A_KONG_HU | AYMJSettle.A_SELF;
                case MJMatchHuOperate.HU_KONG_OTHER:
                    bool b = rule.getRuleAtribute("konghuasself");
                    return b ? AYMJSettle.A_KONG_HU | AYMJSettle.A_SELF : AYMJSettle.A_KONG_HU;
                case MJMatchHuOperate.HU_SELF:
                    return AYMJSettle.A_SELF;
                default:
                    return 0;
            }
        }

        private int _findPairSeque(int[] arr, int s, int e, bool isNormal)
        {
            bool find;
            int left, right;
            if (isNormal)
            {
           
                    right = e;
                    while ((find = e - s > 1))
                    {
                        left = s;
                        while (find)
                        {
                            if (Eliminator.eliminateIgnorePair(arr)) goto search;
                            // 缩小左边进行查找： abcde -> bcde
                            arr[s++] += 2;
                            find = e - s > 1;
                        }
                        // 还原s
                        while (s > left)
                            arr[--s] -= 2;
                        // 缩小右边进行查找： abcde -> abcd
                        arr[e--] += 2;
                    }
                    // 还原e
                    while (right > e)
                        arr[++e] -= 2;
                search:
                ;

            }
            else
                find = e - s > 1;

            for (int i = s; i <= e; i++)
                arr[i] += 2;
            return find ? (e - s + 1) / 3 : 0;
        }

        private int findPairSeque(int[] arr, bool isNormal)
        {
            int total = 0, k = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 2)
                {
                    if (k != -1)// 存在起始索引，且与当前索引距离达到最小长度
                    {
                        total += _findPairSeque(arr, k, i - 1, isNormal);
                        k = -1;
                    }
                }
                else
                {
                    if (k == -1) k = i;
                    arr[i] -= 2;
                }
            }
            if (k != -1) total += _findPairSeque(arr, k, arr.Length - 1, isNormal);
            return total;
        }


        public int isBBG(int bits, int ptype)
        {
            if (ptype == AYMJSettle.P_TRIPLET || !rule.getRuleAtribute("enableBBG")) return 0;
            bool isNormal = ptype == AYMJSettle.P_PING_HU;
            int total = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                if ((bits & MJCard.BITS[i]) == 0) continue;
                total += findPairSeque(counts[i], isNormal);
            }
            if (total == 0) return 0;
            return total == 1 ? AYMJSettle.A_BBG_1 : AYMJSettle.A_BBG_2;
        }

        /** 是否是一条龙 */
        public bool isShun19(int bits)
        {
            if (!rule.getRuleAtribute("enableShun1To9")) return false;
            int shun19Type = -1;
            for (int i = 0; i < counts.Length; i++)
            {
                bool shun = true;
                for (int j = 0; j < counts.Length; j++)
                {
                    if (counts[i][j] == 0)
                    {
                        shun = false;
                        break;
                    }
                }
                if (shun)
                {
                    shun19Type = i;
                    break;
                }
            }
            if (shun19Type == -1) return false;
            int[] arr = counts[shun19Type];
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= 1;
            bool b = Eliminator.checkHu(counts, bits);
            for (int i = 0; i < arr.Length; i++)
                arr[i] += 1;
            return b;
        }

        /** 获取根数得附加类型 */
        protected virtual int getATypeFromRoot(int root)
        {
            switch (root)
            {
                case 1:
                    return AYMJSettle.A_ROOT_1;
                case 2:
                    return AYMJSettle.A_ROOT_2;
                case 3:
                    return AYMJSettle.A_ROOT_3;
                case 4:
                    return AYMJSettle.A_ROOT_4;
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
                    return AYMJSettle.P_NEAT_R;
                case 2:
                    return AYMJSettle.P_NEAT_R1;
                case 3:
                    return AYMJSettle.P_NEAT_R2;
                default:
                    return AYMJSettle.P_NEAT;
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

