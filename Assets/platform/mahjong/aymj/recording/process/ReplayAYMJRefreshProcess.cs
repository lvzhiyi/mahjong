using UnityEngine;

namespace platform.mahjong
{
    public class ReplayAYMJRefreshProcess : Process
    {
        AYMJMatch scene;
        public void setScene(AYMJMatch scene)
        {
            this.scene = scene;
        }

        private ReplayAYMJRoomPanel panel;
        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();

            AYMJMatch.match = this.scene;

            panel.refreshBaoKongCard(0, true);
            panel.refreshHandCard(0,null,false,true);
            panel.refreshFixedCard(0,true);
            panel.refreshDisAbleView(0,true);
            panel.hideLastSendCard();
            panel.refreshCardNum();
            panel.hidesOperateView();
            panel.gameView.getPiaoStatusView().showpiao(AYMJMatch.match.getPlayerCardsStatus());
            panel.refreshHuVew(0, false, true);
            panel.gameView.showDistypeCardView(scene.getPlayerDistypes());
            panel.getGameView<AYMJGameView>().getBaoCardView().setBaoPai(AYMJMatch.match.getPlayerCardsStatus());
            distype();
            hsz();
            initStage();
        }

        private void initStage()
        {
            if (scene.isstage(AYMJMatch.STAGE_INIT))
            {
                panel.gameView.getMjIndexCenterView().hide();
            }
        }


        private void distype()
        {
            if (scene.isstage(AYMJMatch.STAGE_DISTYPE))
            {
                panel.refreshDingQue(0, true); //先刷新所有人的定缺状态
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshHSZOrDQDerection(AYMJPlayerCards.STATUS_DISTYPE);
            }
        }

        private void hsz()
        {
            if (scene.isstage(AYMJMatch.STAGE_REPLACE))
            {
                panel.refreshDingQue(0, true);
                panel.refreshHuanSZ(0, true);
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshHSZOrDQDerection(AYMJPlayerCards.STATUS_REPLACE);
            }
        }
    }
}
