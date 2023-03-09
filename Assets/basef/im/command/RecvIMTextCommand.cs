using cambrian.common;
using scene.game;

namespace basef.im
{
    public class RecvIMTextCommand : RecvPort
    {

        public RecvIMTextCommand()
        {
            this.id = RecvConst.COMMAND_IM_RECV_MESSAGE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);

            IMTextMsg msg = new IMTextMsg();
            msg.bytesRead(data);

            IMQuickMsgManager.showTextMsg(msg);
        }
    }
}
