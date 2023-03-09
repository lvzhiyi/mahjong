using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaRemoveMemberCommand:UserCommand
    {
        private long arenaid;

        private long destid;

        public ArenaRemoveMemberCommand(long arenaid,long destid)
        {
            id = CommandConst.PORT_ARENA_MEMBER_REMOVE;
            this.arenaid = arenaid;
            this.destid = destid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(destid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }
    }
}
