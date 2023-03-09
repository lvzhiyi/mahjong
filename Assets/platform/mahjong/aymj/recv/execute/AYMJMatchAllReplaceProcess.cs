namespace platform.mahjong
{
    /// <summary>
    /// 所有人替换完后
    /// </summary>
    public class AYMJMatchAllReplaceProcess:Process
    {
        MJMatchAllReplaceOperate operate;

        int selfOperate;

        public AYMJMatchAllReplaceProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchAllReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {

            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.addHuanSZCards(operate.cards);//添加后，已经排好序

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);


            //这里要考虑换进来的牌有点动画效果
            panel.refreshHandCard(0, null, false, true);//刷新手牌

            panel.refreshHSZHandCard(match.mindex,match.getPlayerCardIndex<AYMJPlayerCards>(match.mindex).huansz, hszOver);

            panel.gameView.getHuanSZView().setVisible(false);
            panel.gameView.getHSZOverView().showMode(operate.replaceMode, null);
            //showOperate();
        }

        public void hszOver()
        {
            showOperate();
        }


        private void showOperate()
        {
            if (MJOperate.isDisType(selfOperate))
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshDingQue(0, true);
                panel.gameView.getDQView().showDingQue(AYMJMatch.match.getRecommendType());
            }
            else
            {
                if (selfOperate > 0)
                {
                    AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().handCardSort(true);
                    int card = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                    panel.gameView.getOperateView()
                        .showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex));
                    TingCardsInfo[] tinginfos = AYMJMatch.match.getSelfTingCards(AYMJMatch.match.mindex);
                    panel.refreshHandCard(AYMJMatch.match.mindex, tinginfos, false);
                    if (MJOperate.isDisCard(selfOperate))
                        panel.gameView.showDisCardRedmine(true);
                }
                else
                {
                    panel.refreshSingleHandCard(AYMJMatch.match.mindex);
                }
            }
            operate.playOver();
        }
    }
}
