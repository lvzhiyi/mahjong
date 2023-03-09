using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    public class ReplayRecvHuCardsProcess : Process
    {
        private HuOperate operate;
        public ReplayRecvHuCardsProcess(BaseOperate operate)
        {
            this.operate = (HuOperate)operate;
        }


        public override void execute()
        {

            if (operate.indexs == null || operate.indexs.Length == 0) return;

            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            CPMatch.match.setStage(operate.stage);

            //播放胡的动画
            for (int i = 0; i < operate.indexs.Length ; i++)
            {
                panel.showHu(operate.indexs[i]);
            }

            if (operate.indexs != null) SoundManager.playHu(Room.room.players[operate.indexs[0]].player.sex, CPMatch.match.getRoomRule().rule.getIntAtribute("soundType"));

            Replay.hu_card = operate.hu_card;
            this.operate.playOver();
        }
    }
}
