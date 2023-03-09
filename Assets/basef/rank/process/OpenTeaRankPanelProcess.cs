using cambrian.common;

namespace basef.rank
{
    public class OpenTeaRankPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new TeaRankListPanelCommand(),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            TeaRankListPanel panel = UnrealRoot.root.getDisplayObject<TeaRankListPanel>();
            panel.setData(((TeaRankList)obj).ranks);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
