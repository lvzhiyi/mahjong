using scene.game;
using scene.loading;

namespace basef.server
{
    /// <summary>
    /// 删除资源
    /// </summary>
    public class DelResoureProcess:MouseClickProcess
    {
        public override void execute()
        {
            LoadingRoot.isDelete = true;
            VersionsCfg.cfg = null;
            new SwtichSceneEvent(0).execute();
        }
    }
}
