using basef.rank;
using cambrian.common;

namespace basef.arena
{
    public class ArenaSelectRankTypeProcess:MouseClickProcess
    {
        public override void execute()
        {
            ArenaRankLeftBar bar = transform.parent.GetComponent<ArenaRankLeftBar>();
            ArenaRankPanel panel= getRoot<ArenaRankPanel>();
            panel.setSelectType(bar.index_space,bar.type);
            CommandManager.addCommand(new GetArenaRankListCommand(Arena.arena.getId(), bar.type), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaRankPanel panel = getRoot<ArenaRankPanel>();
            panel.setData((RankList)obj);
            panel.refreshRight();
        }
    }
}
