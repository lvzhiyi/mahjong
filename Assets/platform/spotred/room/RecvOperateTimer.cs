using cambrian.common;
using UnityEngine;

namespace platform.spotred.room
{
    public class RecvOperateTimer : MonoBehaviour
    {
        private long time;

        Queue<BaseOperate> recvOperates = new Queue<BaseOperate>(100);

        public delegate void ActionBack(ByteBuffer data);

        public ActionBack action;

        private ByteBuffer data;

        public void setAction(ActionBack action, ByteBuffer data)
        {
            this.data =new ByteBuffer(data.toArray());
            this.action = action;
          
        }

        public BaseOperate removeFirstRecvOperate()
        {
            if (recvOperates.isEmpty()) return null;

            return recvOperates.remove();
        }

        public void addOperate(BaseOperate operate)
        {
            this.recvOperates.add(operate);
        }

        /// <summary>
        /// 清空接收操作队列
        /// </summary>
        public void clearBaseOperate()
        {
            this.recvOperates.clear();
        }

        private BaseOperate operate;

        void Update()
        {
            long curtime = TimeKit.currentTimeMillis;

            if (time == 0)
            {
                time = curtime;
                return;
            }

            if (operate == null)
            {
                operate = removeFirstRecvOperate();
                if (operate != null)
                {
                    time = curtime + operate.getWaittime();
                }
            }

            if (operate != null)
            {
                if (!operate.isStart)
                {
                    operate.isStart = true;
                    exec(operate);
                }
                else if (operate.isOver && curtime >= time)
                {
                    time = curtime;
                    operate = null;
                }
            }

            if (operate == null && recvOperates.isEmpty())
            {
                if (action != null)
                {
                    action(data);
                    action = null;
                }
            }
        }

        public void exec(BaseOperate operate)
        {
            exeOperate(operate.type, operate);
        }

        public virtual void exeOperate(int type, BaseOperate operate)
        {
            switch (type)
            {
                case -1:
                    DealCardsOverOperate recvop = (DealCardsOverOperate)recvOperates.get();
                    new RecvDealCardsProcess(operate.step, 0, 0, 0, 0, recvop.cards, operate.operates[Room.room.indexOf()], recvop.disablecard, recvop.stage, recvop.paidui, operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_MATCH_START:
                    new RecvMatchStartProcess((MatchStartOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_PIAO:
                    new RecvPiaoProcess((PiaoOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_DEALCARDS:
                    new RecvDealCardsOverProcess((DealCardsOverOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_SINGLE:
                    new RecvSingleProcess((SingleOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_DANG:
                    new RecvDangProcess((DangOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_OPENCARD: //翻牌
                    new RecvOpenCardsProcess((OpenCardOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_DISCARD: //出牌
                    new RecvDisCardsProcess((DisCardOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_HU: //胡
                    new RecvHuCardsProcess((HuOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_PONG: //碰
                    new RecvPongCardProcess((PongOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_DRAW:
                    new RecvDrawCardsProcess((DrawOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_BAOPAI: //报牌
                    new RecvBaoPaiProcess((BaopaiOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CHOW: //吃
                    new RecvChowCardsProcess((ChowOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_ZHAOPONG:
                    new RecvZhaoPongCardsProcess((ZhaoPongOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_ZHAOCHOW:
                    new RecvZhaoChowCardsProcess((ZhaoChowOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CHOWTU:
                    new RecvChowTuCardsProcess((ChowTuOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_COUNT4:
                    new RecvCount4CardsProcess((Count4Operate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_COUNT3: //掉坎
                    new RecvCount3CardsProcess((Count3Operate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CHOW3:
                    new RecvChow3Process((Chow3Operate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CHAIDUI: //撕对
                    new RecvChaiDuiProcess((ChaiDuiOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_SHOWFIXED:
                    new RecvShowFixedCardsProcess((ShowFixedOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_SLIP:
                    new RecvSlipCardsProcess((SlipOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_OVER:
                    new RecvOverProcess((OverOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_UPDATE_STATES:
                    new RecvUpdateStateProcess((StateOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_KONG:
                    new RecvKongProcess((KongOperate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_KONG4:
                    new RecvKong4Process((Kong4Opreate) operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CANCEL:
                    new RecvCancelProcess((CancelOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_ZHAN_PAI:
                    new RecvZhaPaiProcess((ZhaPaiOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_PO_DUI:
                    new RecvPoDuiProcess((PoDuiOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CAN_PO_DUI:
                    new RecvCanPoDuiProcess((CanPoDuiOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CHOW4:
                    new RecvChow4Process((Chow4Operate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_AFTER7:
                    new RecvAfter7Process((After7Operate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_REMOVE_ROOM://金币场自己被退出房间（服务器端主动移除）
                    new RemoveRoomProcess((RemoveRoomOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_REMOVE_PLAYER://金币场移除玩家（服务器端主动移除）
                    new RemovePlayerProcess((RemovePlayerOperate)operate).execute();
                    break;
                case RecvCPMatchMsg.MSG_CLUB_GOLD_ROOM_REMOVE_PLAYER://金币房卡场移除玩家（服务器端主动移除）
                    new RemoveClubGoldPlayerProcess((RemoveClubGoldPlayerOperate)operate).execute();
                    break;
            }
        }
    }
}
