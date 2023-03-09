namespace platform.mahjong
{
    /// <summary>
    /// 所有玩家定缺完成
    /// </summary>
    public class MJMatchAllDisTypeRecording : Process
    {
        MJMatchAllDisTypeOperate operate;

        public MJMatchAllDisTypeRecording(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllDisTypeOperate)baseoperate;
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui,operate.round);
            MJPlayerCards[] playerCards = match.getPlayerCardss<MJPlayerCards>();
            for (int i = 0; i < playerCards.Length; i++)
                playerCards[i].setDistype(operate.distypes[i]);
         
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            panel.gameView.getDQView().setVisible(false);
            panel.gameView.showDistypeCardView(operate.distypes);
            showOperate();
        }

        private void showOperate()
        {
            //定缺后，需要排序
            bool b = false;
            for (int i = 0; i < MJMatch.match.players.Length; i++)
            {
                b = (i == MJMatch.match.banker);
                MJMatch.match.getPlayerCardIndex<MJPlayerCards>(i).handCardSort(b);
            }

            panel.refreshHandCard(0, null, false, true);
            int card = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(MJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }
    }
}
