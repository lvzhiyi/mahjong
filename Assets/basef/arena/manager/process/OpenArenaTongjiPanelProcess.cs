using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开统计界面
    /// </summary>
    public class OpenArenaTongjiPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaTongjiPanel panel= UnrealRoot.root.getDisplayObject<ArenaTongjiPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
