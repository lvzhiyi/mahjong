using basef.player;
using cambrian.common;

namespace platform.mahjong
{
    public class AYMJOrder
    {
        public MJBaseOperate recvOperate = null;
        /// <summary>
        /// 场景
        /// </summary>
        public AYMJMatch scene;

        public void byteRead(ByteBuffer data, int selfIndex)
        {
            int type = data.readInt();
            breadOt(type, selfIndex, data);
        }

        public void breadOt(int type, int selfIndex, ByteBuffer data)
        {
            bytesOperate(type, selfIndex);
            recvOperate.bytesRead(data);
        }


        public void bytesOperate(int type, int index)
        {
            switch (type)
            {
                case RecvAYMJMatchMsg.MSG_START:
                    recvOperate = new AYMJMatchStartOperate(type, index,true);
                    break;
                case RecvAYMJMatchMsg.MSG_DEALCARDS:
                    recvOperate = new MJMatchDealCardsOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE:
                    recvOperate = new MJMatchSingleReplaceOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE_ALL:
                    recvOperate = new MJMatchAllReplaceOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE:
                    recvOperate = new MJMatchSingleDistypeOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE_ALL:
                    recvOperate = new MJMatchAllDisTypeOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_DRAW:
                    recvOperate = new MJMatchDrawOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_DISCARD:
                    recvOperate = new MJMatchDiscardOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_PONG:
                    recvOperate = new MJMatchPongOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_KONG:
                    recvOperate = new MJMatchKongOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_HU:
                    recvOperate = new MJMatchHuOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_OVER:
                    recvOperate = new AYMJMatchOverOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_CANCEL:
                    recvOperate = new MJMatchCancelOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_UPDATE_STATES:
                    recvOperate = new MJMatchUpdateStateslOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO:
                    recvOperate = new MJMatchSinglePiaoOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO_OVER:
                    recvOperate = new MJMatchAllPiaoOverOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_BAOPAI:
                    recvOperate = new AYMJMatchBaoPaiOperate(type, index);
                    break;
                case RecvAYMJMatchMsg.MSG_BAO_KONG:
                    recvOperate = new AYMJMatchBaoKongOperate(type, index);
                    break;
            }
        }
        
        public AYMJMatch executeOrder(Room room)
        {
            ReplayAYMJRoomPanel mpanel = setData(room);
            executeOperates();
            return AYMJMatch.match;
        }

        public ReplayAYMJRoomPanel setData(Room room)
        {
            if (scene != null)
                scene.setRoomRule(room.roomRule);
            ReplayAYMJRoomPanel mpanel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();

            if (AYMJMatch.match != null)
            {
                int mine = 0;
                for (int i = 0; i < room.players.Length; i++)
                {
                    if (Player.player.userid == room.players[i].player.userid)
                    {
                        mine = i;
                    }
                }
                AYMJMatch.match.setPlayers(room.players, mine);
            }

            return mpanel;
        }

        public void executeOperates()
        {
            int type=recvOperate.type;

            switch (type)
            {
                case RecvAYMJMatchMsg.MSG_START:
                    new AYMJMatchStartRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DEALCARDS:
                    new AYMJMatchDealCardsRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE:
                    new AYMJMatchSingleReplaceRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_REPLACE_ALL:
                    new AYMJMatchAllReplaceRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE:
                    new AYMJMatchSingDistypeRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISTYPE_ALL:
                    new AYMJMatchAllDisTypeRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DRAW:
                    new AYMJMatchDrawRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_DISCARD:
                    new AYMJMatchDiscardRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PONG:
                    new AYMJMatchPongRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_KONG:
                    new AYMJMatchKongRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_HU:
                    new AYMJMatchHuRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_OVER:
                    new AYMJMatchOverRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_CANCEL:
                    new AYMJMatchCancelRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_UPDATE_STATES:
                    new AYMJMatchUpdateStatesRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_ROOM_REMOVE_PLAYER:
                    new AYMJMatchKickPlayerRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DELAY:
                    new AYMJMatchDelayRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO:
                    new AYMJMatchSingPiaoRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_PIAO_OVER:
                    new AYMJMatchAllPiaoOverRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_BAOPAI:
                    new AYMJMatchBaoPaiRecording(recvOperate).execute();
                    break;
                case RecvAYMJMatchMsg.MSG_BAO_KONG:
                    new AYMJMatchBaoKongRecording(recvOperate).execute();
                    break;
            }
        }
    }
}
