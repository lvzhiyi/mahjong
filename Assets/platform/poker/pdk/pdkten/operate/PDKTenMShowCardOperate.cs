using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKTenMShowCardOperate : PKBaseOperate
    {
        public PDKTenCardInfo cardsinfo;

        public int index;

        public PDKTenMShowCardOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            cardsinfo = new PDKTenCardInfo(index);
            cardsinfo.bytesRead(data);
        }
    }
}
