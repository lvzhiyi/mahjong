using cambrian.common;

namespace platform.spotred
{
    public class RecvCPRoomPlayersGPSInfoMsg : RecvMsgHandle
    {
        public RecvCPRoomPlayersGPSInfoMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
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
