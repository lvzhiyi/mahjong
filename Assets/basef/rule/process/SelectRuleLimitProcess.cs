namespace basef.rule
{
    public class SelectRuleLimitProcess : MouseClickProcess
    {
        private WanFaBar bar;
        private SpotRoomRulePanel panel;
        public override void execute()
        {
            bar = GetComponent<WanFaBar>();
            bar.checkedImg(!bar.status);

            panel = getRoot<SpotRoomRulePanel>();
            Rule rule = panel.getRulesView().getRule();
            if (bar.index_space == 0)
                rule.setIpLimit(bar.status);
            else
                rule.setGpsLimit(bar.status);
        }
    }
}
