using basef.bin;

namespace basef.lobby
{
    public class OpenBindAccountPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            BinAccountPanel panel = UnrealRoot.root.getDisplayObject<BinAccountPanel>();
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
