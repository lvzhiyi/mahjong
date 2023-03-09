using basef.player;
using cambrian.common;
using platform.bean;

namespace platform.poker
{
    public class PDKAnYueOrder
    {
        public PDKAnYueMatch match { get; private set; }

        public PKBaseOperate operate { get; private set; }

        private int type, step, stage, round, paidui, len;

        private int[] operates;

        public void byteRead(ByteBuffer data)
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
                operate.bytesRead(data);
            }
        }

        private PKBaseOperate bytesOperate(int type)
        {
            var data = new OperateData(type, step, operates, stage, round, paidui);

            switch (data.type)
            {
                case PDKMatchMsg.START:
                    operate = new PDKAnYueMStartGameOperate(data);
                    operate.operateData.isRelay = true;
                    break;//游戏开始
                case PDKMatchMsg.DEALCARDS:
                    operate = new PDKMSystemDealCardOperate(data);
                    break;//发牌
                case PDKMatchMsg.START_MATCH:
                    operate = new PDKMStartMatchOperate(data);
                    break;//出牌前
                case PDKMatchMsg.SHOWCARD:
                    operate = new PDKAnYueMShowCardOperate(data);
                    break;//出牌
                case PDKMatchMsg.CANCEL:
                    operate = new PDKMCancelOperate(data);
                    break;//取消操作
                case PDKMatchMsg.OVER:
                    operate = new PDKAnYueMOverOperate(data);
                    break;//结束
                case PDKMatchMsg.UPDATE_MATCH_SCORE:
                    operate = new PDKAnYueUpdateScoreOperate(data);
                    break;//结束
            }
            return operate;
        }

        public PDKAnYueMatch executeOrder(Room room)
        {
            if (match != null)
            {
                match.setRoomRule(room.roomRule);
            }

            if (PDKAnYueMatch.match != null)
            {
                for (int i = 0; i < room.players.Length; i++)
                {
                    if (Player.player.userid == room.players[i].player.userid)
                    {
                        PDKAnYueMatch.match.setPlayers(room.players, i);
                        break;
                    }
                }
            }
            executeOperates();
            return PDKAnYueMatch.match;
        }

        private void executeOperates()
        {
            switch (operate.operateData.type)
            {
                case PDKMatchMsg.START:
                    new PDKAnYueMStartGameRecording(operate).execute();
                    break;//开始游戏
                case PDKMatchMsg.DEALCARDS:
                    new PDKAnYueMSystemDealCardRecording(operate).execute();
                    break;//发牌   
                case PDKMatchMsg.START_MATCH:
                    new PDKAnYueMStartMatchRecording(operate).execute();
                    break;//出牌前
                case PDKMatchMsg.SHOWCARD:
                    new PDKAnYueMShowCardRecording(operate).execute();
                    break;//出牌    
                case PDKMatchMsg.CANCEL:
                    new PDKAnYueMCancelRecording(operate).execute();
                    break;//取消
                case PDKMatchMsg.OVER:
                    new PDKAnYueMOverRecording(operate).execute();
                    break;//结束   
                case PDKMatchMsg.UPDATE_MATCH_SCORE:
                    new PDKAnYueUpdateScoreRecording(operate).execute();
                    break;//结束   
            }
        }
    }
}
