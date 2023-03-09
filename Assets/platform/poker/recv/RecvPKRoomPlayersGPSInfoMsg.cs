using cambrian.common;
using platform.spotred;

namespace platform.poker
{
    /// <summary> 斗地主-玩家GPS信息 </summary>
    public class RecvPKRoomPlayersGPSInfoMsg : RecvMsgHandle
    {
        public RecvPKRoomPlayersGPSInfoMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
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
