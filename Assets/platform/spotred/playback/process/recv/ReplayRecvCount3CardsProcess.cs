using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    public class ReplayRecvCount3CardsProcess : Process
    {
        private Count3Operate operate;

        public ReplayRecvCount3CardsProcess(BaseOperate operate)
        {
            this.operate = (Count3Operate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match.setStage(operate.stage);

            panel.showCard(this.operate.index, this.operate.card, SendView.DIAOKAN);

            panel.refreshClock(this.operate.round);

            this.operate.playOver();
        }
    }
}
