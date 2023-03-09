using cambrian.common;
using cambrian.game;

namespace platform.spotred.waitroom
{
    public class ReadRoomProcess: MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new PlayerReadyCommand(true, Room.room.getRoomIndex()));
            SoundManager.playButton();
        }
    }
}
