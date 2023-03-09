namespace platform.poker
{
    public class DDZMMingPaiCancelRecording : Process
    {
        private DDZMMingPaiCancelOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMMingPaiCancelRecording(object operate)
        {
            this.operate = (DDZMMingPaiCancelOperate)operate;
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

            panel.gameView.stage.hidePStatus();                          

            operate.playOver();
        }
    }
}