namespace platform.poker
{
    public class DDZOpenMultiplePanelProcess : MouseClickSlideProcess
    {
        private DDZMultiplePanel panel;

        private bool isEnter = false;

        public override void mouseClick()
        {
            if (!panel) panel = UnrealRoot.root.getDisplayObject<DDZMultiplePanel>();
            panel.setData(DDZMatch.match != null ? DDZMatch.match.multipleBean : null);
            panel.refresh();
            isEnter = !isEnter;
            panel.setVisible(isEnter);
        }
    }
}
