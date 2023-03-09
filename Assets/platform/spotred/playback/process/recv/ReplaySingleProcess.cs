using cambrian.common;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 单张偷
    /// </summary>
    public class ReplaySingleProcess:Process
    {
        private SingleOperate operate;
        public ReplaySingleProcess(BaseOperate operate)
        {
            this.operate = (SingleOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch match = CPMatch.match;
            match.setStage(operate.stage);

            if (this.operate.ishandcard)
            {
                ArrayList<int> handcards = match.getPCards()[this.operate.index].delHandCard(this.operate.card, 1);
                panel.allHandView.selfView.getReplayHandView().showHandCard(handcards.toArray()); //刷新手牌
            }

            match.getPCards()[this.operate.index].addFixCard(FixedCards.SINGLE, new[] { this.operate.card });
            panel.refreshFixed(this.operate.index);

            panel.allHandView.selfView.getReplayHandView().showHandCard(panel.getSelfHandCard());

            match.ResetPlayerCard();

            panel.refreshClock(this.operate.round);

            panel.hideFanCard();

            panel.hideAllPlayCard();

            panel.refreshFuShu();
            this.operate.playOver();
        }
    }
}
