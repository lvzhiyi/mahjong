using cambrian.common;
using scene.game;

namespace basef.im
{
    public class SendQuickMsgCommand: SendCommand
    {
        int type;

        int value;

        public SendQuickMsgCommand(int type, int value)
        {
            this.id = CommandConst.COMMAND_IM_QUICKMESSAGE;
            this.type = type;
            this.value = value;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeInt(type);
            data.writeInt(value);
        }
    }
}
