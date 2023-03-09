using cambrian.common;
using cambrian.game;
using scene.game;

namespace platform
{
    public class AgainArenaRoomCommand:UserCommand
    {
        int cacheid;

        public AgainArenaRoomCommand(int cacheid)
        {
            id = CommandConst.PORT_ARENA_AGAIN_ROOM;
            this.cacheid = cacheid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(cacheid);
        }
        public override void bytesRead(ByteBuffer data)
        {
            Room.instance();
            Room.room.bytesRead(data);
            new ShowWaiteRoomCallBackProcess().execute();
        }

    }
}
