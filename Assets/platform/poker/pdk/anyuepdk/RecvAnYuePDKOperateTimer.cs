namespace platform.poker
{
    /// <summary> 跑得快 执行操作队列 </summary>
    public class RecvAnYuePDKOperateTimer : PKRecvOperateTimer
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
                    new PDKAnYueMStartGameProcess(operate).execute();
                    break;
                case PDKMatchMsg.DEALCARDS:
                    new PDKAnYueMSystemDealCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.START_MATCH:
                    new PDKAnYueMStartMatchProcess(operate).execute();
                    break;
                case PDKMatchMsg.SHOWCARD:
                    new PDKAnYueMShowCardProcess(operate).execute();
                    break;
                case PDKMatchMsg.OVER:
                    new PDKAnYueMOverProcess(operate).execute();
                    break;
                case PDKMatchMsg.CANCEL:
                    new PDKAnYueMCancelProcess(operate).execute();
                    break;
                case PDKMatchMsg.UPDATE_MATCH_SCORE:
                    new PDKAnYueUpdateScoreProcess(operate).execute();
                    break;
                    
            }
        }
    }
}
