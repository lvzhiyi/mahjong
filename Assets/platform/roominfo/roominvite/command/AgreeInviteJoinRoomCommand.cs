using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform
{
    /// <summary>
    /// 同意邀请加入房间
    /// </summary>
    public class AgreeInviteJoinRoomCommand : UserCommand
    {
        int roomid;
        public AgreeInviteJoinRoomCommand(int roomid)
        {
            id = CommandConst.PORT_AGREE_INVITE_JOIN_ROOM;
            this.roomid = roomid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeInt(AccessPlatformMessage.n);
            data.writeInt(AccessPlatformMessage.e);
        }

        public override void bytesRead(ByteBuffer data)
        {
            Room.instance();
            Room.room.bytesRead(data);
            new ShowWaiteRoomCallBackProcess().execute();
            callbackobj = Room.room;
        }
    }
}
