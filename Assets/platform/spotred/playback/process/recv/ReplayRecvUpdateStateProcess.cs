using platform.spotred.playback;
using platform.spotred;

namespace platform.spotred.room
{
    public class ReplayRecvUpdateStateProcess : Process
    {
        private StateOperate operate;

        public ReplayRecvUpdateStateProcess(BaseOperate operate)
        {
            this.operate = (StateOperate)operate;
        }


        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match.setStage(operate.stage);

            CPMatch.match.dangjia = this.operate.dangjia;

            panel.refreshClock(this.operate.round);
            panel.ShowMatchPlayerInfo();

            this.operate.playOver();
        }
    }
}
