using platform.bean;

namespace platform.poker
{
    public class DDZMJiaBeiPoint
    {
        public const int FOUR = 4;
        public const int TWO = 2;
    }

    public class DDZMJiaBeiProcess : Process
    {
        private DDZMJiaBeiOperate operate;
        private DDZMatch match;
        private PKRoomPanel panel;

        public DDZMJiaBeiProcess(object operate)
        {
            this.operate = (DDZMJiaBeiOperate)operate;
        }

        public DDZMJiaBeiProcess(OperateData operateData, int index)
        {
            operate = new DDZMJiaBeiOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            match = DDZMatch.match;

            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;

            special();

            endstage();

            operate.playOver();

            panel.gameView.clock.showClock(Room.room.indexOf());

            panel.gameView.operate.showOperateView(panel.operate, true);
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
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.JIA_BEI);      
            }
        }

        public void endstage()
        {
            if (operate.operateData.stage == DDZSTAGE.MATCH)
            {
                panel.gameView.clock.showClock(operate.operateData.round);                    
                panel.gameView.stage.hidePStatus(true);                                      
            }
        }
    }
}
