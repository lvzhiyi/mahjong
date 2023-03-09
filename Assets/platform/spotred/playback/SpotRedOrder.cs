using basef.player;
using cambrian.common;
using platform.spotred.room;

namespace platform.spotred.playback
{
    public class SpotRedOrder
    {
        public BaseOperate recvOperate = null;
        /// <summary>
        /// 场景
        /// </summary>
        public CPMatch scene;

        public void byteRead(ByteBuffer data,int selfIndex)
        {
            int type = data.readInt();
            int step = data.readInt(); //步数
            int stage = data.readInt(); ///当前比赛阶段
            int round = data.readInt(); //当前比赛回合
            int paidui = data.readInt(); //当前牌堆剩余牌数
            int lengths = data.readInt();
            int[] operates = new int[lengths];
            for (int i = 0; i < lengths; i++)
                operates[i] = data.readInt();
            breadOt(type,step,operates,selfIndex,stage,round,paidui,data);
        }

        public void breadOt(int type,int step, int[] operates,int selfIndex,int stage,int round,int paidui,ByteBuffer data)
        {
            bytesOperate(type, step, operates, selfIndex, stage, round, paidui);
            recvOperate.bytesRead(data);
        }


        public void bytesOperate(int type,int step,int[] operates,int selfIndex,int stage,int round,int paidui)
        {
            switch (type)
            {
                case RecvCPMatchMsg.MSG_MATCH_START://比赛开始
                    recvOperate=new ReplayMatchStartOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_PIAO://飘
                    recvOperate=new PiaoOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_DEALCARDS:
                    recvOperate = new DealCardsOverOperate(type, step, operates, selfIndex, stage, round,
                        paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_DANG:
                    recvOperate = new DangOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_SINGLE:
                    recvOperate = new SingleOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_OPENCARD: //翻牌
                    recvOperate = new OpenCardOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_DISCARD: //出牌
                    recvOperate = new DisCardOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_HU: //胡
                    recvOperate = new HuOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_PONG: //碰
                    recvOperate = new PongOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_DRAW:
                    recvOperate = new DrawOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_BAOPAI: //报牌
                    recvOperate = new BaopaiOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CHOW: //吃
                    recvOperate = new ChowOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_ZHAOPONG:
                    recvOperate = new ZhaoPongOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_ZHAOCHOW:
                    recvOperate = new ZhaoChowOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CHOWTU:
                    recvOperate = new ChowTuOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_COUNT4:
                    recvOperate = new Count4Operate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_COUNT3: //掉坎
                    recvOperate = new Count3Operate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CANCEL: //过
                    recvOperate = new CancelOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CHOW3:
                    recvOperate = new Chow3Operate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CHAIDUI: //撕对
                    recvOperate = new ChaiDuiOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_SHOWFIXED:
                    recvOperate = new ShowFixedOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_SLIP://滑牌
                    recvOperate = new SlipOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_OVER:
                    recvOperate = new OverOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_UPDATE_STATES:
                    recvOperate = new StateOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_KONG:
                    recvOperate = new KongOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_KONG4:
                    recvOperate = new Kong4Opreate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_ZHAN_PAI:
                    recvOperate=new ZhaPaiOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_PO_DUI:
                    recvOperate=new PoDuiOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CAN_PO_DUI:
                    recvOperate=new CanPoDuiOperate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_CHOW4:
                    recvOperate = new Chow4Operate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
                case RecvCPMatchMsg.MSG_AFTER7:
                    recvOperate = new After7Operate(type, step, operates, selfIndex, stage, round, paidui, true);
                    break;
            }
        }

        public CPMatch executeOrder(Room room)
        {
            ReplaySpotRoomPanel mpanel=setData(room);
            executeOperates();
            mpanel.showOperates(recvOperate.operates,recvOperate.paidui);
            return CPMatch.match;
        }

        public ReplaySpotRoomPanel setData(Room room)
        {
            if (this.scene != null)
                this.scene.setRoomRule(room.roomRule);
            ReplaySpotRoomPanel mpanel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();

            if (CPMatch.match != null)
            {
                int mine = 0;
                for (int i = 0; i < room.players.Length; i++)
                {
                    if (Player.player.userid == room.players[i].player.userid)
                    {
                        mine = i;
                    }
                }
                CPMatch.match.setPlayers(room.players, mine);
            }

            return mpanel;
        }

        public void executeOperates()
        {
            if (recvOperate.type == RecvCPMatchMsg.MSG_DEALCARDS)
            {
                new ReplayRecvDealCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type== RecvCPMatchMsg.MSG_MATCH_START)//游戏开始
            {
                new ReplayRecvMatchStartProcess(recvOperate).execute();
            }
            else if (recvOperate.type== RecvCPMatchMsg.MSG_PIAO)
            {
                new ReplayRecvPiaoProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_DANG)
            {
                new ReplayDangProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_SINGLE)
            {
                new ReplaySingleProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_OPENCARD)
            {
                new ReplayRecvOpenCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_DRAW)
            {
                new ReplayRecvDrawCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_DISCARD)
            {
                new ReplayRecvDisCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_HU) //胡
            {
                new ReplayRecvHuCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_PONG) //碰
            {
                new ReplayRecvPongCardProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CHOW) //吃
            {
                new ReplayRecvChowCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_BAOPAI) //报牌
            {
                new ReplayRecvBaoPaiProcess(recvOperate).execute();
            }
            else if (recvOperate.type== RecvCPMatchMsg.MSG_CANCEL)
            {
                new ReplayCancelProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_ZHAOPONG)
            {
                new ReplayRecvZhaoPongCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_ZHAOCHOW)
            {
                new ReplayRecvZhaoChowCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CHOWTU)
            {
                new ReplayRecvChowTuCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_COUNT4)
            {
                new ReplayRecvCount4CardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_COUNT3) //掉坎
            {
                new ReplayRecvCount3CardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CHAIDUI) //撕对
            {
                new ReplayRecvChaiDuiProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CHOW3)
            {
                new ReplayRecvChow3Process(recvOperate).execute();
            }

            else if (recvOperate.type == RecvCPMatchMsg.MSG_SHOWFIXED)
            {
                new ReplayRecvShowFixedCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_SLIP)
            {
                new ReplayRecvSlipCardsProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_OVER)
            {
                new ReplayRecvOverProcess(recvOperate).execute();
            }

            else if (recvOperate.type == RecvCPMatchMsg.MSG_UPDATE_STATES)
            {
                new ReplayRecvUpdateStateProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_KONG4)
            {
                new ReplayRecvKong4Process(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_KONG) //巴杠
            {
                new ReplayRecvKongProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_ZHAN_PAI)
            {
                new ReplayRecvZhaPaiProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_PO_DUI)//破对
            {
                new ReplayRecvPoDuiProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CAN_PO_DUI)
            {
                new ReplayRecvCanPoDuiProcess(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_CHOW4)
            {
                new ReplayRecvChow4Process(recvOperate).execute();
            }
            else if (recvOperate.type == RecvCPMatchMsg.MSG_AFTER7)
            {
                new ReplayRecvAfter7Process(recvOperate).execute();
            }
        }

    }
}