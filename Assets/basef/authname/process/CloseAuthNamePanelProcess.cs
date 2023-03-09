namespace basef.authname
{
    public class CloseAuthNamePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if(UnrealRoot.root.getDisplayObject<AuthNamePanel>().getVisible())
            {
                UnrealRoot.root.getDisplayObject<AuthNamePanel>().setCVisible(false);
            }
        }
    }
}
