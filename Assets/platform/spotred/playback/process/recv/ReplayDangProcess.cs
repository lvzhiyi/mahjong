using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    public class ReplayDangProcess:Process
    {
        DangOperate operate;
        public ReplayDangProcess(BaseOperate operate)
        {
            this.operate = (DangOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.dangjia = operate.index;
            CPMatch.match.setStage(operate.stage);
            panel.refreshClock(operate.round);
            panel.ShowMatchPlayerInfo();
            panel.refreshFuShu();

            this.operate.playOver();
        }
    }
}
