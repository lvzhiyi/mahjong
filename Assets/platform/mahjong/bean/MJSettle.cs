using basef.rule;
using cambrian.common;

namespace platform.mahjong
{
    public class MJSettle : Settle
    {
        /// <summary>结算：胡牌/放炮</summary>
        public const int SETTLE_HU = 1;
        /// <summary>结算：自摸/被自摸</summary>
        public const int SETTLE_HU_SELF = 2;
        /// <summary>结算：抢杠/被抢杠</summary>
        public const int SETTLE_HU_ROB = 3;
        /// <summary> 结算：下雨（点杠），显示忽略牌型，加分类型， </summary>
        public const int SETTLE_RAIN_PUB = 4;
        /// <summary> 结算：下雨（暗杠） </summary>
        public const int SETTLE_RAIN_PRI = 5;
        /// <summary> 结算：刮风（巴杠） </summary>
        public const int SETTLE_RAIN_SUP = 6;
        /// <summary> 结算：转雨 </summary>
        public const int SETTLE_RAIN_TUR = 7;
        /// <summary> 结算：查叫/被查叫 </summary>
        public const int SETTLE_CHECK_LISTEN = 8;
        /// <summary> 结算：查花猪 </summary>
        public const int SETTLE_MULTIPLE = 9;
        /** 结算：飘 */
        public const int SETTLE_PIAO = 10;

        // ======================牌型（从牌组合类型角度考虑）======================
        /// <summary> 无叫（不成型得牌） </summary>
        public const int P_UNFORMED = 0;
        /// <summary> 平胡（0番，计根）：标准胡牌类型 </summary>
        public const int P_PING_HU = 1;
        /// <summary> 对对胡（1番，计根）： </summary>
        public const int P_TRIPLET = 2;
        /// <summary> 七对（2番，不计根，无根）：7对牌 </summary>
        public const int P_NEAT = 3;
        /// <summary> 龙七对（3番，不计根）：7对牌，带1根， </summary>
        public const int P_NEAT_R = 4;
        /// <summary> 双龙七对（4番，不计根）：7对牌，带2根 </summary>
        public const int P_NEAT_R1 = 5;
        /// <summary> 三龙七对（5番，不计根）：7对牌，带3根 </summary>
        public const int P_NEAT_R2 = 6;

        // ======================额外加番类型======================
        /// <summary> 抢杠（1番） </summary>
        public const int A_ROB = 1;
        /// <summary> 自摸（1番或1倍底分） </summary>
        public const int A_SELF = 1 << 1;
        /// <summary> 杠上花（1番） </summary>
        public const int A_KONG_HU = 1 << 2;
        /// <summary> 杠上炮（1番） </summary>
        public const int A_KONG_PAO = 1 << 3;
        /// <summary> 中张/断幺九（1番）：不包含面值为1和9的牌 ，也叫断幺九 </summary>
        public const int A_NO_19 = 1 << 4;
        /// <summary> 门清（1番）：除暗杠外，没有碰或杠 </summary>
        public const int A_NO_FIX = 1 << 5;
        /// <summary> 清一色（2番）：所有牌都是同一种花色 </summary>
        public const int A_QING = 1 << 6;
        /// <summary> 将对（2番）：基础牌型为对对胡，且所有碰杠牌，刻子牌，将牌均为面值为2,5,8的牌 </summary>
        public const int A_ONLY_258 = 1 << 7;
        /// <summary> 金钩钩/大单钓（1番）：基础牌为对对胡，所有牌都碰或杠，仅剩一张手牌，大单钓 </summary>
        public const int A_SINGLE = 1 << 8;
        /// <summary> 带幺九/全幺九（2番）：每副牌都带1或9，如果基础牌为对对胡，那么就是全幺九 </summary>
        public const int A_WITH_19 = 1 << 9;
        /// <summary> 海底（1番）：最后一张胡牌或点炮 </summary>
        public const int A_HAIDI = 1 << 10;
        /// <summary> 胡绝张（1番）：要胡的牌仅剩一张 </summary>
        public const int A_JUST_ONE = 1 << 11;
        /// <summary> 有1根： </summary>
        public const int A_ROOT_1 = 1 << 12;
        /// <summary> 有2根： </summary>
        public const int A_ROOT_2 = 1 << 13;
        /// <summary> 有3根（最多出现根数）： </summary>
        public const int A_ROOT_3 = 1 << 14;
        /// <summary> 天胡（3番）： </summary>
        public const int A_TIAN_HU = 1 << 15;
        /// <summary> 地胡（2番）： </summary>
        public const int A_DI_HU = 1 << 16;
        /// <summary> 有4根： </summary>
        public const int A_ROOT_4 = 1 << 17;
        /** 卡心5(1番) */
        public const int A_LSN_MID_5 = 1 << 18;


