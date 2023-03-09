namespace platform.poker
{
    public class PDKAnYueMCancelRecording : Process
    {
        private PDKMCancelOperate operate;

        private PPDKAnYueReplayRoomPanel panel;

        private PDKAnYueMatch match;

        public PDKAnYueMCancelRecording(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            match = PDKAnYueMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();

            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKAnYueMShowCardRecording(operate.operateData, operate.index).execute();
                    break;
            }
            operate.playOver();
        }
    }
}
