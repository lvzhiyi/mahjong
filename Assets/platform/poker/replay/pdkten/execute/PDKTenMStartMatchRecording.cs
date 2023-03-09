namespace platform.poker
{
    public class PDKTenMStartMatchRecording : Process
    {
        private PDKMStartMatchOperate operate;

        public PDKTenMStartMatchRecording(object operate)
        {
            this.operate = (PDKMStartMatchOperate)operate;
        }

        public override void execute()
        {
            var match = PDKTenMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();

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
