using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class DDZMCallScoreOperate : PKBaseOperate
    {
        public int index;

        public int score;

        public int maxscore;

        public DDZMCallScoreOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            score = data.readInt();
            maxscore = data.readInt();
        }
    }
}
