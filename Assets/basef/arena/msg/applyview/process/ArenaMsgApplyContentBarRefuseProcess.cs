using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 赛场审核 拒绝 </summary>
    public class ArenaMsgApplyContentBarRefuseProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaMsgApplyContentBar bar = transform.parent.GetComponent<ArenaMsgApplyContentBar>();

            CommandManager.addCommand(new ArenaMsgApplyContentEventCommand(false,bar.getData().uuid,ArenaEvent.EVENT_TYPE_MEMBER),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                Alert.show("已拒绝");
                msg_back();
            }
            else
            {
                Alert.show("拒绝失败");
            }
        }

        public void msg_back()
        {
            ArenaEvent data = transform.parent.GetComponent<ArenaMsgApplyContentBar>().getData();
            ArenaMsgPanel panel = UnrealRoot.root.getDisplayObject<ArenaMsgPanel>();
            panel.managerView.arenaApplyView.remove(data);
            panel.managerView.arenaApplyView.refresh();
        }
    }
}