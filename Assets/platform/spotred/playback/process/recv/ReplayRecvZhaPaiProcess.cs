using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放，接收炸牌消息
    /// </summary>
    public class ReplayRecvZhaPaiProcess:Process
    {
        private ZhaPaiOperate operate;
        public ReplayRecvZhaPaiProcess(BaseOperate operate)
        {
            this.operate = (ZhaPaiOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
           
            panel.showPlayCard(operate.index,operate.card,false);

            if (operate.isRemoveHandCard)
            {
                int[] cards = CPMatch.match.removeHandCard(operate.index, operate.card,1).toArray();
                panel.refreshHandCards(operate.index,cards);
            }
            else
            {
                panel.setPMCard(operate.card);
                CPMatch.match.setLastPlayerCard(operate.index,operate.card);
            }

           
            panel.refreshClock(this.operate.round);

            panel.refreshFuShu();

            if (!operate.isRemoveHandCard)
                SoundManager.playChangPai(Room.room.players[operate.index].player.sex, Card.getName(operate.card), CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));
            this.operate.playOver();
        }
    }
}
