using cambrian.common;

namespace platform.spotred.waitroom
{
    public class QuitRoomProcess: MouseClickProcess
    {
        public override void execute()
        {
            Alert.show("您确认要退出游戏房间？", s => CommandManager.addCommand(new QuitRoomCommand(Room.room.getRoomIndex())));
        }
    }
}
