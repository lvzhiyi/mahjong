namespace platform.mahjong
{
    public class MJMatchTangRecording : Process
    {
        MJMatchTangOperate operate;

        int selfOperate;

        public MJMatchTangRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchTangOperate) operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            MJPlayerCards playerCards = match.getPlayerCardIndex<MJPlayerCards>(operate.index);

            playerCards.signTang(operate.cards);
            playerCards.handCardSort(false);
            playerCards.setStatus(MJPlayerCards.STATUS_TANG);

            panel.refreshTangImage(operate.index);
            panel.refreshHandCard(operate.index, null, false);

            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates, MJMatch.match.getLastPlayerCard());
            operate.playOver();
        }
    }
}
