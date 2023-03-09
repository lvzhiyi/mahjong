using cambrian.common;
using cambrian.game;
using UnityEngine;

namespace platform.poker
{
    public class PKRoomDisbandProcess : MouseClickProcess //这个在settingpanl 和 DisbandPanel
    {
        public bool isagree;
        public override void execute()
        {
            if (isagree)
                Alert.confirmShow("您确认要退出游戏房间？",this.disband);
            else this.disband(this.transform);

            SoundManager.playButton();
        }
        void disband(Transform tran)
        {
            CommandManager.addCommand(new PKRoomDisbandCommand(this.isagree));
        }
    }
}
