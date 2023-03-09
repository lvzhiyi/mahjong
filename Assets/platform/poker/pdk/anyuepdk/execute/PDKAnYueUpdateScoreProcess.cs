using platform.bean;

namespace platform.poker
{
    public class PDKAnYueUpdateScoreProcess : Process
    {
        private PDKAnYueUpdateScoreOperate operate;

        private PDKAnYueRoomPanel panel;

        private PDKAnYueMatch match;

       

        public PDKAnYueUpdateScoreProcess(PKBaseOperate operateData)
        {
            operate = (PDKAnYueUpdateScoreOperate)operateData;
        }

        public override void execute()
        {
            if (operate.operateData.stage != PKSTAGE.MATCH) return;
            panel = (PDKAnYueRoomPanel)PKRoomPanel.Panel;
            match = PDKAnYueMatch.match;
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            panel.gameView.clock.showClock(operate.operateData.round);

            panel.gameView.operate.showOperateView(panel.operate);

            match.multiple.changeBoom(operate.index);
            panel.topView.setMuitplier(match.multiple.boomPoint);
            operate.playOver();
        }
    }
}
