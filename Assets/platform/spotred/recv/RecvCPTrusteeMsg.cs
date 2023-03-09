using cambrian.common;
using platform.spotred.room;

namespace platform.spotred
{
    public class RecvCPTrusteeMsg : RecvMsgHandle
    {
        public RecvCPTrusteeMsg()
        {
            gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            if (Room.room == null) return;
            int index = data.readInt();//哪个人
            bool trustee = data.readBoolean();//托管or取消托管

            MatchPlayer[] players = Room.room.getPlayers();
            players[index].setTrustee(trustee);

            SpotRoomPanel panel = UnrealRoot.root.checkDisplayObject<SpotRoomPanel>();
            if (panel != null)
            {
                panel.setTrustee(index, trustee);
            }
        }
    }
}
