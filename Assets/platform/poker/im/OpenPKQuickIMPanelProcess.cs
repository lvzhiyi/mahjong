using basef.im;

namespace platform.poker
{
    public class OpenPKQuickIMPanelProcess : MouseClickSlideProcess
    {
        public override void mouseClick()
        {
            PKQuickIMPanel panel = UnrealRoot.root.getDisplayObject<PKQuickIMPanel>();
            panel.refresh();
            panel.switchTextOrIcon(PKQuickIMPanel.TEXT);
            panel.setVisible(true);
            IMQuickMsgManager.textinfo = PKQuickIMPanel.textInfo;
        }
    }
}