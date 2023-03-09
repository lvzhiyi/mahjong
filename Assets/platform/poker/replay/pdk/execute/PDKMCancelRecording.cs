namespace platform.poker
{
    public class PDKMCancelRecording : Process
    {
        private PDKMCancelOperate operate;

        private PPDKReplayRoomPanel panel;

        private PDKMatch match;

        public PDKMCancelRecording(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            match = PDKMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>();

            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKMShowCardRecording(operate.operateData, operate.index).execute();
                    break;
            }
            operate.playOver();
        }
    }
}
