namespace platform
{
    public class ShowRuleViewProces:MouseClickProcess
    {
        public RuleView view;

        public override void execute()
        {
            this.view.refresh();
            this.view.setVisible(true);
        }
    }
}
