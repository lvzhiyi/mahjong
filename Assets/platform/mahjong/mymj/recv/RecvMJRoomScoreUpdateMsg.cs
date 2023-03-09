using cambrian.common;

namespace platform.mahjong
{
    public class RecvMJRoomScoreUpdateMsg : RecvMsgHandle
    {
        public RecvMJRoomScoreUpdateMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int len = data.readInt();
            for (int i = 0; i < len; i++)
            {
                long score = data.readLong();
                if (Room.room != null)
                    Room.room.getSpotRedCount().getIndexCount(i).score = score;
            }
        }
    }
}
