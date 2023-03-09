using cambrian.common;
using scene.game;

namespace basef.im
{
    public class IMSendTextCommand: SendCommand
    {
        private string text;
        public IMSendTextCommand(string text)
        {
            this.id = CommandConst.COMMAND_IM_SENDMESSAGE;
            this.text = text;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeUTF(text);
        }
    }
}
