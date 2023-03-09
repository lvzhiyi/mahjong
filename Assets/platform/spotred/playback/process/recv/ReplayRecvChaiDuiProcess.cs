using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 拆对
    /// </summary>
    public class ReplayRecvChaiDuiProcess : Process
    {
        private ChaiDuiOperate operate;

        public ReplayRecvChaiDuiProcess(BaseOperate operate)
        {
            this.operate = (ChaiDuiOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.SHIDUI);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
