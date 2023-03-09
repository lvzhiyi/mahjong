using cambrian.game;
using scene.game;

namespace basef.setting
{
    public class LogoutProcess : MouseClickProcess
    {
        public override void execute()
        {
            Alert.confirmShow("是否退出登录？", (trans) =>
            {
                if (User.user != null)
                    User.logout(User.user);
                User.user = null;
                SpotRedRoot.roots.closePanel();
                SpotRedRoot.roots.loading.refresh();
                SpotRedRoot.roots.loading.setVisible(true);
                SpotRedRoot.roots.takeWhStatus();
            });
        }
    }
}
