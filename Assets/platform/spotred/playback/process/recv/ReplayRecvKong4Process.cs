using cambrian.common;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收杠4张
    /// </summary>
    public class ReplayRecvKong4Process:Process
    {
        private Kong4Opreate operate;
        public ReplayRecvKong4Process(BaseOperate operate)
        {
            this.operate = (Kong4Opreate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            ArrayList<int> handcards = CPMatch.match.getPCards()[this.operate.index].delHandCard(this.operate.card, 4);

            CPMatch.match.setStage(operate.stage);
            ///刷新吃牌人的手牌区
            panel.refreshHandCards(this.operate.index, handcards.toArray());

            CPMatch.match.getPCards()[this.operate.index].addFixCard(FixedCards.KONG, new[] { this.operate.card, this.operate.card, this.operate.card, this.operate.card });
            panel.refreshFixed(this.operate.index);

            this.operate.playOver();
        }
    }
}
