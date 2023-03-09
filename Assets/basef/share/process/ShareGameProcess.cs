using cambrian.game;

namespace basef.share
{
    public class ShareGameProcess:MouseClickProcess
    {
        public override void execute()
        {
            SharePanel panel=UnrealRoot.root.getDisplayObject<SharePanel>();
            panel.setCVisible(true);
        }
    }
}
