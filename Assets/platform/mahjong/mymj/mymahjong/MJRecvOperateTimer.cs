namespace platform.mahjong
{
    public class MJRecvOperateTimer: MJBaseRecvOperateTimer
    {
        public override void exeOperate(int type, MJBaseOperate operate)
        {
            switch (type)
            {
                case RecvMJMatchMsg.MSG_START:
                    new MJMatchStartProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DEALCARDS:
                    new MJMatchDealCardsProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_REPLACE:
                    new MJMatchSingleReplaceProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_REPLACE_ALL:
                    new MJMatchAllReplaceProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE:
                    new MJMatchSingDistypeProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE_ALL:
                    new MJMatchAllDisTypeProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DRAW:
                    new MJMatchDrawProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISCARD:
                    new MJMatchDiscardProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PONG:
                    new MJMatchPongProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_KONG:
                    new MJMatchKongProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_HU:
                    new MJMatchHuProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_OVER:
                    new MJMatchOverProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_CANCEL:
                    new MJMatchCancelProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_UPDATE_STATES:
                    new MJMatchUpdateStatesProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_ROOM_REMOVE_PLAYER:
                    new MJMatchKickPlayerProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DELAY:
                    new MJMatchDelayProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PIAO:
                    new MJMatchSingPiaoProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PIAO_OVER:
                    new MJMatchAllPiaoOverPrcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_LIE_CARD:
                    new MJMatchTangProcess(operate).execute();
                    break;
            }
        }
    }
}
