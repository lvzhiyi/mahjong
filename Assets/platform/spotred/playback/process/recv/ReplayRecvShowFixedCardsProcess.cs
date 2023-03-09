using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收显示偷牌(这个是完成后,显示所有玩家首轮滑下来的牌)
    /// </summary>
    public class ReplayRecvShowFixedCardsProcess : Process
    {
        private ShowFixedOperate operate;

        public ReplayRecvShowFixedCardsProcess(BaseOperate operate)
        {
            this.operate = (ShowFixedOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel =UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match.setStage(operate.stage);
            
            for (int i = 0; i < operate.cards.Length; i++)
            {
                CPMatch.match.getPCards()[i].fixCards.clear();
                CPMatch.match.getPCards()[i].addFixCard(operate.cards[i]);
                panel.refreshFixed(i);
            }
            panel.refreshFuShu();

            this.operate.playOver();
        }
    }
}
