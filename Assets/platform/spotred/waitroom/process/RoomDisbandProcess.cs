using cambrian.common;
using UnityEngine;

namespace platform.spotred.waitroom
{
    public class RoomDisbandProcess : MouseClickProcess //这个在settingpanl
    {
        public override void execute()
        {
            Alert.confirmShow("您确认要退出游戏房间？", disband);
        }

        public void disband(Transform tran)
        {
            //后端现在是走的是退出房间的
            CommandManager.addCommand(new QuitRoomCommand(Room.room.getRoomIndex()));
        }
    }
}
