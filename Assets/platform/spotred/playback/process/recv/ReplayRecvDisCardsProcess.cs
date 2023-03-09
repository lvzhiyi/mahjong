using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 接收出牌消息
    /// </summary>
    public class ReplayRecvDisCardsProcess : Process
    {
        private DisCardOperate operate;
        public ReplayRecvDisCardsProcess(BaseOperate operate)
        {
            this.operate = (DisCardOperate)operate;
        }


        CPMatch match;
        public override void execute()
        {
            match = CPMatch.match;
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();


            if (match.addLastPlayerCardToDisable())
            {
                panel.hideDisLastCard(match.getLastPlayerIndex());
                panel.moveShowCardToDisable(match.getLastPlayerIndex(), ab);
                match.ResetPlayerCard();
            }
            else
            {
                ab(false);
            }
        }

        public void ab(bool b)
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            if (b)
            {
                match.ResetPlayerCard();
            }

            match.setStage(operate.stage);

            panel.refreshHandCards(this.operate.index,match.removeHandCard(this.operate.index, this.operate.card, 1).toArray());

            panel.showPlayCard(this.operate.index, this.operate.card, false);

            panel.refreshClock(this.operate.round);

            panel.setPMCard(this.operate.card);

            panel.refreshFuShu();

            match.setLastPlayerCard(this.operate.index, this.operate.card);
            SoundManager.playChangPai(Room.room.players[this.operate.index].player.sex, Card.getName(this.operate.card), CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));
            this.operate.playOver();
        }
    }
}
