using cambrian.common;

namespace platform.mahjong
{
    public class AYMJMatchOverOperate : MJBaseOperate
    {
        public AYMJMatch cloneMatch;


        public AYMJMatchOverOperate(int type, int selfIndex, bool isRelay = false) : base(type, selfIndex, isRelay)
        {
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            cloneMatch = new AYMJMatch(data.readInt());
            cloneMatch.bytesReadAll(data);
        }
    }
}