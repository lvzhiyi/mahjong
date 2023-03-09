using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收吃吐消息
    /// </summary>
    public class RecvChowTuCardsProcess:Process
    {
        int operate;//操作类型

        private ChowTuOperate op;
        public RecvChowTuCardsProcess(ChowTuOperate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.showCard(op.index, op.card, SendView.CHOWTU);
            panel.setOperate(operate);
            CPMatch.match.step= op.step;
            CPMatch.match.setStage(op.stage);

            SoundManager.playChiTu(Room.room.players[op.index].player.sex);
            if (operate > 0)
            {
                panel.operateView.showButton(operate, op.paidui);
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), op.disableCard);
            }

            panel.refreshClock(op.round);

            if (operate == OperateView.CAN_DISCARD)
            {
                panel.allHandView.selfView.showHuaDong();
            }
            else
            {
                panel.allHandView.selfView.hideHuaDong();
            }
            panel.showTextinfo(false);

            this.op.isOver = true;
        }
    }
}
