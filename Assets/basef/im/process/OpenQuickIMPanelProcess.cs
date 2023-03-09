namespace basef.im
{
    public class OpenQuickIMPanelProcess: MouseClickProcess
    {
        public override void execute()
        {
            QuickIMPanel panel = UnrealRoot.root.getDisplayObject<QuickIMPanel>();
            panel.refresh();
            panel.switchTextOrIcon(QuickIMPanel.TEXT);
            panel.setVisible(true);
            IMQuickMsgManager.textinfo = QuickIMPanel.textInfo;
        }
    }
}
