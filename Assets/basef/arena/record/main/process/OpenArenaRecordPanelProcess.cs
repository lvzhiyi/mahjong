using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开战绩面板 </summary>
    public class OpenArenaRecordPanelProcess : MouseClickProcess
    {
        ArenaRecordPanel panel;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            panel.openType = ArenaRecordPanel.OPENTYPE_LOBBY;
            long time = TimeKit.getTodayStart();
            panel.setTime(time);
            panel.setData(null);
            panel.userid = Player.player.userid;
            panel.arenaid = Arena.arena.getId();
            panel.setSelectType(ArenaRecordPanel.TYPE_SELF);
            //判断擂主/推广员   普通成员
            CommandManager.addCommand(new GetArenaRecordSelfDataListCommand(panel.arenaid,panel.userid, time, 0,  0),back);

        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaRecordContentData>)obj);
            panel.refresh();
            panel.setVisible(true);
        }
    }
}
