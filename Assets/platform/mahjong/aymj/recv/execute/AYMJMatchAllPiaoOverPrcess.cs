using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 执行飘结果
    /// </summary>
    public class AYMJMatchAllPiaoOverPrcess:Process
    {
        MJMatchAllPiaoOverOperate operate;

        int selfOperate;

        public AYMJMatchAllPiaoOverPrcess(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllPiaoOverOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            AYMJPlayerCards[] playerCards = match.getPlayerCardss<AYMJPlayerCards>();
           
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (operate.hadPiao(i))
                {
                    playerCards[i].setPiao();
                }
            }

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

           
            panel.gameView.getPiaoStatusView().showpiao(match.getPlayerCardsStatus());
            

            panel.gameView.getPiaoView().setVisible(false);

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
                otherOperate();
            }

            operate.playOver();
        }

        public void otherOperate()
        {
            if (selfOperate > 0)
            {
                panel.refreshHandCard(AYMJMatch.match.mindex, AYMJMatch.match.getSelfTingCards(AYMJMatch.match.mindex), false, false);
                int card = AYMJMatch.match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex));
            }
        }
    }
}
