using basef.player;
using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class MJOrder
    {
        public MJBaseOperate recvOperate = null;
        /// <summary>
        /// 场景
        /// </summary>
        public CPMatch scene;

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
                case RecvMJMatchMsg.MSG_START:
                    recvOperate = new MJMatchStartOperate(type, index,true);
                    break;
                case RecvMJMatchMsg.MSG_DEALCARDS:
                    recvOperate = new MJMatchDealCardsOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_REPLACE:
                    recvOperate = new MJMatchSingleReplaceOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_REPLACE_ALL:
                    recvOperate = new MJMatchAllReplaceOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE:
                    recvOperate = new MJMatchSingleDistypeOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_PIAO:
                    recvOperate=new MJMatchSinglePiaoOperate(type,index);
                    break;
                case RecvMJMatchMsg.MSG_PIAO_OVER:
                    recvOperate=new MJMatchAllPiaoOverOperate(type,index);
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE_ALL:
                    recvOperate = new MJMatchAllDisTypeOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_DRAW:
                    recvOperate = new MJMatchDrawOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_DISCARD:
                    recvOperate = new MJMatchDiscardOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_PONG:
                    recvOperate = new MJMatchPongOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_KONG:
                    recvOperate = new MJMatchKongOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_HU:
                    recvOperate = new MJMatchHuOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_OVER:
                    recvOperate = new MJMatchOverOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_CANCEL:
                    recvOperate = new MJMatchCancelOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_UPDATE_STATES:
                    recvOperate = new MJMatchUpdateStateslOperate(type, index);
                    break;
                case RecvMJMatchMsg.MSG_LIE_CARD:
                    recvOperate=new MJMatchTangOperate(type,index);
                    break;
            }
        }
        
        public MJMatch executeOrder(Room room)
        {
            ReplayMahjongRoomPanel mpanel = setData(room);
            executeOperates();
            return MJMatch.match;
        }

        public ReplayMahjongRoomPanel setData(Room room)
        {
            if (scene != null)
                scene.setRoomRule(room.roomRule);
            ReplayMahjongRoomPanel mpanel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();

            if (MJMatch.match != null)
            {
                int mine = 0;
                for (int i = 0; i < room.players.Length; i++)
                {
                    if (Player.player.userid == room.players[i].player.userid)
                    {
                        mine = i;
                    }
                }
                MJMatch.match.setPlayers(room.players, mine);
            }

            return mpanel;
        }

        public void executeOperates()
        {
            int type=recvOperate.type;

            switch (type)
            {
                case RecvMJMatchMsg.MSG_START:
                   new MJMatchStartRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DEALCARDS:
                     new MJMatchDealCardsRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_REPLACE:
                     new MJMatchSingleReplaceRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_REPLACE_ALL:
                    new MJMatchAllReplaceRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE:
                    new MJMatchSingDistypeRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PIAO:
                    new MJMatchSinglePiaoRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PIAO_OVER:
                    new MJMatchPiaoOverRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISTYPE_ALL:
                    new MJMatchAllDisTypeRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DRAW:
                     new MJMatchDrawRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_DISCARD:
                    new MJMatchDiscardRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_PONG:
                    new MJMatchPongRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_KONG:
                    new MJMatchKongRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_HU:
                    new MJMatchHuRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_OVER:
                    new MJMatchOverRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_CANCEL:
                    new MJMatchCancelRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_UPDATE_STATES:
                    new MJMatchUpdateStatesRecording(recvOperate).execute();
                    break;
                case RecvMJMatchMsg.MSG_LIE_CARD:
                    new MJMatchTangRecording(recvOperate).execute();
                    break;
            }
        }
    }
}
