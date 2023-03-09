using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event:BytesSerializable
    {
        ///<summary> 事件状态：等待中 </summary>
        public const int STATUS_WAIT = 0;
        ///<summary> 事件状态：已同意 </summary>
        public const int STATUS_AGREE = 1;
        ///<summary> 事件状态：已拒绝 </summary>
        public const int STATUS_REFUSE = 2;
        ///<summary> 事件状态：处理中 </summary>
        public const int STATUS_SOLVING = 4;

        ///<summary> 单据流水号 </summary>
        public long uuid;
        ///<summary> 单据类型 </summary>
        public int type;
        ///<summary> 竞赛场id </summary>
        public long arenaid;
        ///<summary> 单据创建时间 </summary>
        public long time;
        /// <summary>
        /// 状态
        /// </summary>
        public int status;
    }
}
