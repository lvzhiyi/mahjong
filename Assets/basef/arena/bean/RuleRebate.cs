using cambrian.common;

namespace basef.arena
{
    public class RuleRebate:BytesSerializable
    {
        /** 锁定规则id */
        public int rule;
        /** 返利比率 */
        public int rate;
        /// <summary>
        /// 上限
        /// </summary>
        public int subRate;

        public override void bytesRead(ByteBuffer data)
        {
            this.rule = data.readInt();
            this.rate = data.readInt();
            this.subRate = data.readInt();
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(rule);
            data.writeInt(rate);
            data.writeInt(subRate);
        }
    }
}
