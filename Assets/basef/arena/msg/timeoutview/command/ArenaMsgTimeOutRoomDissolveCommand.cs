using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaMsgTimeOutRoomDissolveCommand : UserCommand
    {
        long uuid;

        private int type;

        public ArenaMsgTimeOutRoomDissolveCommand(long uuid,int type)
        {
            this.id = CommandConst.PORT_ARENA_HANDLE_EVENT;
            this.uuid = uuid;
            this.type = type;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeInt(type);
            data.writeLong(uuid);
            data.writeBoolean(true);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
