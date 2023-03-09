using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收招碰消息
    /// </summary>
    public class ReplayRecvZhaoPongCardsProcess : Process
    {
        private ZhaoPongOperate operate;
        public ReplayRecvZhaoPongCardsProcess(BaseOperate operate)
        {
            this.operate = (ZhaoPongOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card,SendView.ZHAOPONG);
            this.operate.playOver();
        }
    }
}
