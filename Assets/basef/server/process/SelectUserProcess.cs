using cambrian.game;
using scene.game;

namespace basef.server
{
    /// <summary>
    /// 选择用户 
    /// </summary>
    public class SelectUserProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Develop)
            {
                User user = this.GetComponent<UserBar>().user;
                ((SpotRedRoot)UnrealRoot.root).onAddress(user);
            }
            else if (SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Beta)
            {
                GameLoadingPanel panel = UnrealRoot.root.getDisplayObject<GameLoadingPanel>();
                panel.showWeiXinBtn();
                panel.hideLoading();
                ((SpotRedRoot)UnrealRoot.root).onAddress(new User());
                this.root.setVisible(false);
            }
            else
            {
                this.root.setVisible(false);
            }
        }
    }
}
