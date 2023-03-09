namespace basef.arena
{
    /// <summary> 是否打烊 按钮 </summary>
    public class ArenaCreateArenaRankNoProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaCreateArenaPnael panel = UnrealRoot.root.getDisplayObject<ArenaCreateArenaPnael>();
            panel.setRank(false);
            panel.refreshSelect();
        }
    }
}