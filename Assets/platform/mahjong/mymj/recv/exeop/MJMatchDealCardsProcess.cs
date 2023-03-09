namespace platform.mahjong
{
    public class MJMatchDealCardsProcess:Process
    {
        MJMatchDealCardsOperate operate;

        int selfOperate;

        MahjongRoomPanel panel;

        public MJMatchDealCardsProcess(MJBaseOperate boperate)
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

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
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
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshDingQue(0, true);
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else if (MJOperate.isCanPiao(selfOperate))//选择飘
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshSelectPiao(MJMatch.match.mindex);
            }
            else
            {
                panel.refreshHandCard(0, MJMatch.match.getSelfTingCards(MJMatch.match.mindex),false, true);
                if (MJOperate.isDisCard(selfOperate))
                {
                    panel.gameView.showDisCardRedmine(true);
                }
                else if(selfOperate>0)
                {
                    int card = 0;
                    if (MJOperate.isCanHu(selfOperate))
                        card = MJMatch.match.getSelfPlayerCards<MJPlayerCards>().mocard;
                   
                    panel.gameView.getOperateView()
                        .showOperate(MJMatch.match.getMJOperateInfos(selfOperate, card, false, MJMatch.match.mindex));
                }

                if (selfOperate == 0)
                {
                    panel.refreshSingleHandCard(MJMatch.match.mindex);
                }
            }
            operate.playOver();
        }
    }
}
