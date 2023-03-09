namespace basef.arena
{
    /// <summary> 打开报名面板 </summary>
    public class OpenArenaSignUpPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSignUpPanel panel = UnrealRoot.root.getDisplayObject<ArenaSignUpPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
