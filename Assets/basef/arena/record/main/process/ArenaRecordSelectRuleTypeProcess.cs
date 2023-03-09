using cambrian.common;
using cambrian.game;

namespace basef.arena
{
    /// <summary> 玩家选择游戏规则 打开相应的战绩面板 </summary>
    public class ArenaRecordSelectRuleTypeProcess : MouseClickProcess
    {
        ArenaRecordPanel panel;

        ArenaRecordRuleTypeBar bar;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            bar = transform.parent.GetComponent<ArenaRecordRuleTypeBar>();
            long time = bar.time;
            panel.updateTime(time);
            UserCommand command;
            if (panel.selectType == 0)
                command = new GetArenaRecordSelfDataListCommand(panel.arenaid, panel.userid, panel.selectTime, panel.getRuleType(), 0);
            else
                command = new GetArenaRecordDataListCommand(panel.arenaid, panel.selectTime, panel.getRuleType(), 0);

            CommandManager.addCommand(command, back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaRecordContentData>)obj);
            panel.refresh1();
        }
    }
}
