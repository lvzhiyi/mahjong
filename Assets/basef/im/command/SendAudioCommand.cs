using cambrian.common;
using scene.game;

namespace basef.im
{
    public class SendAudioCommand: SendCommand
    {
        private string url;
        public SendAudioCommand(string url)
        {
            this.id = CommandConst.COMMAND_IM_SENDAUDIO;
            this.url = url;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            ByteBuffer buffer=new ByteBuffer();
            buffer.writeUTF(url);
            data.write(buffer.toArray());
        }
    }
}
