namespace basef.arena
{
    /// <summary> 打开兑换面板 </summary>
    public class OpenArenaExchangePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaExchangePanel panel = UnrealRoot.root.getDisplayObject<ArenaExchangePanel>();
            panel.showView(0);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
