using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 报杠
    /// </summary>
    public class AYMJMatchBaoKongOperate : MJBaseOperate
    {
        public int index;

        public int[] cards;

        public AYMJMatchBaoKongOperate(int type, int selfIndex, bool isreplay = false) : base(type, selfIndex, isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            index = data.readInt();
            cards = data.readInts();
        }
    }
}
