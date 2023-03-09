namespace basef.arena
{
    public class ArenDecrTicketLevelProcess:MouseClickProcess
    {
        public override void execute()
        {
            //ArenaRulePanel panel = getRoot<ArenaRulePanel>();
            ArenaWfNextPanel panel = getRoot<ArenaWfNextPanel>();
            int index=transform.parent.GetComponent<ArenaIndulgePointBar>().index_space;
            if (!panel.indulgeView.rule.delteTicketLevel(index))
            {
                Alert.autoShow("无法删除");
            }

            panel.indulgeView.refresh1();
        }
    }
}
