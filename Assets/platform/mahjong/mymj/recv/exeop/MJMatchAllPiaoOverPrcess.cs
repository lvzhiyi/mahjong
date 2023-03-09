using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 执行飘结果
    /// </summary>
    public class MJMatchAllPiaoOverPrcess:Process
    {
        MJMatchAllPiaoOverOperate operate;

        int selfOperate;

        public MJMatchAllPiaoOverPrcess(MJBaseOperate baseoperate)
        {
            operate = (MJMatchAllPiaoOverOperate)baseoperate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

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

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            if (operate.isShuaiPiao())//是甩飘
            {
                panel.gameView.getPiaoStatusView().showPiaoAnimation(match.getPlayerCardsStatus(),operate.getFistDice(),operate.getSecondDice());
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
                panel.refreshHandCard(MJMatch.match.mindex, MJMatch.match.getSelfTingCards(MJMatch.match.mindex), false, false);
                int card = MJMatch.match.getSelfPlayerCards<MJPlayerCards>().mocard;
                panel.gameView.getOperateView().showOperate(MJMatch.match.getMJOperateInfos(selfOperate, card, false, MJMatch.match.mindex));
            }
        }
    }
}
