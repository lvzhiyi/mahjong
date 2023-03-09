using cambrian.common;

namespace basef.rank
{
    /// <summary>
    /// 金币场选择日期
    /// </summary>
    public class SelectGoldDayRankProcess:MouseClickProcess
    {
        public override void execute()
        {
            GoldRankDayBar bar= this.transform.GetComponent<GoldRankDayBar>();
            if (bar.getState()) return;
            CommandManager.addCommand(new GoldRankListCommand(bar.type),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            RankList list = (RankList) obj;
            GoldRankListPanel panel= this.getRoot<GoldRankListPanel>();
            panel.setRankList(list);
            GoldRankDayBar bar = this.transform.GetComponent<GoldRankDayBar>();
            panel.refreshRightView(panel.selectedGroupIndex);
            panel.refreshRightViewDayIndex(bar.getId());
        }
    }
}
