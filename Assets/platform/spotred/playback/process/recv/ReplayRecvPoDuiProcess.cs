using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    public class ReplayRecvPoDuiProcess : Process
    {
        private PoDuiOperate operate;

        public ReplayRecvPoDuiProcess(BaseOperate operate)
        {
            this.operate = (PoDuiOperate) operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(operate.index, operate.card, SendView.PODUI);
            this.operate.playOver();
        }
    }
}
