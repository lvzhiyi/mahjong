namespace basef.arena
{
    /// <summary> 打开新增奖品面板 </summary>
    public class OpenArenaAddAwardPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAddAwardPanel panel = UnrealRoot.root.getDisplayObject<ArenaAddAwardPanel>();
            panel.setExchangeEntery(null);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
