using cambrian.common;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接收房间再来一局
    /// </summary>
    public class RecvRoomPlayerAginCommand : RecvPort
    {
        public RecvRoomPlayerAginCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_AGAIN;
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            RoomAgainGame info = new RoomAgainGame();
            info.bytesRead(data);
            RoomPlayerInvitePanel panel = UnrealRoot.root.getDisplayObject<RoomPlayerInvitePanel>();
            panel.setType(2);
            panel.setRoomAgainGame(info);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
