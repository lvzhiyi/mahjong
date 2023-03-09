using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKAnYueUpdateScoreOperate: PKBaseOperate
    {
        public int index;
        public PDKAnYueUpdateScoreOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
        }
    }
}
