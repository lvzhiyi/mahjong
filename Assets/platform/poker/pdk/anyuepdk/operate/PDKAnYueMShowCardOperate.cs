using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKAnYueMShowCardOperate : PKBaseOperate
    {
        public PDKAnYueCardInfo cardsinfo;

        public int index;

        public PDKAnYueMShowCardOperate(OperateData data) : base(data)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            index = data.readInt();
            cardsinfo = new PDKAnYueCardInfo(index);
            cardsinfo.bytesRead(data);
        }
    }
}
