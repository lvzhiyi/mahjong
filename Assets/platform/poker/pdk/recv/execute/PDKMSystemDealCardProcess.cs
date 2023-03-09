namespace platform.poker
{
    public class PDKMSystemDealCardProcess : Process
    {
        private PDKMSystemDealCardOperate operate;

        public PDKMSystemDealCardProcess(object operate)
        {
            this.operate = (PDKMSystemDealCardOperate)operate;
        }

        public override void execute()
        {
            var panel = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
            var match = PDKMatch.match;
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            match.DealCards(operate.cards);                                 
            match.recorder.incrCount(operate.cards[match.mindex]);             
            panel.operate = operate.operateData.operates[match.mindex];         
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            panel.gameView.recorder.setVisible(false);                          
            panel.gameView.status.hideBanker();                                
            panel.gameView.clock.setVisible(false);                             
            panel.gameView.clock.setIndex(operate.operateData.round);        
            panel.topView.setMuitplier(match.multiple.boomPoint);              
            panel.gameView.dealpoker.deal(operate.cards);                    
            operate.playOver();
        }
    }

}
