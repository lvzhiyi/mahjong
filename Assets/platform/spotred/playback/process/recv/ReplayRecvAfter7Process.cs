using platform.spotred.room;

/// <summary>
/// 回放吃成4根
/// </summary>
namespace platform.spotred.playback
{
    public class ReplayRecvAfter7Process : Process
    {
        private After7Operate operate;

        public ReplayRecvAfter7Process(BaseOperate operate)
        {
            this.operate = (After7Operate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.AFTER7);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
