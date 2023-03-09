using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收推挡操作
    /// </summary>
    public class RecvDangProcess:Process
    {
        private DangOperate op;

        private int operate;

        public RecvDangProcess(DangOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }


        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch.match.step=op.step;
            CPMatch.match.setStage(op.stage);

            SoundManager.playDang(Room.room.players[op.index].player.sex);


            if (operate == OperateView.CAN_DISCARD)
            {
                panel.refreshHandCard(new int[0]);
                panel.allHandView.selfView.showHuaDong();
            }
            else
            {
                panel.allHandView.selfView.hideHuaDong();
            }

            int count = panel.operateView.showButton(operate, op.paidui);
            if (count > 0)
            {
                panel.refreshHandCard(new int[0]);
            }

            CPMatch.match.dangjia = op.index;
            panel.refreshClock(op.round);
            panel.refreshFuShu();
            panel.showTextinfo(false);

            op.isOver = true;
        }
    }
}
