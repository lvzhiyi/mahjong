using cambrian.common;
using platform.spotred;

namespace platform.spotred.room
{
    public class SelectKongCardProcess : MouseClickProcess
    {
        public override void execute()
        {
            SpotRoomPanel panel=UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.operateView.hideAllBtn();
            panel.showTextinfo(true);
            KongCardBar bar = GetComponent<KongCardBar>();
            CommandManager.addCommand(new SendMatchCommand(SendMatchCommand.KONG, CPMatch.match.step, bar.card, 0, null));
            this.root.setVisible(false);
        }
    }
}
