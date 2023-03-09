namespace platform.mahjong
{
    /// <summary>
    /// 所有玩家定缺完成
    /// </summary>
    public class MJMatchAllDisTypeProcess : Process
    {
        MJMatchAllDisTypeOperate operate;

        int selfOperate;

        public MJMatchAllDisTypeProcess(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllDisTypeOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui,operate.round);
            MJPlayerCards[] playerCards = match.getPlayerCardss<MJPlayerCards>();
            for (int i = 0; i < playerCards.Length; i++)
                playerCards[i].setDistype(operate.distypes[i]);
            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            panel.gameView.getDQView().setVisible(false);
            panel.gameView.showDistypeCardView(operate.distypes);
            showOperate();
        }

        private void showOperate()
        {
            //定缺后，需要排序
            MJMatch.match.getSelfPlayerCards<MJPlayerCards>().handCardSort(MJOperate.hasCanDisCard(selfOperate));
            panel.refreshHandCard(0, null, false, true);
            if (selfOperate > 0)
            {
                int card = MJMatch.match.getSelfPlayerCards<MJPlayerCards>().mocard;
                panel.gameView.getOperateView()
                    .showOperate(MJMatch.match.getMJOperateInfos(selfOperate, card, false,MJMatch.match.mindex));
                TingCardsInfo[] tinginfos = MJMatch.match.getSelfTingCards(MJMatch.match.mindex);
                panel.refreshHandCard(MJMatch.match.mindex, tinginfos, false);
                if (MJOperate.isDisCard(selfOperate))
                    panel.gameView.showDisCardRedmine(true);
            }
            else
            {
                panel.refreshSingleHandCard(MJMatch.match.mindex);
            }
            operate.playOver();
        }
    }
}
