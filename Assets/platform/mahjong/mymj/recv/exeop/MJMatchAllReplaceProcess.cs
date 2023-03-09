using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 所有人替换完后
    /// </summary>
    public class MJMatchAllReplaceProcess:Process
    {
        MJMatchAllReplaceOperate operate;

        int selfOperate;

        public MJMatchAllReplaceProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchAllReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.addHuanSZCards(operate.cards);//添加后，已经排好序

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);


            //这里要考虑换进来的牌有点动画效果
            panel.refreshHandCard(0, null, false, true);//刷新手牌

            panel.refreshHSZHandCard(match.mindex, match.getPlayerCardIndex<MJPlayerCards>(match.mindex).huansz, hszOver);

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
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else
            {
                if (selfOperate > 0)
                {
                    MJMatch.match.getSelfPlayerCards<MJPlayerCards>().handCardSort(MJOperate.hasCanDisCard(selfOperate));
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
            }
            operate.playOver();
        }
    }
}
