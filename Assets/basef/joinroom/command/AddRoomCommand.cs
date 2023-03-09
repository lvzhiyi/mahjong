using cambrian.common;
using cambrian.game;
using scene.game;
using platform;
using platform.spotred;

namespace basef.joinroom
{
    public class AddRoomCommand:UserCommand
    {
        private int index;
        public AddRoomCommand(int index)
        {
            this.id = CommandConst.COMMAND_ROOM_ADD;
            this.index = index;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(index);
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
