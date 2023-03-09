using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开赛场日志面板 </summary>
    public class OpenArenaLogPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            
            CommandManager.addCommand(new GetArenaLogCommand(0), back);
        }
        private void back(object obj)
        {
            if (obj == null) return;
            ArenaLogPanel panel = UnrealRoot.root.getDisplayObject<ArenaLogPanel>();
            panel.setData((ArenaLog[])obj);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
