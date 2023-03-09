using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开奖章明细
    /// </summary>
    public class OpenArenaMemeberMedalProcess : MouseClickProcess
    {
        private ArenaAccountsMedalPanel panel;

        private ArenaMemberDetailView memberview;

        private int medal;

        public override void execute()
        {
            memberview = transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaAccountsMedalPanel>();
            panel.userid = memberview.member.userid;
            CommandManager.addCommand(new GetArenaAccountsAssignMedalListCommand(panel.userid),getMedalBack);
        }

        private void getMedalBack(object obj)
        {
            if (obj == null) return;
            medal = (int)obj;
            CommandManager.addCommand(new GetArenaAccountsMedalListCommand(panel.userid,panel.getType(),0),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaMedalBill>)obj,medal);
            panel.refresh();
            panel.setCVisible(true);
            memberview.setVisible(false);
        }
    }
}
