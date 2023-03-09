using basef.arena;
using cambrian.common;
using cambrian.game;
using platform;

namespace basef.arean
{
    public class ArenaQuickGameProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaPanel panel = getRoot<ArenaPanel>();
            ArenaLockRule rule = panel.quickView.getLockRule();
            if (rule != null)
                CommandManager.addCommand(new ArenaQuickGameCommand(Arena.arena.getId(), rule.uuid), back);
            else
                Alert.show("选择规则");
        }

        public void back(object obj)
        {
            Room room = (Room)obj;
            if (room == null) return;
            new ShowWaiteRoomCallBackProcess().execute();
            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
        }
    }
}