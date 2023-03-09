using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKMCancelOperate : PKBaseOperate
    {
        public int type;

        public int index;

        public PDKMCancelOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            type = data.readInt();
            operateData.waittime = 1000;
        }
    }
}
