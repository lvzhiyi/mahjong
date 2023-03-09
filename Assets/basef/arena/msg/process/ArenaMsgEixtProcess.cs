using cambrian.game;

namespace basef.arena
{
    /// <summary> 返回竞赛场面板 </summary>
    public class ArenaMsgEixtProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
            SoundManager.playButton();
        }
    }
}
