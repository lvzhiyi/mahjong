using cambrian.common;

namespace platform.spotred.room
{
    public class OverOperate:BaseOperate
    {
        public int selfIndex;

        public CPMatch scene;

        public OverOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui,isRelay)
        {
            this.selfIndex = selfIndex;
        }

        public override void bytesRead(ByteBuffer data)
        {
            scene = new CPMatch(data.readInt());
            scene.bytesReadAll(data);
            bool b = data.readBoolean();
            index = RecvOverProcess.NORMAL_OVER; //某人无牌可出
            if (b) index = data.readInt();
        }

        public override long getWaittime()
        {
            return base.getWaittime();
        }
    }
}
