using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaAddExtensionCommand:UserCommand
    {
        private long userid;

        public ArenaAddExtensionCommand(long userid)
        {
            id = CommandConst.PORT_ARENA_CREATE_AGENGT;
            this.userid = userid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(userid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
