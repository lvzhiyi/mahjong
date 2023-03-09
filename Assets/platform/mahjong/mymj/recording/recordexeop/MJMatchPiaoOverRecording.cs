namespace platform.mahjong
{
    public class MJMatchPiaoOverRecording:Process
    {
        MJMatchAllPiaoOverOperate operate;

        int selfOperate;

        public MJMatchPiaoOverRecording(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllPiaoOverOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            MJPlayerCards[] playerCards = match.getPlayerCardss<MJPlayerCards>();

            for (int i = 0; i < playerCards.Length; i++)
            {
                if (operate.hadPiao(i))
                {
                    playerCards[i].setPiao();
                }
            }

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            if (operate.isShuaiPiao())//是甩飘
            {
                panel.gameView.getPiaoStatusView().showPiaoAnimation(match.getPlayerCardsStatus(), operate.getFistDice(), operate.getSecondDice());
            }
            else//选飘
            {
                panel.gameView.getPiaoStatusView().showpiao(match.getPlayerCardsStatus());
            }

            panel.gameView.getPiaoView().setVisible(false);

            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }

    }
}
