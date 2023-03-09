using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 解散竞赛场房间
    /// </summary>
    public class DisbandArenaRoomCommand : UserCommand
    {
        private long arenaid;

        private int roomid;

        private long createtime;

        public DisbandArenaRoomCommand(long arenaid, int roomid, long createtime)
        {
            id = CommandConst.PORT_ARENA_ROOM_DISBAND;
            this.arenaid = arenaid;
            this.roomid = roomid;
            this.createtime = createtime;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeInt(roomid);
            data.writeLong(createtime);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data==null||data.length == 0) return;
            this.callbackobj = data;
        }
    }
}
