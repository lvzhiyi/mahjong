using cambrian.common;
using scene.game;

namespace basef.player
{
    public class RecvPlayerMedalCommand:RecvPort
    {

        public RecvPlayerMedalCommand()
        {
            id = RecvConst.PORT_CLIENT_MEDAL_UPDATE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            Player.player.medal = (int)data.readLong();
        }
    }
}
