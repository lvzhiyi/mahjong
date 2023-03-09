using cambrian.game;

namespace platform.mahjong
{
    public class MJIMSwitchProcess : MouseClickProcess
    {
        public int type;
        public override void execute()
        {
            this.getRoot<MJQuickIMPanel>().switchTextOrIcon(type);
            SoundManager.playButton();
        }
    }
}
