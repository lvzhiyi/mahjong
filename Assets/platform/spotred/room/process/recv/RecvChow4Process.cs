using cambrian.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 吃成4根
    /// </summary>
    public class RecvChow4Process : Process
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        int operate;

        private Chow4Operate op;

        public RecvChow4Process(Chow4Operate op)
        {
            this.op = op;
            this.operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.showCard(op.index, op.card, SendView.CHOW4);
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
