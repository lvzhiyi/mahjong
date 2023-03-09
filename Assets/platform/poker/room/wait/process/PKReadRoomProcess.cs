using cambrian.common;
using cambrian.game;

namespace platform.poker
{
    public class PKReadRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new PKPlayerReadyCommand(true, Room.room.getRoomIndex()));
            SoundManager.playButton();
        }
    }
}
