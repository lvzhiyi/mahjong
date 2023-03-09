using basef.arena;

namespace Assets.basef.arena.arenarule.process
{
    /// <summary>
    /// 打开竞赛场规则面板
    /// </summary>
    public class OpenArenRulePanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRulePanel panel = UnrealRoot.root.getDisplayObject<ArenaRulePanel>();
            panel.setRule(null,false);
            panel.refresh();
            panel.setCVisible(true);
            panel.setLastPanel(this.root);
            this.root.setCVisible(false);
        }
    }
}
