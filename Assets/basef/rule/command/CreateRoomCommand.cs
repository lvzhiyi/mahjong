using cambrian.common;
using cambrian.game;
using scene.game;
using platform;

namespace basef.rule
{
    public class CreateRoomCommand: UserCommand
    {
        Rule rule;

        public CreateRoomCommand(Rule rule)
        {
            id = CommandConst.COMMAND_ROOM_CTEATE;
            this.rule = rule;
        }
        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(AccessPlatformMessage.n);
            data.writeInt(AccessPlatformMessage.e);
            rule.bytesWrite(data);
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
