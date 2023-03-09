namespace platform.poker
{
    public class PDKTenMCancelRecording : Process
    {
        private PDKMCancelOperate operate;

        private PPDKTenReplayRoomPanel panel;

        private PDKTenMatch match;

        public PDKTenMCancelRecording(object operate)
        {
            this.operate = (PDKMCancelOperate)operate;
        }

        public override void execute()
        {
            match = PDKTenMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PPDKTenReplayRoomPanel>();

            switch (operate.type)
            {
                case PKCancelType.PASS:
                    new PDKTenMShowCardRecording(operate.operateData, operate.index).execute();
                    break;
            }
            operate.playOver();
        }
    }
}
