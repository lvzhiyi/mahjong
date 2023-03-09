namespace basef.rule
{
    public class SelectRuleTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            RuleBar bar= this.transform.GetComponent<RuleBar>();
            bar.selected();
            this.getRoot<SpotRoomRulePanel>().showView(bar.getRule(),bar.index_space);
        }
    }
}
