using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.poker
{
    public class PKRoomDisbandCommand : UserCommand
    {
        bool b; //是否同意解散房间
        public PKRoomDisbandCommand(bool b)
        {
            this.id = CommandConst.COMMAND_ROOM_DISBAND;
            this.b = b;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            base.bytesWrite(data);
            data.writeBoolean(this.b);
        }
    }
}
