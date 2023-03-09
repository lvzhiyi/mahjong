using platform.bean;

namespace platform.poker
{
    public class DDZMLandlordGrabRecording : Process
    {
        private DDZMLandlordGrabOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMLandlordGrabRecording(object operate)
        {
            this.operate = (DDZMLandlordGrabOperate)operate;
        }

        public DDZMLandlordGrabRecording(OperateData operateData, int index)
        {
            operate = new DDZMLandlordGrabOperate(null);
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

            special();

            endstage();

            operate.playOver();
        }
                                           
        private void special()
        {
            if (operate.operateData.type == DDZMatchMsg.CANCEL)
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_QIANG_DI_ZHU, true);
            }
            else
            {
                match.multipleBean.changeGrabLandlord();                       
                panel.topView.setMuitplier(match.grossMuitiplier);            
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.QIANG_DI_ZHU, true);
            }
        }
                                                    
        private void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.JIABEI)
            {
                panel.gameView.clock.showClock(Room.room.indexOf());            
            }
            if (operate.operateData.stage == DDZSTAGE.JIABEI ||                
                operate.operateData.stage == DDZSTAGE.MATCH ||                  
                operate.operateData.stage == DDZSTAGE.MINGPAI)                  
            {
                panel.gameView.stage.hidePStatus(true);                         
            }
        }
    }
}