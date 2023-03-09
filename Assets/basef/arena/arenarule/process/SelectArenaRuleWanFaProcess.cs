using basef.rule;

namespace basef.arena
{
    public class SelectArenaRuleWanFaProcess:MouseClickProcess
    {
        public override void execute()
        {
            WanFaBar bar = GetComponent<WanFaBar>();
            bar.checkedImg(!bar.status);

            ArenaRulePanel panel = getRoot<ArenaRulePanel>();

            Rule rule = panel.rulesView.getRule();
            if (rule.sid == 1009 && (bar.index_space == 2 || bar.index_space == 5)) //广元市区
            {
                if (bar.index_space == 2 && bar.status)
                {
                    panel.refreshWanFa(false, 5);
                    ((WanFaBar)panel.rulesView.wanfa.bars[5]).checkedImg(false);
                }
                else if (bar.index_space == 5 && bar.status)
                {
                    panel.refreshWanFa(false, 2);
                    ((WanFaBar)panel.rulesView.wanfa.bars[2]).checkedImg(false);
                }
            }
            panel.refreshWanFa(bar.status, bar.index_space);
        }
    }
}
