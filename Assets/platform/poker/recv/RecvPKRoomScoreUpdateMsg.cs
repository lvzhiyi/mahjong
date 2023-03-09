using cambrian.common;

namespace platform.poker
{
    public class RecvPKRoomScoreUpdateMsg : RecvMsgHandle
    {
        public RecvPKRoomScoreUpdateMsg()
        {
            this.gamePlatform = GamePlatform.PK_GAME;
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
            if (len > 0)
            {
                PKRoomPanel.Panel.headView.updateSocre();
            }
        }
    }
}
