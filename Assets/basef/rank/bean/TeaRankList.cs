using cambrian.common;

namespace basef.rank
{
    public class TeaRankList:BytesSerializable
    {
        public TeaRank[] ranks;
        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ranks=new TeaRank[len];
            for (int i = 0; i < len; i++)
            {
                ranks[i]=new TeaRank();
                ranks[i].bytesRead(data);
            }
        }
    }
}
