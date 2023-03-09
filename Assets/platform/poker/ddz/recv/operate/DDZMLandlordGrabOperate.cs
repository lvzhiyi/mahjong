using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class DDZMLandlordGrabOperate : PKBaseOperate
    {
        public int index;

        public int multiple;

        public DDZMLandlordGrabOperate(OperateData data) : base(data) { }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            multiple = data.readInt();
        }
    }
}
