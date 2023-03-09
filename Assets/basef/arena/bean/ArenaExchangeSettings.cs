using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 奖章兑换设置
    /// </summary>
    public class ArenaExchangeSettings : BytesSerializable
    {
        public ArrayList<ExchangeEntery> list;

        public override void bytesRead(ByteBuffer data)
        {
            data.readInt();
            int len = data.readInt();
            list = new ArrayList<ExchangeEntery>(len);
            ExchangeEntery entry;
            for (int i = 0; i < len; i++)
            {
                entry = new ExchangeEntery();
                entry.bytesRead(data);
                this.list.add(entry);
            }
        }
    }
}
