using cambrian.game;

namespace basef.im
{
    public class IMSwitchIconProcess: MouseClickProcess
    {
        public override void execute()
        {
            this.getRoot<QuickIMPanel>().switchTextOrIcon(QuickIMPanel.ICON);
            SoundManager.playButton();
        }
    }
}
