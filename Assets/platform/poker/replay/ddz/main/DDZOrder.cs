using System.Text;
using basef.player;
using cambrian.common;
using platform.bean;
using UnityEngine;

namespace platform.poker
{
    public class DDZOrder
    {
        public DDZMatch match { get; private set; }

        public PKBaseOperate operate { get; private set; }

        private int type, step, stage, round, paidui, len;

        private int[] operates;

        public void byteRead(ByteBuffer data, int selfIndex)
        {
            type = data.readInt();
            step = data.readInt();
            stage = data.readInt();
            round = data.readInt();
            paidui = data.readInt();
            len = data.readInt();
            operates = new int[len];
            for (int i = 0; i < len; i++)
                operates[i] = data.readInt();
            operate = bytesOperate(type);
            if (operate != null)
            {
                operate.selfIndex = selfIndex;
                operate.bytesRead(data);
            }              
        }
       
        private PKBaseOperate bytesOperate(int type)
        {
            var data = new OperateData(type, step, operates, stage, round, paidui);

            switch (data.type)
            {   
                case DDZMatchMsg.START:
                    operate = new DDZMStartGameOperate(data);
                    operate.operateData.isRelay = true;
                    break;//游戏开始
                case DDZMatchMsg.DEALCARDS:
                    operate = new DDZMSystemDealCardOperate(data);
                    break;//发牌
                case DDZMatchMsg.FLOW:
                    operate = new DDZMFlowGameOperate(data);
                    break;//流局
                case DDZMatchMsg.DEALLADNLORDCARD:
                    operate = new DDZMDealLandlordCardOperate(data);
                    break;//发地主牌
                case DDZMatchMsg.SHOWCARD:
                    operate = new DDZMShowCardOperate(data);
                    break;//出牌
                case DDZMatchMsg.CANCEL:
                    operate = new DDZMCancelOperate(data);
                    break;//取消操作
                case DDZMatchMsg.OVER:
                    operate = new DDZMOverOperate(data);
                    break;//结束
                case DDZMatchMsg.LANDLORDCALL:
                    operate = new DDZMLandlordCallOperate(data);
                    break;//叫地主
                case DDZMatchMsg.LANDLORDGRAB:
                    operate = new DDZMLandlordGrabOperate(data);
                    break;//抢地主
                case DDZMatchMsg.JIABEI:
                    operate = new DDZMJiaBeiOperate(data);
                    break;//加倍
                case DDZMatchMsg.MINGPAI:
                    operate = new DDZMMingPaiOperate(data);
                    break;//明牌
                case DDZMatchMsg.MINGPAI_CANCEL:
                    operate = new DDZMMingPaiCancelOperate(data);
                    break;//明牌取消
                case DDZMatchMsg.LANDLORD_CALLSCORE:
                    operate = new DDZMCallScoreOperate(data);
                    break;//叫分
            }
            return operate;
        }

        public DDZMatch executeOrder(Room room)
        {
            if (match != null)
            {
                match.setRoomRule(room.roomRule);
            }

            if (DDZMatch.match != null)
            {
                for (int i = 0; i < room.players.Length; i++)
                {
                    if (Player.player.userid == room.players[i].player.userid)
                    {
                        DDZMatch.match.setPlayers(room.players, i);
                        break;
                    }
                }
            }
            executeOperates();
            return DDZMatch.match;
        }

        private void executeOperates()
        {
            switch (operate.operateData.type)
            {
                case DDZMatchMsg.START:
                    new DDZMStartGameRecording(operate).execute();
                    break;//开始游戏
                case DDZMatchMsg.DEALCARDS:
                    new DDZMSystemDealCardRecording(operate).execute();
                    break;//发牌
                case DDZMatchMsg.LANDLORDCALL:
                    new DDZMLandlordCallRecording(operate).execute();
                    break;//叫地主
                case DDZMatchMsg.LANDLORDGRAB:
                    new DDZMLandlordGrabRecording(operate).execute();
                    break;//抢地主
                case DDZMatchMsg.JIABEI:
                    new DDZMJiaBeiRecording(operate).execute();
                    break;//加倍
                case DDZMatchMsg.SHOWCARD:
                    new DDZMShowCardRecording(operate).execute();
                    break;//出牌                                                        
                case DDZMatchMsg.MINGPAI:
                    new DDZMMingPaiRecording(operate).execute();
                    break;//明牌
                case DDZMatchMsg.CANCEL:
                    new DDZMCancelRecording(operate).execute();
                    break;//取消
                case DDZMatchMsg.OVER:
                    new DDZMOverRecording(operate).execute();
                    break;//结束
                case DDZMatchMsg.DEALLADNLORDCARD:
                    new DDZMDealLandlordCardRecording(operate).execute();
                    break;//发地主牌
                case DDZMatchMsg.FLOW:
                    new DDZMFlowGameRecording(operate).execute();
                    break;//流局
                case DDZMatchMsg.MINGPAI_CANCEL:
                    new DDZMMingPaiCancelRecording(operate).execute();
                    break;//取消明牌阶段
                case DDZMatchMsg.LANDLORD_CALLSCORE:
                    new DDZMCallScoreRecording(operate).execute();
                    break;//叫分
            }
        }
    }
}
