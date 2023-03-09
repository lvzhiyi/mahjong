using cambrian.game;

namespace basef.joinroom
{
    public class JoinRoomNumInputProcess : MouseClickProcess
    {
        public override void execute()
        {
            NumberKey key = this.GetComponent<NumberKey>();
            this.getRoot<SpotJoinRoomPanel>().input(key.key);
            SoundManager.playButton();
        }
    }
}
