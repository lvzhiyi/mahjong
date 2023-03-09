namespace basef.arena
{
    /// <summary> 打开赛场消息面板 </summary>
    public class OpenArenaChangeUIPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaChangeUIPanel panel = UnrealRoot.root.getDisplayObject<ArenaChangeUIPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
