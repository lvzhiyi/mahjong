using cambrian;
using cambrian.game;
using scene.game;

namespace basef.server
{
    /// <summary>
    /// 选择服务器
    /// </summary>
    public class SelectServerProcess : MouseClickProcess
    {
        public override void execute()
        {
            ServerBar bar = GetComponent<ServerBar>();
            ServerInfos.server = bar.server;
            ((SpotRedRoot) UnrealRoot.root).onSelectServerAddress();
            this.root.setVisible(false);
        }
    }
}
