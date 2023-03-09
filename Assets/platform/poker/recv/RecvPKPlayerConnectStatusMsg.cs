using cambrian.common;

namespace platform.poker
{
    /// <summary> 斗地主-房间-客户端接收-玩家是否在线 </summary>
    public class RecvPKPlayerConnectStatusMsg : RecvMsgHandle
    {
        public RecvPKPlayerConnectStatusMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            var index = data.readInt();
            var online = data.readBoolean();//true,上线
            long time = data.readLong();
            var panel = PKRoomPanel.Panel;
            if (!panel) return;
            panel.headView.updateOnLine(index, online ? 1 : 0);
            Room.room.players[index].offlinetime = time;
            panel.headView.refresh();
        }
    }
}
