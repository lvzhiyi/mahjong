using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKMShowCardOperate : PKBaseOperate
    {
        public PDKCardInfo cardsinfo;

        public int index;

        public PDKMShowCardOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            cardsinfo = new PDKCardInfo(index);
            cardsinfo.bytesRead(data);
        }
    }
}
