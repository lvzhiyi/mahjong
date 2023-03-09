using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace platform
{
    /// <summary>
    /// 10分钟不再接收邀请
    /// </summary>
    public class NOAcceptInviteTimeProcess:MouseClickProcess
    {
        Image gou;
        public override void execute()
        {
            RoomPlayerInvitePanel panel= getRoot<RoomPlayerInvitePanel>();

            gou = panel.normal;

           
            if (!gou.IsActive())
            {
                
                 if (panel.type == 3)
                {
                    Alert.confirmShow("勾选后在10分钟内接受不到所有玩家邀请。", arenaok);
                }
                else
                {
                Alert.confirmShow("勾选后在10分钟内接受不到所有玩家邀请。", ok);
                }
            }
            else
            {
                gou.gameObject.SetActive(false);
            }
        }
        public void arenaok(Transform tr)
        {
            CommandManager.addCommand(new ArenaNoAcceptInviteCommand(), back);
        }

        public void ok(Transform tr)
        {
            CommandManager.addCommand(new NOAcceptInviteTimeCommand(),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            bool b = (bool)obj;
            gou.gameObject.SetActive(b);
        }
    }
}
