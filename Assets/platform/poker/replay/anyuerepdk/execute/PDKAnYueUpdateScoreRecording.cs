namespace platform.poker
{
    public class PDKAnYueUpdateScoreRecording : Process
    {
        private PDKAnYueUpdateScoreOperate operate;

        private PDKAnYueRoomPanel panel;

        private PDKAnYueMatch match;

       

        public PDKAnYueUpdateScoreRecording(PKBaseOperate operateData)
        {
            operate = (PDKAnYueUpdateScoreOperate)operateData;
        }

        public override void execute()
        {
            if (operate.operateData.stage != PKSTAGE.MATCH) return;
            panel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();
            match = PDKAnYueMatch.match;

            match.setBase(operate.operateData.type,
                  operate.operateData.stage,
                  operate.operateData.paidui,
                  operate.operateData.step,
                  operate.operateData.round,
                  operate.operateData.operates);
        
            panel.gameView.clock.showClock(operate.operateData.round);

            match.multiple.changeBoom(operate.index);
            panel.topView.setMuitplier(match.multiple.boomPoint);
            operate.playOver();
        }
    }
}
