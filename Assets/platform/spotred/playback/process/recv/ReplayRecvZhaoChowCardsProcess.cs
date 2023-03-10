using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收招吃消息
    /// </summary>
    public class ReplayRecvZhaoChowCardsProcess : Process
    {
        private ZhaoChowOperate operate;

        public ReplayRecvZhaoChowCardsProcess(BaseOperate operate)
        {
            this.operate = (ZhaoChowOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match.setStage(operate.stage);
            panel.showCard(this.operate.index, this.operate.card, SendView.ZHAOCHOW);

            this.operate.playOver();
        }
    }
}
