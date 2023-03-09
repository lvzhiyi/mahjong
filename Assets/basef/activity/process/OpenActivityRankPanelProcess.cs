using cambrian.common;
using basef.rank;

namespace basef.activity
{
    /// <summary> 打开活动排名 </summary>
    public class OpenActivityRankPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            Activity activity = this.transform.parent.GetComponent<HuoDongView>().getActivity();
            CommandManager.addCommand(new ActivityRankListCommand(activity.sid),callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            RankList list = (RankList)obj;
            ActivityRankPanel panel = UnrealRoot.root.getDisplayObject<ActivityRankPanel>();

            panel.addRandList(list);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
