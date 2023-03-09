using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class UpdateArenaRadioCommand:UserCommand
    {
        private RebateList list;
        public UpdateArenaRadioCommand(RebateList list)
        {
            id = CommandConst.PORT_ARENA_SETTING_AGENT_RATE;
            this.list = list;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
           list.bytesWrite(data);
        }

        public override void bytesRead(ByteBuffer data)
        {
            RebateList list=new RebateList();
            list.bytesRead(data);
            callbackobj = list;
        }
    }
}
