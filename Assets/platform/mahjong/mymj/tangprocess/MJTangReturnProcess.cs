namespace platform.mahjong
{
    /// <summary>
    /// 在选择躺牌的视图中，选择返回
    /// </summary>
    public class MJTangReturnProcess:MouseClickProcess
    {
        public override void execute()
        {
            MJMatch match = MJMatch.match;
            MahjongRoomPanel panel = getRoot<MahjongRoomPanel>();


            match.getSelfPlayerCards<MJPlayerCards>().moveCardToMoCard(match.selfMoCard);
            TingCardsInfo[] tinginfos = match.getSelfTingCards(match.mindex);
            panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);


            panel.gameView.getOperateView()
                .showOperate(match.getMJOperateInfos(panel.getOperate(), MJMatch.match.selfMoCard, false, MJMatch.match.mindex));
            panel.tangOperate = -1;

           
        }
    }
}
