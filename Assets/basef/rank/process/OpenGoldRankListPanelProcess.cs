using cambrian.common;

namespace basef.rank
{
    public class OpenGoldRankListPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GoldGroupRankListCommand(),callback);
        }

        public void callback(object obj)
        {
            ArrayList<GoldRankGroup> list = (ArrayList<GoldRankGroup>) obj;
            if (list.count == 0) return;
            GoldRankListPanel panel = UnrealRoot.root.getDisplayObject<GoldRankListPanel>();
            panel.setGroups(list);
            CommandManager.addCommand(new GoldRankListCommand(list.get(0).rankTypes[0]),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            RankList list = (RankList)obj;
            GoldRankListPanel panel = UnrealRoot.root.getDisplayObject<GoldRankListPanel>();
            panel.setRankList(list);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
