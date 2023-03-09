namespace platform.poker
{
    public class PDKAnYueMStartMatchProcess : Process
    {
        private PDKMStartMatchOperate operate;

        public PDKAnYueMStartMatchProcess(object operate)
        {
            this.operate = (PDKMStartMatchOperate)operate;
        }

        public override void execute()
        {
            var panel = (PDKAnYueRoomPanel)PKRoomPanel.Panel;
            var match = PDKAnYueMatch.match;
            match.firstCard=operate.firstCard;
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;                                   
            match.step = operate.operateData.step;
            panel.gameView.stage.refresh();                                               
            match.selectCard.clear();                                                   
            panel.gameView.hand.cancelAllSelect();                                      
            panel.gameView.clock.showClock(operate.operateData.round);                  
            panel.topView.setMuitplier(match.multiple.boomPoint);                         
            panel.gameView.operate.showOperateView(panel.operate, true);                 
            operate.playOver();
        }
    }
}
