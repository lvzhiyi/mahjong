namespace platform.mahjong
{
    /// <summary>
    /// 麻将复位
    /// </summary>
    public class AYMJHandCardResetProcess:MouseClickProcess
    {
        public override void execute()
        {
            MahJongPanel panel = MahJongPanel.getPanel();
            if (panel.getWaitView().getVisible()) return;
            AYMJMatch match= AYMJMatch.match;
            if (match!=null&&match.isstage(AYMJMatch.STAGE_MATCH))
            {
                panel.gameView.getHandView().refreshSelfUpHandCard(-1);
                panel.gameView.setTingView(null);
                panel.refreshSelectCard(-1);
            }
        }
    }
}
