using basef.rule;

namespace basef.arena
{
    public class SelectArenRuleGameTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            RuleGameTypeBar bar = transform.GetComponent<RuleGameTypeBar>();
            if (bar.checkbox.getState()) return;
            bar.selected();
            getRoot<ArenaRulePanel>().showRule(bar.getGameType(), bar.index_space);
        }
    }
}
