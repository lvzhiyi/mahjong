using basef.rule;
using cambrian.common;

namespace platform.mahjong
{
    public class MYMJSettle: MJSettle
    {
        ///** 躺牌（1番） */
        public static int A_TANG = 1 << 18;
        ///** 对躺（2番） */
        public static int A_TANG_2 = 1 << 19;


        /// <summary>  </summary>
        public MYMJSettle()
        {
        }

        public MYMJSettle(int type, int dest, int ptype, int atype, int point)
        {
            this.type = type;
            this.dest = dest;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public MYMJSettle(int type, int ptype, int atype, int point)
        {
            this.type = type;
            this.ptype = ptype;
            this.atype = atype;
            this.point = point;
        }

        public MYMJSettle(MYMJSettle acco)
        {
            type = acco.type;
            dest = acco.dest;
            ptype = acco.ptype;
            atype = acco.atype;
            point = acco.point;
            score = acco.score;
        }

        public override void getAName(long atype, CharBuffer buf,Rule rule)
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
            if (StatusKit.hasStatus(atype, A_QING)) buf.append("清一色(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_ONLY_258)) buf.append("将对(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_WITH_19)) buf.append("幺九(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_TIAN_HU)) buf.append("天胡(3番)").append('、');
            if (StatusKit.hasStatus(atype, A_DI_HU)) buf.append("地胡(2番)").append('、');
            if (StatusKit.hasStatus(atype, A_TANG)) buf.append("躺").append('、');
            if (StatusKit.hasStatus(atype, A_TANG_2)) buf.append("对躺").append('、');
            buf.moveTop(-1);
        }
    }
}
