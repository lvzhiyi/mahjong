namespace basef.arena
{
    /// <summary>
    /// 打开合伙人界面
    /// </summary>
    public class OpenArenaAgentPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAgentPanel panel= UnrealRoot.root.getDisplayObject<ArenaAgentPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
