using scene.game;

namespace basef.regions
{
    public class OpenRegionPanelProcess:Process
    {
        public override void execute()
        {
            RegionsPanel panel = UnrealRoot.root.getDisplayObject<RegionsPanel>();
            panel.setAction(SpotRedRoot.roots.takeWhStatus);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
