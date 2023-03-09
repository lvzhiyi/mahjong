using cambrian.common;

namespace basef.arena
{
    /// <summary> 兑换记录 数据 </summary>
    public class ArenaExchangeRecordContentData : BytesSerializable
    {
        /// <summary> 兑换奖章数量 </summary>
        public int medal = 0;

        /// <summary> 消耗金豆数量 </summary>
        public int gold = 0;

        /// <summary> 兑换状态 </summary>
        public int status = 0;

        /// <summary> 兑换时间 </summary>
        public long etime = 0;
    }
}
