using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收偷成4根消息
    /// </summary>
    public class ReplayRecvCount4CardsProcess : Process
    {
        private Count4Operate operate;

        public ReplayRecvCount4CardsProcess(BaseOperate operate)
        {
            this.operate = (Count4Operate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.COUNT4);
            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
