namespace basef.arena
{
    /// <summary> 排行榜显示按钮 按钮 </summary>
    public class ArenaCreateArenaRankYesProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaCreateArenaPnael panel = UnrealRoot.root.getDisplayObject<ArenaCreateArenaPnael>();
            panel.setRank(true);
            panel.refreshSelect();
        }
    }
}
