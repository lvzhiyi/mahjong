using basef.authname;
using cambrian.common;
using cambrian.game;

namespace basef.lobby
{
    public class OpenAuthNamePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            //if (!StatusKit.hasStatus(User.user.status,User.STATUS_LINK_MOBILE))
            //{
            //    UnrealRoot.root.getDisplayObject<AuthNamePanel>().refresh();
            //    UnrealRoot.root.getDisplayObject<AuthNamePanel>().setCVisible(true);
            //}
            //else if (StatusKit.hasStatus(User.user.status,User.STATUS_LINK_MOBILE) && !StatusKit.hasStatus(User.user.status,User.STATE_UPDATE_PWD))
            //{
            //    UnrealRoot.root.getDisplayObject<SetPasswordPanel>().refresh();
            //    UnrealRoot.root.getDisplayObject<SetPasswordPanel>().setCVisible(true);
            //}
            //else if (StatusKit.hasStatus(User.user.status,User.STATUS_LINK_MOBILE) && StatusKit.hasStatus(User.user.status,User.STATE_UPDATE_PWD))
            //{
            //    UnrealRoot.root.getDisplayObject<UpdatePasswordPanel>().refresh();
            //    UnrealRoot.root.getDisplayObject<UpdatePasswordPanel>().setCVisible(true);
            //}

            // hideTxsmrz();

            if (!StatusKit.hasStatus(User.user.status, User.STATE_UPDATE_PWD))
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
