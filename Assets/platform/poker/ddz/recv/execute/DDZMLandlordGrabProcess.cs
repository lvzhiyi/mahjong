using platform.bean;

namespace platform.poker
{
    public class DDZMLandlordGrabProcess : Process
    {
        private DDZMLandlordGrabOperate operate;
        private DDZMatch match;
        private PKRoomPanel panel;

        public DDZMLandlordGrabProcess(object operate)
        {
            this.operate = (DDZMLandlordGrabOperate)operate;
        }

        public DDZMLandlordGrabProcess(OperateData operateData, int index)
        {
            operate = new DDZMLandlordGrabOperate(null);
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

            panel.gameView.stage.hidePStatus(operate.operateData.round);
            panel.gameView.clock.showClock(operate.operateData.round);

            special();

            endstage();

            operate.playOver();

            panel.gameView.operate.showOperateView(panel.operate);
        }

        private void special()
        {
            if (operate.operateData.type == DDZMatchMsg.CANCEL)
            {
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.NO_QIANG_DI_ZHU);
            }
            else
            {
                match.multipleBean.changeGrabLandlord();
                panel.topView.setMuitplier(match.grossMuitiplier);
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.QIANG_DI_ZHU);
            }
        }

        /// <summary> 阶段结束时操作 </summary>
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
