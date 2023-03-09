using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开合伙人操作视图
    /// </summary>
    public class OpenArenaAgentOperateViewProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaExtensionNextBar bar = transform.parent.GetComponent<ArenaExtensionNextBar>();
            getRoot<ArenaAgentPanel>().nextView.showOperateView(bar);
        }

    }
}
