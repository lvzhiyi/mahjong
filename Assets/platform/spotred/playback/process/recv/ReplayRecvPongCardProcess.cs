using cambrian.common;
using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收碰牌消息
    /// </summary>
    public class ReplayRecvPongCardProcess : Process
    {
        private PongOperate operate;

        public ReplayRecvPongCardProcess(BaseOperate operate)
        {
            this.operate = (PongOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
       
            int[] cards = null;
            //从手牌区移除牌返回手牌
            ArrayList<int> hand_cards= CPMatch.match.getPCards()[this.operate.index].delAllSameCard(this.operate.card, this.operate.count);

            if (this.operate.count > 2)
            {
                cards=new int[this.operate.count];
                for (int i = 0; i < this.operate.count; i++)
                {
                    cards[i] = this.operate.card;
                }
            }
            else
            {
                cards=new int[3];
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i] = this.operate.card;
                }
            }

            CPMatch.match.setStage(operate.stage);

            panel.addFixed(this.operate.index, FixedCards.PONG, cards);//添加到固定牌区

            panel.refreshHandCards(this.operate.index, hand_cards.toArray());

            panel.refreshDisCard(this.operate.destindex);

            ///重置上一家人打的牌
            CPMatch.match.ResetPlayerCard();

            panel.refreshClock(this.operate.round);

            panel.hideFanCard();

            panel.hideAllPlayCard();

            SoundManager.playPeng(Room.room.players[this.operate.index].player.sex, CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));

            panel.refreshMoveFixed(this.operate.index, animationOver);
        }

        public void animationOver()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            panel.refreshFixed(this.operate.index);
            panel.refreshFuShu();
            this.operate.playOver();
        }
    }
}
