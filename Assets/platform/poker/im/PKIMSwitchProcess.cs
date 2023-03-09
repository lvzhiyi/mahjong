using cambrian.game;

namespace platform.poker
{
    public class PKIMSwitchProcess : MouseClickProcess
    {
        public int type;

        public override void execute()
        {
            getRoot<PKQuickIMPanel>().switchTextOrIcon(type);
            SoundManager.playButton();
        }
    }
}
