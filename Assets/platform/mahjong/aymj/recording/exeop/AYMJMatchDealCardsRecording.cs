namespace platform.mahjong
{
    public class AYMJMatchDealCardsRecording : Process
    {
        MJMatchDealCardsOperate operate;

        int selfOperate;

        ReplayAYMJRoomPanel panel;

        public AYMJMatchDealCardsRecording(MJBaseOperate boperate)
        {
            operate = (MJMatchDealCardsOperate)boperate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.DealCards(operate.cards);//发牌
            match.addBankerCard(operate.card);//给庄家增加一张牌
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.refreshHandCard(0, null, false, true);
            showOperate();
        }

        private void showOperate()
        {
            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }
    }
}
