using cambrian.game;

namespace basef.authname
{
    public class OpenUpdatePasswordPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if(User.user.status==0)
            {
                Alert.show("你尚未进行账号绑定，请先绑定！");
                UnrealRoot.root.getDisplayObject<AuthNamePanel>().refresh();
                UnrealRoot.root.getDisplayObject<AuthNamePanel>().setCVisible(true);
            }
            else if(User.user.status==User.STATUS_LINK_MOBILE)
            {
                UnrealRoot.root.getDisplayObject<SetPasswordPanel>().refresh();
                UnrealRoot.root.getDisplayObject<SetPasswordPanel>().setCVisible(true);
            }
            else
            {
                UnrealRoot.root.getDisplayObject<UpdatePasswordPanel>().refresh();
                UnrealRoot.root.getDisplayObject<UpdatePasswordPanel>().setCVisible(true);
            }
        }
    }
}
