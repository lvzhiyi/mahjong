using basef.authname;
using basef.IDCard;
using cambrian.common;
using cambrian.game;

namespace basef.lobby
{
    public class OpenIDCardPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (!StatusKit.hasStatus(User.user.status, User.STATE_SET_SFRZ))
            {
                UnrealRoot.root.getDisplayObject<IDCardPanel>().refresh();
                UnrealRoot.root.getDisplayObject<IDCardPanel>().setCVisible(true);
            }
            else
            {
                UnrealRoot.root.getDisplayObject<SpotLobbyPanel>().refresh();
            }
        }
    }
}
