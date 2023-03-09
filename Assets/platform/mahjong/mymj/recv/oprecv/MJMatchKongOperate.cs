using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 暗杠
    /// </summary>
    public class MJMatchKongOperate:MJBaseOperate
    {
        /// <summary>
        /// 明杠
        /// </summary>
        public const int KONG_PUB = 1;
        /// <summary>
        /// 杠牌类型：暗杠
        /// </summary>
        public const int KONG_PRI = 2;
        /** 杠牌类型：补杠（巴杠）成功 */
        public const int KONG_SUP = 3;
        /** 杠牌类型：补杠（巴杠）等待（等待其他人抢杠，如果其他人取消抢杠，会重新通知一次KONG_SUP类型的消息） */
        public const int KONG_SUP_WAIT_ROB = 4;
        /** 杠牌类型：补杠（巴杠）继续（等待其他人抢杠，如果其他人取消抢杠，会重新通知一次KONG_SUP类型的消息） */
        public const int KONG_SUP_EXIT_ROB = 5;

        /** 杠牌者 */
        public int index;
        /** 杠的哪张牌 */
        public int card;
        /// <summary>
        /// 来源
        /// </summary>
        public int from;
        /**
         * 杠牌类型：用于前端判定显示效果
         * 
         * <pre>
         * 下雨：暗杠（其他所有人下雨），明杠（点杠出下雨）
         * 刮风：巴杠成功，其他所有人刮风
         * </pre>
         */
        public int kongType;

        public MJMatchKongOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }


        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            from = data.readInt();
            card = data.readInt();
            kongType = data.readInt();
        }

        public override string ToString()
        {
            return "MJMatchKongOperate,index="+index+",from="+from+",card="+MJCard.getName(card)+",kongType="+kongType;
        }
    }
}
