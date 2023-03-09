using platform.bean;
using UnityEngine;

namespace platform.poker
{
    /// <summary> 游戏叫分 </summary>
    public class DDZMCallScoreRecording : Process
    {
        private DDZMCallScoreOperate operate;
        private PDDZReplayRoomPanel panel;
        private DDZMatch match;

        public DDZMCallScoreRecording(object operate)
        {
            this.operate = (DDZMCallScoreOperate)operate;
        }

        public DDZMCallScoreRecording(PKBaseOperate bOperate)
        {
            operate = (DDZMCallScoreOperate)bOperate;
        }

        public DDZMCallScoreRecording(OperateData operateData, int index)
        {
            operate = new DDZMCallScoreOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            match.setBase(operate.operateData.type,        
                      operate.operateData.stage,         
                      operate.operateData.paidui,         
                      operate.operateData.step,           
                      operate.operateData.round,           
                      operate.operateData.operates);       

            panel.gameView.stage.hidePStatus(operate.operateData.round);                          
            panel.gameView.clock.showClock(operate.operateData.round);                        
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            panel.topView.refresh();

            special();

            endstage();

            operate.playOver();
        }

        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_JIAO_DIZHU, true);
            }
            else
            {
                switch (operate.score)
                {
                    case 1:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_ONE, true);
                        break;
                    case 2:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_TWO, true);
                        break;
                    case 3:
                        panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.CALLSCORE_THREE, true);
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
