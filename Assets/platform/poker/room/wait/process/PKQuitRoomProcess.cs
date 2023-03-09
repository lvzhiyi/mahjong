using cambrian.common;
using platform.spotred.waitroom;

namespace platform.poker
{
    public class PKQuitRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            Alert.show("您确认要退出游戏房间？", s => 
            CommandManager.addCommand(new QuitRoomCommand(Room.room.getRoomIndex())));
        }
    }
}
