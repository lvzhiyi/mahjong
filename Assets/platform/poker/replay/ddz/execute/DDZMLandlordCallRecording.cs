using platform.bean;

namespace platform.poker
{
    public class DDZMLandlordCallRecording : Process
    {
        private DDZMLandlordCallOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMLandlordCallRecording(object operate)
        {
            this.operate = (DDZMLandlordCallOperate)operate;
        }

        public DDZMLandlordCallRecording(OperateData operateData, int index)
        {
            operate = new DDZMLandlordCallOperate(null);
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

            panel.gameView.clock.showClock(operate.operateData.round);                          
            panel.gameView.stage.hidePStatus(operate.operateData.round);                       
            panel.gameView.recorder.setData(match.recorder.getRemainCount());                    

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
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.JIAO_DI_ZHU, true);
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