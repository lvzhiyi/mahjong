using cambrian.game;
using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放接收报牌
    /// </summary>
    public class ReplayRecvBaoPaiProcess:Process
    {
        private BaopaiOperate operate;
        public ReplayRecvBaoPaiProcess(BaseOperate operate)
        {
            this.operate = (BaopaiOperate)operate;
        }

        public override void execute()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            CPMatch.match.setStage(operate.stage);
            CPMatch.match.getPCards()[operate.index].setStatus(CPPlayerCards.STATUS_PIAO);
            panel.playAnimation(this.operate.index, MovieClipView.MC_TYPE_BAO);
            SoundManager.playBaoPai(Room.room.players[this.operate.index].player.sex);
            panel.refreshBaoPaiIndex(this.operate.index);
            this.operate.playOver();
        }
    }
}
