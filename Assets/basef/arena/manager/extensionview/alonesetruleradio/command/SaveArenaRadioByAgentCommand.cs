using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class SaveArenaRadioByAgentCommand:UserCommand
    {
        private long dest;

        private RebateList list;

        public SaveArenaRadioByAgentCommand(long dest,RebateList list)
        {
            id = CommandConst.PORT_ARENA_SETTING_AGENT_ALONE_RATE;
            this.dest = dest;
            this.list = list;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(dest);
            list.bytesWrite(data);
        }

        public override void bytesRead(ByteBuffer data)
        {
            RebateList list = new RebateList();
            list.bytesRead(data);
            callbackobj = list;
        }
    }
}
