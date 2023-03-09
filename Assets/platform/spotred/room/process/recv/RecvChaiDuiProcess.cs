using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    public class RecvChaiDuiProcess:Process
    {
        int operate;//操作类型

        private ChaiDuiOperate op;

        public RecvChaiDuiProcess(ChaiDuiOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.showCard(op.index, op.card, SendView.SHIDUI);
            panel.setOperate(operate);
            CPMatch.match.step= op.step;
            CPMatch.match.setStage(op.stage);
            SoundManager.playShiDui(Room.room.players[op.index].player.sex);

            if (operate > 0)
            {
                panel.operateView.showButton(operate, op.paidui);
                panel.allHandView.selfView.getHandView().showHandCard(panel.getSelfHandCard(), op.disableCard);
            }

            panel.refreshClock(op.round);

            if (operate == OperateView.CAN_DISCARD)
                panel.allHandView.selfView.showHuaDong();
            else
                panel.allHandView.selfView.hideHuaDong();

            panel.showTextinfo(false);
            op.isOver = true;
        }
    }
}
