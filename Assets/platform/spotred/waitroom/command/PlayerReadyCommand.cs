using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.spotred.waitroom
{
    public class PlayerReadyCommand:UserCommand
    {
        bool ready;

        int roomid;
        public PlayerReadyCommand(bool ready,int roomid)
        {
            this.id = CommandConst.COMMAND_ROOM_READY;
            this.ready = ready;
            this.roomid = roomid;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeBoolean(this.ready);
        }
    }
}
