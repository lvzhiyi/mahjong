namespace platform.spotred.room
{
    public class RecvCanPoDuiProcess:Process
    {
        int operate;//操作类型

        private CanPoDuiOperate op;

        public RecvCanPoDuiProcess(CanPoDuiOperate op)
        {
            this.op = op;
            operate = op.operates[op.selfIndex];
        }

        public override void execute()
        {
            if (op.index == op.selfIndex)
            {
                CPMatch.match.getPlayerCardss<CPPlayerCards>()[op.index].addCanpdPlayCards(op.cards);
            }
            op.isOver = true;
        }
    }
}
