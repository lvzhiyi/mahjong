using basef.rule;
using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开玩法界面
    /// </summary>
    public class OpenArenaSettingPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSettingPanel panel = UnrealRoot.root.getDisplayObject<ArenaSettingPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
