
using basef.arena;

namespace Assets.basef.arena.arenarule.process
{
    /// <summary>
    /// 打开玩法显示界面
    /// </summary>
    public class OpenArenaRuleWFShowProcess : MouseClickProcess
    {
        ArenaRuleWFShowPanel panel;
        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ArenaRuleWFShowPanel>();
            panel.setData(Arena.arena.fkcSettings.freedomEnable,Arena.arena.fkcSettings.showRules,Arena.arena.fkcSettings.getRules());
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}

