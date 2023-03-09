namespace basef.arena
{
    /// <summary> 打开邀请界面 </summary>
    public class OpenArenaInvitationPnaelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaInvitationPanel panel = UnrealRoot.root.getDisplayObject<ArenaInvitationPanel>();
            panel.setData(null);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
