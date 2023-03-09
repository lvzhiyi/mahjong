using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKMStartMatchOperate : PKBaseOperate
    {
        public int firstCard;
        public PDKMStartMatchOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            //PDKMatch.match.firstCard = data.readInt();
            firstCard = data.readInt();
        }
    }
}
