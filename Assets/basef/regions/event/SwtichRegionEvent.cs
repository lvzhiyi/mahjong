using scene.game;
using scene.loading;

namespace basef.regions
{
    /// <summary>
    /// 切换地区
    /// </summary>
    public class SwtichRegionEvent : MouseClickProcess
    {
        public override void execute()
        {
            RegionsPanel panel= getRoot<RegionsPanel>();
            int id= panel.regionsView.select_id;
            SpotRedRoot.roots.regionModule.save(id);
            root.setVisible(false);
            SpotRedRoot.roots.closePanel();
            SpotRedRoot.roots.loading.refresh();
            SpotRedRoot.roots.loading.setVisible(true);
            if (SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Develop ||
                SpotRedRoot.roots.versionstype == SpotRedRoot.VersionsType.Beta)
            {
                SpotRedRoot.roots.takeWhStatus();
            }
            else
            {
                new SwtichSceneEvent(0).execute();
            }
            
        }
    }
}
