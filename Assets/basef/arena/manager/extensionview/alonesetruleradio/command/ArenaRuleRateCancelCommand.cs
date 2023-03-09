using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class ArenaRuleRateCancelCommand:UserCommand
    {
        private long dest;
        public ArenaRuleRateCancelCommand(long dest)
        {
            id = CommandConst.PORT_ARENA_RULE_RATE_CANCEL_SPECIAL;
            this.dest = dest;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(dest);
        }

        public override void bytesRead(ByteBuffer data)
        {
            RebateList list = new RebateList();
            list.bytesRead(data);
            callbackobj = list;
        }
    }
}