        /// <summary> 根据牌型和附加类型生成牌型名称信息 </summary>
        public string getPName(int ptype, long atype, Rule rule)
        {
            CharBuffer buf = new CharBuffer();
            getPName(ptype, atype, rule, buf);
            return buf.getString();
        }
        /// <summary> 根据牌型和附加类型生成牌型名称信息 </summary>
        public void getPName(int ptype, long atype, Rule rule, CharBuffer buf)
        {
            switch (ptype)
            {
                case P_UNFORMED:
                    buf.append("无叫");
                    break;
                case P_PING_HU:
                    buf.append("平胡");
                    break;
                case P_TRIPLET:
                    if (rule.getRuleAtribute("enableTriplet2"))
                        buf.append("大对子(2番)");
                    else
                        buf.append("大对子");
                    break;
                case P_NEAT:
                    if (rule.getIntAtribute("fangshu") == 1)
                        buf.append("四对");
                    else
                        buf.append("七对");
                    break;
                case P_NEAT_R:
                    if (rule.getIntAtribute("fangshu") == 1)
                        buf.append("龙四对");
                    else
                        buf.append("龙七对");
                    break;
                case P_NEAT_R1:
                    if (rule.getIntAtribute("fangshu") == 1)
                        buf.append("双龙对");
                    else
                        buf.append("双龙七对");
                    break;
                case P_NEAT_R2:
                    buf.append("三龙七对");
                    break;
                default:
                    buf.append("未知牌型");
                    break;
            }
            if (atype != 0) buf.append("、");
            getAName(atype, buf, rule);
        }

        /// <summary> 获得附加类型名称 </summary>
        public virtual void getAName(long atype, CharBuffer buf, Rule rule)
        {
            if (atype == 0) return;
            if (StatusKit.hasStatus(atype, A_SELF)) buf.append("自摸").append('、');
            else if (type != SETTLE_CHECK_LISTEN)
                buf.append("点炮").append('、');
            if (StatusKit.hasStatus(atype, A_ROOT_1)) buf.append("根x1").append('、');
            if (StatusKit.hasStatus(atype, A_ROOT_2)) buf.append("根x2").append('、');
            if (StatusKit.hasStatus(atype, A_ROOT_3)) buf.append("根x3").append('、');
            if (StatusKit.hasStatus(atype, A_ROOT_4)) buf.append("根x4").append('、');
            if (StatusKit.hasStatus(atype, A_ROB)) buf.append("抢杠").append('、');
            if (StatusKit.hasStatus(atype, A_KONG_HU)) buf.append("杠上花").append('、');
            if (StatusKit.hasStatus(atype, A_KONG_PAO)) buf.append("杠上炮").append('、');
            if (StatusKit.hasStatus(atype, A_NO_19)) buf.append("中张(1番)").append('、');
            if (StatusKit.hasStatus(atype, A_NO_FIX)) buf.append("门清(1番)").append('、');
            if (StatusKit.hasStatus(atype, A_HAIDI)) buf.append("海底(1番)").append('、');
            if (StatusKit.hasStatus(atype, A_SINGLE)) buf.append("金钩钓(1番)").append('、');
            if (StatusKit.hasStatus(atype, A_JUST_ONE)) buf.append("绝张(1番)").append('、');
            if (rule.getRuleAtribute("enableQing"))
                buf.append("清一色(1番)").append('、');
            else if (StatusKit.hasStatus(atype, A_QING)) buf.append("清一色(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_ONLY_258)) buf.append("将对(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_WITH_19)) buf.append("幺九(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_TIAN_HU))
            {
                buf.append("天胡").append("(");
                buf.append(rule.sid == 2010 ? rule.maxPoint : 3);
                buf.append("番)").append("、");
            }
            if (StatusKit.hasStatus(atype, A_DI_HU))
            {
                buf.append("地胡").append("(");
                buf.append(rule.sid == 2010 ? rule.maxPoint : 2);
                buf.append("番)").append("、");
            }

            if (StatusKit.hasStatus(atype, A_LSN_MID_5)) buf.append("卡心5(1番)").append('、');
            buf.moveTop(-1);
        }

        /// <summary>  </summary>
        public MJSettle()
        {
        }

        public MJSettle(int type, int dest, int ptype, int atype, int point)
        {
            this.type = type;
            this.dest = dest;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public MJSettle(int type, int ptype, int atype, int point)
        {
            this.type = type;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public MJSettle(MJSettle acco)
        {
            type = acco.type;
            dest = acco.dest;
            ptype = acco.ptype;
            atype = acco.atype;
            point = acco.point;
            score = acco.score;
        }

        public override string getSettleName(int baseScore, Rule rule)
        {
            switch (type)
            {
                case SETTLE_HU:
                case SETTLE_HU_SELF:
                case SETTLE_HU_ROB:
                    return getPName(ptype, atype, rule);
                case SETTLE_CHECK_LISTEN:
                    return "查叫(" + getPName(ptype, atype, rule) + ")";
                case SETTLE_RAIN_PUB:
                    if (MathKit.abs(score) != baseScore)
                        return "直杠";
                    else
                        return "擦挂";
                case SETTLE_RAIN_PRI:
                    return "暗杠";
                case SETTLE_RAIN_SUP:
                    return "绕杠";
                case SETTLE_RAIN_TUR:
                    return "转雨";
                case SETTLE_MULTIPLE:
                    return "查花猪";
                case SETTLE_PIAO:
                    return "飘";
                default:
                    return "未知";
            }
        }

        public static string getPiaoName(int point)
        {
            switch (point)
            {
                case 1:
                    return "x2";
                case 2:
                    return "二顶三";
                case 3:
                    return "三顶四";
                case 4:
                    return "四顶五";
                default:
                    return "";
            }
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(dest);
            data.writeInt(ptype);
            data.writeInt((int)atype);
            data.writeInt(point);
            data.writeInt(score);
        }

        public override void bytesRead(ByteBuffer data)
        {
            type = data.readInt();
            dest = data.readInt();
            ptype = data.readInt();
            atype = data.readInt();
            point = data.readInt();
            score = data.readInt();
        }

        public override string ToString()
        {
            return "type=" + type + ", dest=" + dest + ", ptype=" + ptype + ", atype=" + atype + ", point=" + point + ", score=" + score;
        }
    }
}
