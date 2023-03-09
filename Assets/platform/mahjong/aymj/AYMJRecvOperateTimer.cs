namespace platform.mahjong
{
    public class AYMJRecvOperateTimer: MJBaseRecvOperateTimer
    {
        public override void exeOperate(int type, MJBaseOperate operate)
        {
            switch (type)
            {
                case RecvAYMJMatchMsg.MSG_START:
                    new AYMJMatchStartProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DEALCARDS:
                    new AYMJMatchDealCardsProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE:
                    new AYMJMatchSingleReplaceProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE_ALL:
                    new AYMJMatchAllReplaceProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE:
                    new AYMJMatchSingDistypeProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE_ALL:
                    new AYMJMatchAllDisTypeProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DRAW:
                    new AYMJMatchDrawProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISCARD:
                    new AYMJMatchDiscardProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PONG:
                    new AYMJMatchPongProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_KONG:
                    new AYMJMatchKongProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_HU:
                    new AYMJMatchHuProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_OVER:
                    new AYMJMatchOverProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_CANCEL:
                    new AYMJMatchCancelProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_UPDATE_STATES:
                    new AYMJMatchUpdateStatesProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_ROOM_REMOVE_PLAYER:
                    new AYMJMatchKickPlayerProcess(operate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DELAY:
                    new AYMJMatchDelayProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO:
                    new AYMJMatchSingPiaoProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO_OVER:
                    new AYMJMatchAllPiaoOverPrcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_BAOPAI:
                    new AYMJMatchBaoPaiProcess(operate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_BAO_KONG:
                    new AYMJMatchBaoKongProcess(operate).execute();
                    break;
            }
        }
    }
}
