using basef.player;
using cambrian.common;

namespace platform.poker
{
    /// <summary> 房间-客户端接收-玩家准备(广播) </summary>
    public class RecvPKPlayerReadyMsg : RecvMsgHandle
    {
        public RecvPKPlayerReadyMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            var index = data.readInt();
            var ready = data.readBoolean();
            var player = Room.room.getPlayers()[index];
            if (player == null) return;
            player.setReady(ready);
            if (player.userid == Player.player.userid)
            {
                PKRoomPanel.Panel.topView.setMuitplier(0);
                PKRoomPanel.Panel.refreshWaitView();
            }
            else PKRoomPanel.Panel.headView.refresh();
        }
    }
}
