using platform.bean;

namespace platform.poker
{
    public class DDZMCallScoreProcess : Process
    {
        private DDZMCallScoreOperate operate;
        private PDDZRoomPanel panel;
        private DDZMatch match;

        public DDZMCallScoreProcess(object operate)
        {
            this.operate = (DDZMCallScoreOperate)operate;
        }

        public DDZMCallScoreProcess(OperateData operateData, int index)
        {
            operate = new DDZMCallScoreOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = PKRoomPanel.GetPanel<PDDZRoomPanel>();
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            panel.gameView.stage.hidePStatus(operate.operateData.round);
            panel.gameView.clock.showClock(operate.operateData.round);
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            special();
            endstage();
            panel.topView.refresh();
            panel.gameView.operate.showOperateView(panel.operate);
            operate.playOver();
        }

        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_JIAO_DIZHU);
            }
            else
            {
                switch (operate.score)
                {
                    case 1:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_ONE);
                        break;
                    case 2:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_TWO);
                        break;
                    case 3:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_THREE);
                        break;
                }
                match.setCallScore(operate.score);
            }
        }

        private void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.MATCH)
            {
                panel.gameView.stage.hidePStatus(true);
            }
            else if (operate.operateData.stage == DDZSTAGE.JIABEI)
            {
                panel.gameView.stage.hidePStatus(true);
                panel.gameView.clock.showClock(Room.room.indexOf());
            }
        }
    }
}
