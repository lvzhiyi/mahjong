using cambrian.common;
using cambrian.game;
using platform;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 加入竞赛场房间
    /// </summary>
    public class JoinArenaRoomCommand:UserCommand
    {
        private int roomid;
        public JoinArenaRoomCommand(int roomid)
        {
            this.id = CommandConst.COMMAND_ROOM_ADD;
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
            if (data.length == 0) return;
            Room.instance();
            Room.room.bytesRead(data);
            this.callbackobj = Room.room;
        }
    }
}
