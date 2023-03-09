using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 个人战绩 选择时间 </summary>
    public class ArenaRecordSelectSelfProcess : MouseClickProcess
    {
        private int selectType = 0;

        ArenaRecordPanel panel;
        Transform normal;
        Transform selected;

        Transform normal1;
        Transform selected1;
        public override void execute()
        {
            if (panel == null) panel = UnrealRoot.root.getDisplayObject<ArenaRecordPanel>();
            panel.setSelectType(selectType);
            CommandManager.addCommand(new GetArenaRecordSelfDataListCommand(panel.arenaid, panel.userid, panel.selectTime, panel.getRuleType(), 0), back);

        }

        private void back(object obj)
        {
            if (obj == null) return;
            panel.setSearchType(1);
            panel.setData((ArrayList<ArenaRecordContentData>)obj);
            panel.refresh();
        }
    }
}

