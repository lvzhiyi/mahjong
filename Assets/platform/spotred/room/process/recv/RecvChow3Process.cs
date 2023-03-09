using cambrian.game;
using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 吃成坎
    /// </summary>
    public class RecvChow3Process:Process
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        int operate;

        private Chow3Operate op;

        public RecvChow3Process( Chow3Operate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.showCard(op.index, op.card, SendView.CHOW3);
            panel.setOperate(operate);
            CPMatch.match.step= op.step;
            CPMatch.match.setStage(op.stage);
            SoundManager.playChiChengKan(Room.room.players[op.index].player.sex);

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
