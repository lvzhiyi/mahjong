namespace basef.arena
{
    public class SelectChangUIRuleProcess : MouseClickProcess
    {
        public override void execute()
        {
            ChangUIRuleBar bar = this.transform.GetComponent<ChangUIRuleBar>();
            ChangUIRuleView view = this.transform.parent.parent.parent.parent.GetComponent<ChangUIRuleView>();
            ArenaLockRule rule = bar.rule;
            view.selectRule(rule.uuid,rule.rule.getPlatFormValue());
        }
    }
}
