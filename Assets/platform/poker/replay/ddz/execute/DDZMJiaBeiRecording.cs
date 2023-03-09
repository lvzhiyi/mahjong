using platform.bean;

namespace platform.poker
{
    public class DDZMJiaBeiRecording : Process
    {
        private DDZMJiaBeiOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMJiaBeiRecording(object operate)
        {
            this.operate = (DDZMJiaBeiOperate)operate;
        }

        public DDZMJiaBeiRecording(OperateData operateData, int index)
        {
            operate = new DDZMJiaBeiOperate(null);
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

            special();

            endstage();

            operate.playOver();

            panel.gameView.clock.showClock(Room.room.indexOf());

            operate.playOver();
        }

        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_JIA_BEI);
            }
            else
            {
                match.multipleBean.setBoorPoint(operate.index, DDZMJiaBeiPoint.TWO);
                panel.topView.setMuitplier(DDZMatch.match.grossMuitiplier);
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.JIA_BEI, true);
            }
        }

        private void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.MATCH)
            {
                panel.gameView.clock.showClock(operate.operateData.round);                        
                panel.gameView.stage.hidePStatus(true);
            }
        }
    }
}