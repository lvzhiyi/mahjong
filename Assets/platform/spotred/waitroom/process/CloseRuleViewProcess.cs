namespace platform.spotred.waitroom
{
    public class CloseRuleViewProcess:MouseClickProcess
    {
        public override void execute()
        {
            GetComponent<RuleView>().setVisible(false);
        }
    }
}
