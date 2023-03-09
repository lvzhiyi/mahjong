using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 赛场战绩 选择时间 </summary>
    public class ArenaRecordSelectTimeTypeProcess : MouseClickProcess
    {
        private int selectType = 1;

        ArenaRecordPanel panel;

        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            panel.setSelectType(selectType);
            CommandManager.addCommand(new GetArenaRecordDataListCommand(panel.arenaid, panel.selectTime, panel.getRuleType(), 0),back);

        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setData((ArrayList<ArenaRecordContentData>)obj);
            panel.refresh();

        }
    }
}
