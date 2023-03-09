using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开限制组界面
    /// </summary>
    public class OpenArenaLimitDeskPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaMutexCommand(Arena.arena.getId()),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaLimitDeskPanel panel = UnrealRoot.root.getDisplayObject<ArenaLimitDeskPanel>();
            panel.setData((ArenaMutex[])obj);
            panel.refresh();
            panel.setCVisible(true);
        }

        
    }
}
