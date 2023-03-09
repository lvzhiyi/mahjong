using cambrian.game;

namespace basef.joinroom
{
    public class JoinRoomDelProcess:MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<SpotJoinRoomPanel>().del();
            SoundManager.playButton();
        }
    }
}
