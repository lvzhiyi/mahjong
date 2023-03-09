using cambrian.common;
using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收吃牌消息
    /// </summary>
    public class ReplayRecvChowCardsProcess : Process
    {
        private ChowOperate operate;

        public ReplayRecvChowCardsProcess(BaseOperate operate)
        {
            this.operate = (ChowOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            //删除手牌
            ArrayList<int> list= CPMatch.match.getPCards()[this.operate.index].delHandCard(this.operate.card, 1);

            ///
            CPMatch.match.getPCards()[this.operate.index].addFixCard(FixedCards.CHOW, new[] { this.operate.destCard, this.operate.card });
            CPMatch.match.setStage(operate.stage);

            panel.refreshDisCard(this.operate.destindex);

            ///刷新吃牌人的手牌区
            panel.refreshHandCards(this.operate.index,list.toArray());

            panel.refreshClock(this.operate.round);

            CPMatch.match.ResetPlayerCard();

            panel.hideFanCard();

            panel.hideAllPlayCard();

            SoundManager.playChi(Room.room.players[this.operate.index].player.sex,CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));// 播放吃声音

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
