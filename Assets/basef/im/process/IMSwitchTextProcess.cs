using cambrian.game;

namespace basef.im
{
    public class IMSwitchTextProcess: MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<QuickIMPanel>().switchTextOrIcon(QuickIMPanel.TEXT);
            SoundManager.playButton();
        }
    }
}
