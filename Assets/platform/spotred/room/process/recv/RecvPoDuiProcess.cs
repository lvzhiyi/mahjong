using platform.spotred;

namespace platform.spotred.room
{
    public class RecvPoDuiProcess:Process
    {
        int operate;//操作类型

        private PoDuiOperate op;

        public RecvPoDuiProcess(PoDuiOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.showCard(op.index, op.card, SendView.PODUI);
            panel.setOperate(operate);
            CPMatch.match.step = op.step;
            CPMatch.match.setStage(op.stage);

            if (op.index == op.selfIndex)//将破对的牌加入到不能打的牌中去
                (CPMatch.match.getPlayerCardss<CPPlayerCards>()[op.index]).addNoCanPlayCard(op.card);

            //缺少破对的音效

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
