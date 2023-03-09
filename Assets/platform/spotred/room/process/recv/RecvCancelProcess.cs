namespace platform.spotred.room
{
    /// <summary>
    /// 金币场用
    /// </summary>
    public class RecvCancelProcess: Process
    {
        private CancelOperate op;

        public RecvCancelProcess(CancelOperate op)
        {
            this.op = op;
        }

        public override void execute()
        {
            SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            if (op.operates[op.selfIndex] == 0)//无操作
                panel.operateView.hideAllBtn();
            op.isOver = true;
        }
    }
}
