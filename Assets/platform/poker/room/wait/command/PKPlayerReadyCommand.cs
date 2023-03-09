using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform.poker
{
    public class PKPlayerReadyCommand : UserCommand
    {
        bool ready;

        int roomid;

        public PKPlayerReadyCommand(bool ready,int roomid)
        {
            this.id = CommandConst.COMMAND_ROOM_READY;
            this.ready = ready;
            this.roomid = roomid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(roomid);
            data.writeBoolean(ready);
        }
    }
}
