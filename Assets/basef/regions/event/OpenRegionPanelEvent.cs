using cambrian.game;
using scene.game;

namespace basef.regions
{
    public class OpenRegionPanelEvent : MouseClickProcess
    {
        public override void execute()
        {

            var panel = UnrealRoot.root.getDisplayObject<RegionsPanel>();

            if (User.user != null)
                User.logout(User.user);

            User.user = null;
            var p = UnrealRoot.root.checkDisplayObject<GameLoadingPanel>();
            if (p != null)
                panel.setAction(SpotRedRoot.roots.takeWhStatus);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
