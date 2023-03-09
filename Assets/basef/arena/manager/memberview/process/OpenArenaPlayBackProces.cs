using cambrian.common;

namespace basef.arena
{
    public class OpenArenaPlayBackProces:MouseClickProcess
    {
        ArenaRecordPanel panel;

        private ArenaMemberDetailView memberview;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            panel.openType = ArenaRecordPanel.OPENTYPE_MEMBER;
            //panel.setData(null, Arena.arena.fkcSettings.getRules());
            long time = TimeKit.getTodayStart();
            panel.setTime(time);
            ArenaMemberDetailView memberview = transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            panel.userid = memberview.member.userid;
            panel.arenaid = Arena.arena.getId();
            panel.refresh();
            CommandManager.addCommand(new GetArenaRecordSelfDataListCommand(panel.arenaid,panel.userid, time,0, 0), back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaRecordContentData>)obj);
            panel.refresh();
            panel.setVisible(true);
            panel.setLastPanel(root);
            root.setVisible(false);
        }
    }
}
