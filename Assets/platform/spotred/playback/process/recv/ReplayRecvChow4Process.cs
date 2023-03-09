using platform.spotred.room;

/// <summary>
/// 回放吃成4根
/// </summary>
namespace platform.spotred.playback
{
    public class ReplayRecvChow4Process : Process
    {
        private Chow4Operate operate;

        public ReplayRecvChow4Process(BaseOperate operate)
        {
            this.operate = (Chow4Operate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.CHOW4);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
