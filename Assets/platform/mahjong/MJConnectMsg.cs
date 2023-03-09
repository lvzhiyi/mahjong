using cambrian.common;
using platform.spotred.room;

namespace platform.mahjong
{
    public class MJConnectMsg : RecvMsgHandle
    {
        public MJConnectMsg()
        {
            gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            DisbandPanel dpanel = null;
            //解散房间时间
            bool b = data.readBoolean();
            if (b)
            {
                Voting voting = new Voting();
                voting.bytesRead(data);
                dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
                dpanel.init();
                dpanel.recvInfo(voting);
                dpanel.show();
                dpanel.refresh();
                dpanel.setVisible(true);
            }
            Room room = Room.room;
            //前面牌局的结算信息
            Room.tempMatchs.clear();
            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);
            int count = data.readInt();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int playerCount = data.readInt();
                    cloneMatch(playerCount,gametype,data);
                }
            }

            if (room.isStatus(Room.STATE_MATCH))
            {
                matchConnect(room,gametype,data);
            }
            if (b)
            {
                dpanel.setVisible(true);
            }
        }

        private void cloneMatch(int playerCount,int gametype,ByteBuffer data)
        {
            MJMatch cloneMatch = new MJMatch(playerCount);
           
            if (gametype == GamePanelData.AY_GAME)
            {
                AYMJMatch ayMatch = new AYMJMatch(playerCount);
                ayMatch.bytesReadAll(data);
                Room.tempMatchs.add(ayMatch);
                return;
            }

            cloneMatch.bytesReadAll(data);
            Room.tempMatchs.add(cloneMatch);
        }

        private void matchConnect(Room room,int gametype,ByteBuffer data)
        {
            MJMatch.match = new MJMatch(room.roomRule.rule.playerCount);
          
            if (gametype == GamePanelData.AY_GAME)
            {
                AYMJMatch.match = new AYMJMatch(room.roomRule.rule.playerCount);
                AYMJMatch.match.setPlayers(room.players, room.indexOf());
                AYMJMatch.match.setRoomRule(room.roomRule);
                AYMJMatch.match.bytesRead(data);
                return;
            }
            MJMatch.match.setPlayers(room.players, room.indexOf());
            MJMatch.match.setRoomRule(room.roomRule);
            MJMatch.match.bytesRead(data);
        }
    }
}
