using cambrian.common;

namespace basef.arena
{
    public class Bill:BytesSerializable
    {
        /** 单据流水号 */
        public long uuid;
        /** 单据类型 */
        public int type;
        /** 单据来源，归属（例如：用户ID,休闲场ID等） */
        public long source;
        /// <summary>
        /// 单据数值
        /// </summary>
        protected long value;
        /** 单据创建时间 */
        public long time;
        /** 单据附加信息 */
        public string info;
    }
}
