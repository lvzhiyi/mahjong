namespace platform.poker
{
    public class DDZMCancelRecording : Process
    {
        private DDZMCancelOperate operate;

        private PDDZReplayRoomPanel panel;

        private DDZMatch match;

        public DDZMCancelRecording(object operate)
        {
            this.operate = (DDZMCancelOperate)operate;
        }

        public override void execute()
        {
            match = DDZMatch.match;
            panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            switch (operate.type)
            {
                case PKCancelType.Landlord_Call_NO:
                    new DDZMLandlordCallRecording(operate.operateData, operate.index).execute();
                    break;
                case PKCancelType.Landlord_Grab_NO:
                    new DDZMLandlordGrabRecording(operate.operateData, operate.index).execute();
                    break;
                case PKCancelType.JIABEI_NO:
                    new DDZMJiaBeiRecording(operate.operateData, operate.index).execute();
                    break;
                case PKCancelType.PASS:
                    new DDZMShowCardRecording(operate.operateData, operate.index).execute();
                    break;
                case PKCancelType.CALLSCORE:
                    new DDZMCallScoreRecording(operate.operateData, operate.index).execute();
                    break;
            }
            operate.playOver();
        }
    }
}
