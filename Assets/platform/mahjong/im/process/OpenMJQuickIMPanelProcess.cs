using basef.im;

namespace platform.mahjong
{
    public class OpenMJQuickIMPanelProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJQuickIMPanel panel = UnrealRoot.root.getDisplayObject<MJQuickIMPanel>();
            panel.refresh();
            panel.switchTextOrIcon(MJQuickIMPanel.TEXT);
            panel.setVisible(true);
            IMQuickMsgManager.textinfo = MJQuickIMPanel.textInfo;
        }
    }
}
