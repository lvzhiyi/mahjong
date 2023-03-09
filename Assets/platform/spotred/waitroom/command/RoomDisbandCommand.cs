using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 解散房间
    /// </summary>
    public class RoomDisbandCommand:UserCommand
    {
        bool b; //是否同意解散房间
        /// <summary>
        /// 解散类型
        /// </summary>
        private int type;

        int roomid;

        public RoomDisbandCommand(bool b,int type,int roomid)
        {
            id = CommandConst.COMMAND_ROOM_DISBAND;
            this.b = b;
            this.type = type;
            this.roomid = roomid;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeInt(roomid);
            data.writeInt(type);
            data.writeBoolean(b);
        }
    }
}
