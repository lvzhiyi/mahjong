namespace platform.poker
{
    /// <summary> 跑得快 执行操作队列 </summary>
    public class RecvPDKOperateTimer : PKRecvOperateTimer
    {
        private void Update()
        {
            base.update();
        }

        public override void exec(PKBaseOperate operate)
        {
            ddzExeOperate(operate);
        }

        private void ddzExeOperate(PKBaseOperate operate)
        {
            switch (operate.operateData.type)
            {
                case PDKMatchMsg.START:
                    new PDKMStartGameProcess(operate).execute();
                    break;
                case PDKMatchMsg.DEALCARDS:
                    new PDKMSystemDealCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.START_MATCH:
                    new PDKMStartMatchProcess(operate).execute();
                    break;
                case PDKMatchMsg.SHOWCARD:
                    new PDKMShowCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.OVER:
                    new PDKMOverProcess(operate).execute();
                    break;
                case PDKMatchMsg.CANCEL:
                    new PDKMCancelProcess(operate).execute();
                    break;
            }
        }
    }
}
