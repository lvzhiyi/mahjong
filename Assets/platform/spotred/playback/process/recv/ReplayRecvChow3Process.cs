using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    public class ReplayRecvChow3Process:Process
    {
        private Chow3Operate operate;

        public ReplayRecvChow3Process(BaseOperate operate)
        {
            this.operate = (Chow3Operate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.CHOW3);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
