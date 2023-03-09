using cambrian.common;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收巴牌
    /// </summary>
    public class ReplayRecvKongProcess: Process
    {
        private KongOperate operate;
        public ReplayRecvKongProcess(BaseOperate operate)
        {
            this.operate = (KongOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            ArrayList<int> handcards = CPMatch.match.getPCards()[this.operate.index].delHandCard(this.operate.card, 1);

            CPMatch.match.setStage(operate.stage);
            panel.refreshHandCards(this.operate.index,handcards.toArray());

            CPMatch.match.getPCards()[this.operate.index].getSameFixedCards(this.operate.card, 3).addSameCards(this.operate.card);
            panel.refreshFixed(this.operate.index);

            panel.showCard(this.operate.index, this.operate.card, SendView.KONG);

            this.operate.playOver();
        }
    }
}
