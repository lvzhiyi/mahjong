using cambrian.game;

namespace basef.joinroom
{
    public class JoinRoomClearProcess:MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<SpotJoinRoomPanel>().clear();
            SoundManager.playButton();
        }
    }
}
