using cambrian.common;

namespace basef.rank
{
    public class OpenRankListPanelProcess:MouseClickProcess
    {
        public int[] rankTypes;

        public override void execute()
        {
            if(rankTypes[0]==RankListPanel.WEALTH)
            {
                CommandManager.addCommand(new GoldRankListCommand(rankTypes[0]),onCommand);
            }
        }

        public void onCommand(object obj)
        {
            if (obj == null) return;
            RankListPanel panel = UnrealRoot.root.getDisplayObject<RankListPanel>();
            panel.setTypes(rankTypes);
            panel.addRandList((RankList)obj);
            panel.refreshSelected(rankTypes[0]);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
