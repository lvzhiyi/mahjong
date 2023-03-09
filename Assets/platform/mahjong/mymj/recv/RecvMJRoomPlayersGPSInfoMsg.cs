using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class RecvMJRoomPlayersGPSInfoMsg : RecvMsgHandle
    {
        public RecvMJRoomPlayersGPSInfoMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            int longitude = data.readInt();
            int latitude = data.readInt();

            if (Room.room != null)
            {
                MatchPlayer player = Room.room.players[index];
                player.gps_longitude = longitude;
                player.gps_latitude = latitude;
            }
        }
    }
}
