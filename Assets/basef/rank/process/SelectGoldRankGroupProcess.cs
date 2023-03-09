using cambrian.common;

namespace basef.rank
{
    public class SelectGoldRankGroupProcess:MouseClickProcess
    {
        public override void execute()
        {
            GoldRankTypeBar bar= this.transform.parent.GetComponent<GoldRankTypeBar>();
            CommandManager.addCommand(new GoldRankListCommand(bar.@group.rankTypes[0]),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            GoldRankTypeBar bar = this.transform.parent.GetComponent<GoldRankTypeBar>();
            RankList list = (RankList)obj;
            if (bar.group.rankTypes[0] == GoldRankGroup.RANK_GROUP_WIN && list.getJueShu() < 0)
            {
                list.setJueShu(0);
            }
            else if (bar.group.rankTypes[0] == GoldRankGroup.RANK_GROUP_LOSE && list.getJueShu() > 0)
            {
                list.setJueShu(0);
            }

            GoldRankListPanel panel= this.getRoot<GoldRankListPanel>();
            panel.refreshLeftView(bar.index_space);
            panel.setRankList(list);
            panel.refreshRightView(bar.index_space);
        }
    }
}
