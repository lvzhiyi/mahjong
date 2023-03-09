using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 搜索玩家id按钮
    /// </summary>
    public class ArenaSearchUserIdProcess : MouseClickProcess
    {
        ArenaRecordPanel panel;

        public override void execute()
        {
            panel= UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            if (panel.getUserId()==null||panel.getUserId()=="")
            {
                Alert.show("暂无数据");
                return;
            }
            long usersid = StringKit.parseLong(panel.getUserId());
            panel.userid = usersid;

            CommandManager.addCommand(new GetArenaRecordSelfDataListCommand(panel.arenaid,usersid, panel.selectTime, panel.getRuleType(), 0), back);
        }
        private void back(object o)
        {
            if (o == null) return;

            panel.setData((ArrayList<ArenaRecordContentData>)o);
            panel.refresh1();
        }
    }
}