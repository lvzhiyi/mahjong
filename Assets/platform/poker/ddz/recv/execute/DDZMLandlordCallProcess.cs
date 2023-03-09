using platform.bean;

namespace platform.poker
{
    public class DDZMLandlordCallProcess : Process
    {
        private DDZMLandlordCallOperate operate;
        private DDZMatch match;
        private PDDZRoomPanel panel;

        public DDZMLandlordCallProcess(object operate)
        {
            this.operate = (DDZMLandlordCallOperate)operate;
        }

        public DDZMLandlordCallProcess(OperateData operateData, int index)
        {
            operate = new DDZMLandlordCallOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();

            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;

            panel.gameView.clock.showClock(operate.operateData.round);                          
            panel.gameView.stage.hidePStatus(operate.operateData.round);                        
            panel.gameView.recorder.setData(match.recorder.getRemainCount());                     

            special();

            endstage();

            operate.playOver();

            panel.gameView.operate.showOperateView(panel.operate);                             
        }

        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_JIAO_DIZHU);
            }
            else panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.JIAO_DI_ZHU);
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
