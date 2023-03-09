using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 打开商品兑换记录面板 </summary>
    public class OpenArenaExchangePhysicalRecordProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaExchangePhysicalRecordCommand(0),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaExchangePhysicalRecordPanel panel = UnrealRoot.root.getDisplayObject<ArenaExchangePhysicalRecordPanel>();
            panel.setData(obj);
            panel.refresh();
            panel.setCVisible(true);
            panel = null;
        }
    }
}
