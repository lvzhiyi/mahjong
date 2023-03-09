using platform.spotred.room;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收发牌消息
    /// </summary>
    public class ReplayRecvDealCardsProcess : Process
    {
        private DealCardsOverOperate operate;

        public ReplayRecvDealCardsProcess(BaseOperate operate)
        {
            this.operate = (DealCardsOverOperate)operate;
        }

        public override void execute()
        {
            ///开始比赛
            ReplaySpotRoomPanel panel =UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            int[][] cards=new int[operate.cards.Length][];
            for (int i = 0; i < operate.cards.Length; i++)
            {
                int[] c=new int[operate.cards[i].Length];
                for (int j = 0; j < c.Length; j++)
                {
                    c[j] = operate.cards[i][j];
                }

                cards[i] = c;
            }
            CPMatch.match.DealCards(cards);
            CPMatch.match.paidui=operate.paidui;//发完牌后剩余的牌
            panel.refreshCardNum();
            panel.refreshAllHandCards(Room.room.indexOf());
            panel.refreshClock(operate.round);
            panel.refreshFuShu();

            this.operate.playOver();
        }
    }
}
