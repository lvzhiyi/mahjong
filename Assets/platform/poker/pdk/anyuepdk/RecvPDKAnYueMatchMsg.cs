using platform.bean;

namespace platform.poker
{
    public class RecvPDKAnYueMatchMsg
    {
        public PKBaseOperate recvOperate(int type, int step, int[] operates, int stage, int round, int paidui)
        {
            PKBaseOperate operate = null;

            OperateData data = new OperateData(type, step, operates, stage, round, paidui);

            switch (type)
            {
                case PDKMatchMsg.START:
                    operate = new PDKAnYueMStartGameOperate(data);
                    break;
                case PDKMatchMsg.DEALCARDS:
                    operate = new PDKMSystemDealCardOperate(data);
                    break;
                case PDKMatchMsg.START_MATCH:
                    operate = new PDKMStartMatchOperate(data);
                    break;
                case PDKMatchMsg.SHOWCARD:
                    operate = new PDKAnYueMShowCardOperate(data);
                    break;
                case PDKMatchMsg.CANCEL:
                    operate = new PDKMCancelOperate(data);
                    break;
                case PDKMatchMsg.OVER:
                    operate = new PDKAnYueMOverOperate(data);
                    break;
                case PDKMatchMsg.UPDATE_MATCH_SCORE:
                    operate = new PDKAnYueUpdateScoreOperate(data);
                    break;
                    
                default: break;
            }
            return operate;
        }
    }
}
