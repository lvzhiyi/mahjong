namespace basef.arena
{
    public class ShowArenaRuleViewProcess:MouseClickProcess
    {
        public override void execute()
        {
            getRoot<ArenaRulePanel>().showRulesView();
        }
    }
}
