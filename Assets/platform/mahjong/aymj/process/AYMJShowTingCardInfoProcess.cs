namespace platform.mahjong
{
    /// <summary>
    /// 显示听牌内容
    /// </summary>
    public class AYMJShowTingCardInfoProcess:MouseClickProcess
    {
        private AYMJMatch match;

        private AYMJPlayerCards playerCards;
        public override void execute()
        {
            AYMJRoomPanel panel= getRoot<AYMJRoomPanel>();

            match=AYMJMatch.match;

            if (panel.gameView.tingCardView.getVisible())
            {
                if (match.isRound(match.mindex)&& panel.getOperate() != 0)
                    return;
                else
                {
                    panel.gameView.tingCardView.setVisible(false);
                    return;
                }
            }
            else
            {
                if (match.isRound(match.mindex)&& panel.getOperate()!=0)
                {
                    panel.gameView.tingCardView.setVisible(false);
                    return;
                }
            }

            playerCards= match.getSelfPlayerCards<AYMJPlayerCards>();
          
            TingCardsInfo infos = AYMJMatch.match.getDengTingCards(match.mindex);
           
            if (infos == null)
            {
                transform.gameObject.SetActive(false);
                return;
            }
            panel.gameView.setTingView(infos);
        }
    }

   
}
