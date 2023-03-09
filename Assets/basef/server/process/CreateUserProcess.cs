using cambrian;
using cambrian.game;
using scene.game;

namespace basef.server
{
    public class CreateUserProcess:MouseClickProcess
    {
        public override void execute()
        {
            if (SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Develop)
            {
                ((SpotRedRoot)UnrealRoot.root).onAddress(new User());
            }
            else if (SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Beta)
            {
                GameLoadingPanel panel = UnrealRoot.root.getDisplayObject<GameLoadingPanel>();
                panel.showWeiXinBtn();
                panel.hideLoading();
            }

            UnrealRoot.root.getDisplayObject<UsersPanel>().setVisible(false);
        }
    }
}
