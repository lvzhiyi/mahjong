using platform.spotred.playback;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收游戏结束
    /// </summary>
    public class ReplayRecvOverProcess : Process
    {
        private OverOperate operate;

        public ReplayRecvOverProcess(BaseOperate operate)
        {
            this.operate = (OverOperate)operate;
        }

        public override void execute()
        {
            ReplayOverPanel overpanel = UnrealRoot.root.getDisplayObject<ReplayOverPanel>();
            CPMatch.match.setStage(operate.stage);
            Room.room.addSpotRedCount(operate.scene.getSpotRedCounts());

            CPMatch scene = operate.scene;
            scene.dangjia = CPMatch.match.dangjia;
            scene.xiaojia = CPMatch.match.xiaojia;

            overpanel.show(Room.room,operate.scene,Replay.hu_card,operate.index);
            overpanel.refresh();
            overpanel.setVisible(true);
            this.operate.playOver();
        }
    }
}
