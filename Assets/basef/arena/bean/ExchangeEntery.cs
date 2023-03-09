using cambrian.common;

namespace basef.arena
{
    public class ExchangeEntery:BytesSerializable
    {
        public int uuid;
        /// <summary>
        /// 金豆数
        /// </summary>
        long value;

        public long getValue()
        {
            return value / Arena.PROPORTION;
        }

        public override void bytesRead(ByteBuffer data)
        {
            uuid = data.readInt();
            value = data.readLong();
        }
    }
}
