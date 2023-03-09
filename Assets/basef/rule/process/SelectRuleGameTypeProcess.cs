namespace basef.rule
{
    public class SelectRuleGameTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            RuleGameTypeBar bar = this.transform.GetComponent<RuleGameTypeBar>();
            if (bar.checkbox.getState()) return;
            bar.selected();
            this.getRoot<SpotRoomRulePanel>().showRule(bar.getGameType(),bar.index_space);
        }
    }
}
