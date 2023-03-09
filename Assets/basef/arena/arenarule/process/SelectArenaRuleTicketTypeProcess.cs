namespace basef.arena
{
    public class SelectArenaRuleTicketTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRuleTicketTypeBar bar = GetComponent<ArenaRuleTicketTypeBar>();
            if (bar.isSelect) return;
            ArenaIndulgeView view= transform.parent.parent.parent.parent.parent.GetComponent<ArenaIndulgeView>();
            int type= bar.index_space == 0 ? ArenaLockRule.TYPE_STEP : ArenaLockRule.TYPE_AA;
            view.ticketsType = type;
            view.rule.mpType = type;
            view.refreshTicketType(type);
            view.verticalPosition = 0;
            view.refreshShow();
        }
    }
}
