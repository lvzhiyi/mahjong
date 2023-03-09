using basef.rank;
using cambrian.common;

namespace basef.arena
{
    public class OpenArenaRankListProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaRankListCommand(Arena.arena.getId(),0),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaRankPanel panel = UnrealRoot.root.getDisplayObject<ArenaRankPanel>();
            panel.setData((RankList)obj);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
