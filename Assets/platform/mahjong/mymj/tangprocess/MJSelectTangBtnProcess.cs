namespace platform.mahjong
{
    /// <summary>
    /// 在操作中使用的躺
    /// </summary>
    public class MJSelectTangBtnProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match=MJMatch.match;
            MahjongRoomPanel panel= getRoot<MahjongRoomPanel>();
            panel.gameView.getHandView().refreshSelfUpHandCard(-1);
            panel.refreshSelectCard(-1);

            panel.tangOperate = MJTangStage.SELECT_PLAY_CARD;
            panel.gameView.showLieView(MJLieView.SELECT_PLAY_CARD);

            match.selfMoCard = match.getSelfPlayerCards<MJPlayerCards>().getMoCard();
            TingCardsInfo[] tinginfos = match.getSelfTingCards(match.mindex);
            panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);
            panel.gameView.setTingView(null);
        }
    }
}
