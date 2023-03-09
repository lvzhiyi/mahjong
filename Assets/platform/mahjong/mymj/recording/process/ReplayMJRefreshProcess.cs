using UnityEngine;

namespace platform.mahjong
{
    public class ReplayMJRefreshProcess : Process
    {
        MJMatch scene;
        public void setScene(MJMatch scene)
        {
            this.scene = scene;
        }

        private ReplayMahjongRoomPanel panel;
        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();

            MJMatch.match = this.scene;


            panel.refreshHandCard(0,null,false,true);
            panel.refreshFixedCard(0,true);
            panel.refreshDisAbleView(0,true);
            panel.hideLastSendCard();
            panel.refreshCardNum();
            panel.hidesOperateView();
            panel.gameView.getPiaoStatusView().showpiao(MJMatch.match.getPlayerCardsStatus());
            panel.refreshTangImage(0, true);
            panel.refreshHuVew(0, false, true);
            panel.gameView.showDistypeCardView(scene.getPlayerDistypes());
            distype();
            hsz();
            initStage();
        }

        private void initStage()
        {
            if (scene.isstage(MJMatch.STAGE_INIT))
            {
                panel.gameView.getMjIndexCenterView().hide();
            }
        }


        private void distype()
        {
            if (scene.isstage(MJMatch.STAGE_DISTYPE))
            {
                panel.refreshDingQue(0, true); //先刷新所有人的定缺状态
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshHSZOrDQDerection(MJPlayerCards.STATUS_DISTYPE);
            }
        }

        private void hsz()
        {
            if (scene.isstage(MJMatch.STAGE_REPLACE))
            {
                panel.refreshDingQue(0, true);
                panel.refreshHuanSZ(0, true);
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshHSZOrDQDerection(MJPlayerCards.STATUS_REPLACE);
            }
        }
    }
}
