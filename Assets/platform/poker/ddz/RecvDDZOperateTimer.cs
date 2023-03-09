namespace platform.poker
{
    using System.Collections;
    using UnityEngine
        ;

    /// <summary> 斗地主 执行操作队列 </summary>
    public class RecvDDZOperateTimer : PKRecvOperateTimer
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
                case DDZMatchMsg.START:
                    new DDZMStartGameProcess(operate).execute();
                    break;
                case DDZMatchMsg.DEALCARDS:
                    new DDZMSystemDealCardProcess(operate).execute();
                    break;
                case DDZMatchMsg.LANDLORDCALL:
                    new DDZMLandlordCallProcess(operate).execute();
                    break;
                case DDZMatchMsg.LANDLORDGRAB:
                    new DDZMLandlordGrabProcess(operate).execute();
                    break;
                case DDZMatchMsg.JIABEI:
                    new DDZMJiaBeiProcess(operate).execute();
                    break;
                case DDZMatchMsg.SHOWCARD:
                    new DDZMShowCardProcess(operate).execute();
                    break;
                case DDZMatchMsg.MINGPAI:
                    new DDZMMingPaiProcess(operate).execute();
                    break;
                case DDZMatchMsg.CANCEL:
                    new DDZMCancelProcess(operate).execute();
                    break;
                case DDZMatchMsg.OVER:
                    new DDZMOverProcess(operate).execute();
                    break;
                case DDZMatchMsg.DEALLADNLORDCARD:
                    new DDZMDealLandlordCardProcess(operate).execute();
                    break;
                case DDZMatchMsg.FLOW:
                    new DDZMFlowGameProcess(operate).execute();
                    break;
                case DDZMatchMsg.MINGPAI_CANCEL:
                    new DDZMMingPaiCancelProcess(operate).execute();
                    break;
                case DDZMatchMsg.LANDLORD_CALLSCORE:
                    new DDZMCallScoreProcess(operate).execute();
                    break;
            }
        }
    }
}
