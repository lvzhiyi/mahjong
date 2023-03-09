using cambrian.common;
using cambrian.game;
using scene.game;

/// <summary>
/// 获取赛场可换桌列表
/// </summary>
namespace basef.arena
{
    public class ArenaChangeRoomCommand : UserCommand
    {
        private int roomid;

        private long createtime;

        public ArenaChangeRoomCommand(int roomid, long createtime)
        {
            id = CommandConst.PORT_ARENA_ROOM_CHANGE_ROOM;
            this.roomid = roomid;
            this.createtime = createtime;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeLong(createtime);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data;
        }
    }
}
