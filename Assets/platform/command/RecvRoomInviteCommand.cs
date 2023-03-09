using cambrian.common;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接收房间邀请
    /// </summary>
    public class RecvRoomInviteCommand : RecvPort
    {
        public RecvRoomInviteCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_INVITE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            RoomInviteInfo info = new RoomInviteInfo();
            info.bytesRead(data);

            RoomPlayerInvitePanel panel = UnrealRoot.root.getDisplayObject<RoomPlayerInvitePanel>();
            panel.setType(1);
            panel.setRoomInviteInfo(info);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
