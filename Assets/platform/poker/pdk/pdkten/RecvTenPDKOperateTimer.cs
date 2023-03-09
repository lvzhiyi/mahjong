namespace platform.poker
{
    /// <summary> 跑得快 执行操作队列 </summary>
    public class RecvTenPDKOperateTimer : PKRecvOperateTimer
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
                    new PDKTenMStartGameProcess(operate).execute();
                    break;
                case PDKMatchMsg.DEALCARDS:
                    new PDKTenMSystemDealCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.START_MATCH:
                    new PDKTenMStartMatchProcess(operate).execute();
                    break;
                case PDKMatchMsg.SHOWCARD:
                    new PDKTenMShowCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.OVER:
                    new PDKTenMOverProcess(operate).execute();
                    break;
                case PDKMatchMsg.CANCEL:
                    new PDKTenMCancelProcess(operate).execute();
                    break;
            }
        }
    }
}
