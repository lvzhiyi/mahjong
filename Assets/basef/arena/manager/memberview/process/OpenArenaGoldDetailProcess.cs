using cambrian.common;

namespace basef.arena
{
    public class OpenArenaGoldDetailProcess : MouseClickProcess
    {
        private ArenaAccountsGoldPanel panel;

        private ArenaMemberDetailView memberview;

        public override void execute()
        {
            memberview = transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaAccountsGoldPanel>();
            int type = panel.getType();
            bool checkGoldminus = panel.getCheckGoldminus();
            CommandManager.addCommand(
                new GetArenaAccountsGoldListCommand(memberview.member.userid,type,checkGoldminus,0),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;

            panel.setInitData((ArrayList<ArenaAccountsGoldContentData>)obj,memberview.member.userid,memberview.member.getArenaGold());
            panel.refresh();
            panel.setCVisible(true);
            memberview.setVisible(false);
        }
    }
}
