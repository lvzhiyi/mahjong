namespace platform.poker
{
    public class PDKAnYueMStartMatchRecording : Process
    {
        private PDKMStartMatchOperate operate;

        public PDKAnYueMStartMatchRecording(object operate)
        {
            this.operate = (PDKMStartMatchOperate)operate;
        }

        public override void execute()
        {
            var match = PDKAnYueMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();

            match.setBase(operate.operateData.type,
                      operate.operateData.stage,
                      operate.operateData.paidui,
                      operate.operateData.step,
                      operate.operateData.round,
                      operate.operateData.operates);

            match.firstCard = operate.firstCard;

            panel.gameView.stage.refresh();                                                 

            match.selectCard.clear();                                                   
            panel.gameView.hand.cancelAllSelect();                                        
            panel.gameView.clock.showClock(operate.operateData.round);                 
            panel.topView.setMuitplier(match.multiple.boomPoint);                        
            operate.playOver();
        }
    }
}
