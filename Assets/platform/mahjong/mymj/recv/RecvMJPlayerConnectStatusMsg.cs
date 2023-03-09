using basef.player;
using cambrian.common;

namespace platform.mahjong
{
    public class RecvMJPlayerConnectStatusMsg : RecvMsgHandle
    {
        public RecvMJPlayerConnectStatusMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            bool online = data.readBoolean();//true,上线
            long time = data.readLong();
            if (Room.room != null)
            {
                if (Room.room.players[index] != null)
                    Room.room.players[index].player.setStatus(SimplePlayer.STATUS_ONLINE, online);
                else
                    return;
                Room.room.players[index].offlinetime = time;

                MahJongPanel roomPanel = MahJongPanel.getPanel();
                if (Room.room.players[index] != null)
                {
                    int banker = -1;
                    if (roomPanel.gameView.getVisible() && MJMatch.match != null)
                        banker = MJMatch.match.banker;

                    roomPanel.getHeadView().showMatchPlayers(banker);
                }
            }
        }
    }
}
