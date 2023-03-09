namespace platform.mahjong
{
    public class MJMatchDealCardsRecording : Process
    {
        MJMatchDealCardsOperate operate;

        int selfOperate;

        ReplayMahjongRoomPanel panel;

        public MJMatchDealCardsRecording(MJBaseOperate boperate)
        {
            operate = (MJMatchDealCardsOperate)boperate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            MJMatch match= MJMatch.match;
            match.DealCards(operate.cards);//发牌
            match.addBankerCard(operate.card);//给庄家增加一张牌
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);
            panel.refreshHandCard(0,null,false,true);
            showOperate();
        }

        private void showOperate()
        {
            if (MJOperate.isCanReplace(selfOperate))
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshHuanSZ(0, true);
                panel.refreshHuanSZUpCard();
            }
            else if (MJOperate.isDisType(selfOperate))
            {
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else
            {
                bool b = false;
                for (int i = 0; i < MJMatch.match.players.Length; i++)
                {
                    b = (i == MJMatch.match.banker);
                    MJMatch.match.getPlayerCardIndex<MJPlayerCards>(i).handCardSort(b);
                }

                panel.refreshHandCard(0, null, false, true);
                int card = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(MJMatch.match.banker).mocard;
                panel.showOperates(operate.operates, card);
            }

            operate.playOver();
        }
    }
}
