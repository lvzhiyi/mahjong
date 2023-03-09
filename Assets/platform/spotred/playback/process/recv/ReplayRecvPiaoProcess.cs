using platform.spotred.room;
using platform.spotred;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放接收飘
    /// </summary>
    public class ReplayRecvPiaoProcess:Process
    {
        PiaoOperate operate;
        public ReplayRecvPiaoProcess(BaseOperate operate)
        {
            this.operate = (PiaoOperate)operate;
        }

        public override void execute()
        {
            CPMatch.match.getPCards()[operate.index].setStatus(CPPlayerCards.STATUS_PIAO);
            ReplaySpotRoomPanel panel= UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            int index = RoomPanel.getPlayerIndex(operate.index);
            panel.headView.showPiao(index,true);
            operate.playOver();
        }
    }
}
