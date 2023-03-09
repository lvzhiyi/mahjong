using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收滑牌
    /// </summary>
    public class ReplayRecvSlipCardsProcess : Process
    {
        private SlipOperate operate;
        public ReplayRecvSlipCardsProcess(BaseOperate operate)
        {
            this.operate =(SlipOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch match = CPMatch.match;
            match.getPCards()[this.operate.index].fixCards.clear();
            match.getPCards()[this.operate.index].addFixCard(this.operate.cards);
            match.getPCards()[this.operate.index].handcards.clear();
            match.getPCards()[this.operate.index].AddHandCards(this.operate.draws);

            match.setStage(operate.stage);

            panel.refreshFixed(this.operate.index);
            panel.refreshHandCards(this.operate.index, this.operate.draws);

            match.paidui=this.operate.paidui;
            panel.refreshCardNum();

            panel.refreshFuShu();
            panel.refreshClock(this.operate.round);
            this.operate.playOver();
        }
    }
}
