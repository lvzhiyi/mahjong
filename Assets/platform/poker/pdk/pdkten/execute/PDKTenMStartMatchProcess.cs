namespace platform.poker
{
    public class PDKTenMStartMatchProcess : Process
    {
        private PDKMStartMatchOperate operate;

        public PDKTenMStartMatchProcess(object operate)
        {
            this.operate = (PDKMStartMatchOperate)operate;
        }

        public override void execute()
        {
            var panel = (PDKTenRoomPanel)PKRoomPanel.Panel;
            var match = PDKTenMatch.match;
            match.firstCard=operate.firstCard;
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;                                   
            match.step = operate.operateData.step;
            panel.gameView.stage.refresh();                                               
            match.selectCard.clear();                                                   
            panel.gameView.hand.cancelAllSelect();                                      
            panel.gameView.clock.showClock(operate.operateData.round);                  
            panel.topView.setMuitplier(match.multiple.boomPoint);
            panel.gameView.operate.showOperateView(panel.operate);
            operate.playOver();
        }
    }
}
