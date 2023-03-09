namespace platform.mahjong
{
    public class AYMJMatchBaoKongRecording : Process
    {
        AYMJMatchBaoKongOperate operate;

        int selfOperate;

        public AYMJMatchBaoKongRecording(MJBaseOperate baseOperate)
        {
            operate = (AYMJMatchBaoKongOperate)baseOperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.getGameView<AYMJGameView>().getBaoCardView().setSingleDistype(operate.index);
            AYMJPlayerCards playerCards = match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.setBaoKong(operate.cards);
            panel.refreshBaoKongCard(0, true);
            panel.refreshFixedCard(0, true); //先刷新固定牌(因为，在刷新手牌的时候，会去获取固定牌的宽高)
            panel.refreshHandCard(0, null, false, true);

            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);

            operate.playOver();
        }
    }
}
