namespace platform.mahjong
{
    /// <summary>
    /// 麻将复位
    /// </summary>
    public class MJHandCardResetProcess:MouseClickProcess
    {
        public override void execute()
        {
            MahJongPanel panel = MahJongPanel.getPanel();
            if (panel.getWaitView().getVisible()) return;
            MJMatch match= MJMatch.match;
            if (match!=null&&match.isstage(MJMatch.STAGE_MATCH))
            {
                panel.gameView.getHandView().refreshSelfUpHandCard(-1);
                panel.gameView.setTingView(null);
                panel.refreshSelectCard(-1);
            }
        }
    }
}
