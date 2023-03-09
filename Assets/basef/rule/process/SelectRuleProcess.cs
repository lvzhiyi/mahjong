namespace basef.rule
{
    public class SelectRuleProcess : MouseClickProcess
    {
        private WanFaBar bar;
        private SpotRoomRulePanel panel;
        public override void execute()
        {
            bar = GetComponent<WanFaBar>();
            bar.checkedImg(!bar.status);

            panel = getRoot<SpotRoomRulePanel>();

            Rule rule = panel.getRulesView().getRule();
            if (rule.sid == 1009 && (bar.index_space == 2 || bar.index_space == 5)) //广元市区
            {
                if (bar.index_space == 2 && bar.status)
                {
                    panel.refreshWanFa(false, 5);
                    ((WanFaBar) panel.rulesView.wanfa.bars[5]).checkedImg(false);
                }
                else if (bar.index_space == 5 && bar.status)
                {
                    panel.refreshWanFa(false, 2);
                    ((WanFaBar) panel.rulesView.wanfa.bars[2]).checkedImg(false);
                }
            }
            panel.refreshWanFa(bar.status, bar.index_space);
        }
    }
}
