using cambrian.common;
using cambrian.game;

namespace basef.rank
{
    public class SelectRankTypeProcess : MouseClickProcess
    {
        public override void execute()
        {
            RankTypeBar bar = this.GetComponent<RankTypeBar>();
            if (bar.isSelected) return;
            RankListPanel panel = UnrealRoot.root.getDisplayObject<RankListPanel>();
            panel.refreshSelected(bar.rankType);
            
            sendCommand(bar.rankType);
        }

        private void sendCommand(int index)
        {
            UserCommand command = null;

            switch (index)
            {
                case RankListPanel.WEALTH:
                    command = new GoldRankListCommand(index);
                    break;
                case RankListPanel.DAY:
                    command = new RankListTodayCommand();
                    break;
                case RankListPanel.LAST_DAY:
                    command = new RankListLastDayCommand();
                    break;
                case RankListPanel.WEEK:
                    command = new RankListNowCommand();
                    break;
                case RankListPanel.GOLD_WEEK:
                    command=new GoldRankListCommand(2);
                    break;
               
            }
            if (command != null) CommandManager.addCommand(command, onCommand);
        }

        public void onCommand(object obj)
        {
            if (obj == null) return;
            RankListPanel panel = UnrealRoot.root.getDisplayObject<RankListPanel>();
            panel.addRandList((RankList)obj);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
