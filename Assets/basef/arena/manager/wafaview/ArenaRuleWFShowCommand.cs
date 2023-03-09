using basef.rule;
using cambrian.common;
using cambrian.game;
using platform;
using scene.game;

namespace basef.arena
{
    public class ArenaRuleWFShowCommand : UserCommand
    {
        /// <summary>竞赛场id</summary>
        private long arenaid;
        /// <summary>自由桌开关 </summary>
        private bool freedomenable;
        /// <summary>显示规则数组 </summary>
        private ArrayList<int> showrules;
        private ArenaLockRule rule;
        public ArenaRuleWFShowCommand(long arenaid, bool freedomenable, ArrayList<int> showrules)
        {
            id = CommandConst.PORT_ARENA_RULE_WF_SHOW;
            this.arenaid = arenaid;
            this.freedomenable = freedomenable;
            this.showrules = showrules;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeBoolean(freedomenable);
            data.writeInt(showrules.count);
            for(int i=0;i<showrules.count;i++)
            {
                data.writeInt(showrules.get(i));
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            callbackobj = data.readBoolean();
        }

    }
}
