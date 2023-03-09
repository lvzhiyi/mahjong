namespace basef.arena
{
    /// <summary>
    /// 更新
    /// </summary>
    public class OpenArenAddAwardPanelUpdateProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaAwardBar bar= transform.parent.GetComponent<ArenaAwardBar>();
            ArenaAddAwardPanel panel= UnrealRoot.root.getDisplayObject<ArenaAddAwardPanel>();
            panel.setExchangeEntery(bar.entery);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
