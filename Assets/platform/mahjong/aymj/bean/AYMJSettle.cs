using basef.rule;
using cambrian.common;

namespace platform.mahjong
{
    public class AYMJSettle : MJSettle
    {
        /** 一条龙（1番）：同一种花色组成1~9 */
        public const int A_SHUN_19 = 1 << 19;
        /** 板板高（1番）：一组三连对 */
        public const int A_BBG_1 = 1 << 20;
        /** 板板高（2番）：两组三连对 */
        public const int A_BBG_2 = 1 << 21;

        /// <summary>  </summary>
        public AYMJSettle()
        {
        }

        public AYMJSettle(int type, int dest, int ptype, int atype, int point)
        {
            this.type = type;
            this.dest = dest;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public AYMJSettle(int type, int ptype, int atype, int point)
        {
            this.type = type;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public AYMJSettle(AYMJSettle acco)
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


        /// <summary> 获得附加类型名称 </summary>
        public override void getAName(long atype, CharBuffer buf, Rule rule)
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
            if (StatusKit.hasStatus(atype, A_JUST_ONE)) buf.append("独张(1番)").append('、');
            if (rule.getRuleAtribute("enableQing"))
                buf.append("清一色(1番)").append('、');
            else if (StatusKit.hasStatus(atype, A_QING)) buf.append("清一色(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_ONLY_258)) buf.append("将对(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_WITH_19)) buf.append("幺九(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_SHUN_19)) buf.append("一条龙(1番)").append('、');
            if (StatusKit.hasStatus(atype, A_BBG_1)) buf.append("板板高x1").append('、');
            if (StatusKit.hasStatus(atype, A_BBG_2)) buf.append("板板高x2").append('、');
            if (StatusKit.hasStatus(atype, A_TIAN_HU))
            {
                buf.append("天胡").append("(");
                buf.append("3");
                buf.append("番)").append("、");
            }
            if (StatusKit.hasStatus(atype, A_DI_HU))
            {
                buf.append("地胡").append("(");
                buf.append("2");
                buf.append("番)").append("、");
            }

            if (StatusKit.hasStatus(atype, A_LSN_MID_5)) buf.append("夹心5(1番)").append('、');
            buf.moveTop(-1);
        }

        /** 根据牌型和加颗项，计算并返回总计颗数 */
        public static int getPointFromAType(int ptype, long atype, Rule rule)
        {
            //  Debug.Log("胡牌信息：" + getPName(ptype, atype));
            int point = 0;
            switch (ptype)
            {
                case P_TRIPLET:
                    point += rule.getRuleAtribute("enableTriplet2") ? 2 : 1;
                    break;
                case P_NEAT:
                    point += 2;
                    break;
                case P_NEAT_R:
                    point += 3;
                    break;
                case P_NEAT_R1:
                    point += 4;
                    break;
                case P_NEAT_R2:
                    point += 5;
                    break;
            }
            if (atype == 0) return point;
            if (StatusKit.hasStatus(atype, A_ROOT_1)) point += 1;
            if (StatusKit.hasStatus(atype, A_ROOT_2)) point += 2;
            if (StatusKit.hasStatus(atype, A_ROOT_3)) point += 3;
            if (StatusKit.hasStatus(atype, A_ROOT_4)) point += 4;
            if (StatusKit.hasStatus(atype, A_ROB)) point += 1;
            if (StatusKit.hasStatus(atype, A_SELF) && rule.getIntAtribute("zimoAddtion") == 2) point += 1;
            if (StatusKit.hasStatus(atype, A_KONG_HU)) point += 1;
            if (StatusKit.hasStatus(atype, A_KONG_PAO)) point += 1;
            if (StatusKit.hasStatus(atype, A_NO_19)) point += 1;
            if (StatusKit.hasStatus(atype, A_NO_FIX)) point += 1;
            if (StatusKit.hasStatus(atype, A_HAIDI)) point += 1;
            if (StatusKit.hasStatus(atype, A_SINGLE)) point += 1;
            if (StatusKit.hasStatus(atype, A_JUST_ONE)) point += 1;
            if (StatusKit.hasStatus(atype, A_SHUN_19)) point += 1;
            if (StatusKit.hasStatus(atype, A_QING))
            {
                if (rule.getIntAtribute("fangshu") == 1)// 一门时，清一色加1番
                    point += 1;
                else
                    point += 2;
            }
            if (StatusKit.hasStatus(atype, A_ONLY_258)) point += 2;
            if (StatusKit.hasStatus(atype, A_WITH_19)) point += 2;
            if (StatusKit.hasStatus(atype, A_LSN_MID_5)) point += 1;
            if (StatusKit.hasStatus(atype, A_BBG_1)) point += 1;
            if (StatusKit.hasStatus(atype, A_BBG_2)) point += 2;

            //if (point > rule.maxPoint) return rule.maxPoint;
            return point;
        }
    }
}
