namespace basef.arena
{
    public class ArenaAddTicketLevelProcess:MouseClickProcess
    {
        public override void execute()
        {
            //ArenaRulePanel panel= getRoot<ArenaRulePanel>();
            ArenaWfNextPanel panel= getRoot<ArenaWfNextPanel>();
            panel.indulgeView.rule.addTicketLevel();
            panel.indulgeView.refresh1();
        }
    }
}
