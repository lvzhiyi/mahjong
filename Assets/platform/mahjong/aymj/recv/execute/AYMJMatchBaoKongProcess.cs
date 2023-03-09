namespace platform.mahjong
{
    public class AYMJMatchBaoKongProcess : Process
    {
        AYMJMatchBaoKongOperate operate;

        int selfOperate;

        public AYMJMatchBaoKongProcess(MJBaseOperate baseOperate)
        {
            operate = (AYMJMatchBaoKongOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.getGameView<AYMJGameView>().getBaoCardView().setSingleDistype(operate.index);
            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.setBaoKong(operate.cards);
            panel.refreshBaoKongCard(0, true);
            panel.refreshFixedCard(0, true); //先刷新固定牌(因为，在刷新手牌的时候，会去获取固定牌的宽高)
            panel.refreshHandCard(0, null, false, true);

            if (selfOperate > 0)
            {
                int card = match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }

            operate.playOver();
        }
    }
}
