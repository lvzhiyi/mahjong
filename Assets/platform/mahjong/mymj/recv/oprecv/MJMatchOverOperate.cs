using cambrian.common;

namespace platform.mahjong
{
    public class MJMatchOverOperate : MJBaseOperate
    {
        public MJMatch cloneMatch;


        public MJMatchOverOperate(int type, int selfIndex, bool isRelay = false) : base(type, selfIndex, isRelay)
        {
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            cloneMatch = new MJMatch(data.readInt());
            cloneMatch.bytesReadAll(data);
        }
    }
}