using basef.rule;
using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开玩法界面
    /// </summary>
    public class OpenArenaWfPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaWfPanel panel = UnrealRoot.root.getDisplayObject<ArenaWfPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
