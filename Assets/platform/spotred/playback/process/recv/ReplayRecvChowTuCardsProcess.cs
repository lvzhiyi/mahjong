using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收吃吐消息
    /// </summary>
    public class ReplayRecvChowTuCardsProcess : Process
    {
        private ChowTuOperate operate;
        public ReplayRecvChowTuCardsProcess(BaseOperate operate)
        {
            this.operate = (ChowTuOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.CHOWTU);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
