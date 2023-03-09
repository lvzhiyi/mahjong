using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.im
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家语音
    /// </summary>
    public class RecvAudioCommand : RecvPort
    {
        public RecvAudioCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_IMMESSAGE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            long userid = data.readLong();
            byte[] bytes = data.readBytes();

            ByteBuffer buffer = new ByteBuffer(bytes);
            string url = buffer.readUTF();

            SoundManager.playVoice(userid, url);
        }
    }
}
