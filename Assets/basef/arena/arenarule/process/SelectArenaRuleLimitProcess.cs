using basef.rule;

namespace basef.arena
{
    public class SelectArenaRuleLimitProcess : MouseClickProcess
    {
        private WanFaBar bar;
        private ArenaRulePanel panel;
        public override void execute()
        {
            bar = GetComponent<WanFaBar>();
            bar.checkedImg(!bar.status);

            panel = getRoot<ArenaRulePanel>();
            Rule rule = panel.rulesView.getRule();
            if (bar.index_space == 0)
                rule.setIpLimit(bar.status);
            else
                rule.setGpsLimit(bar.status);
        }
    }
}
