using cambrian.common;
using platform.spotred.room;

namespace platform.poker
{
    public class PKConnectMsg : RecvMsgHandle
    {
        public PKConnectMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            bool b = data.readBoolean();
            if (b)
            {
                var voting = new Voting();
                voting.bytesRead(data);
                var dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
                dpanel.init();
                dpanel.recvInfo(voting);
                dpanel.show();
                dpanel.refresh();          
            }
            PKGAME.RULESID = Room.room.roomRule.rule.sid;
            
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ:
                    ddzConnect(data);
                    break;
                case PKGAME.PDK_10:
                    pdkTenConnect(data);
                    break;
                case PKGAME.PDK_ANYUE:
                    pdkAnYueConnect(data);
                    break;
                case PKGAME.PDK:
                    pdkConnect(data);
                    break;
            }
            UnrealRoot.root.getDisplayObject<DisbandPanel>().setCVisible(b);
        }

        /// <summary> 跑得快10张 重连 </summary>
        private void pdkAnYueConnect(ByteBuffer data)
        {
            var panel = UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();
            var room = Room.room;
            if (room.isStatus(Room.STATE_MATCH))
            {
                PDKAnYueMatch.match = new PDKAnYueMatch();
                PDKAnYueMatch.match.setPlayers(room.players, room.indexOf());
                PDKAnYueMatch.match.setRoomRule(room.roomRule);
                PDKAnYueMatch.match.bytesRead(data);
            }
            else if (room.isStatus(Room.STATUS_INIT))
            {
                panel.refreshWaitView();
            }
            panel.setVisible(true);
        }

        /// <summary> 跑得快10张 重连 </summary>
        private void pdkTenConnect(ByteBuffer data)
        {
            var panel = UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
            var room = Room.room;
            if (room.isStatus(Room.STATE_MATCH))
            {
                PDKTenMatch.match = new PDKTenMatch();
                PDKTenMatch.match.setPlayers(room.players, room.indexOf());
                PDKTenMatch.match.setRoomRule(room.roomRule);
                PDKTenMatch.match.bytesRead(data);
            }
            else if (room.isStatus(Room.STATUS_INIT))
            {
                panel.refreshWaitView();
            }
            panel.setVisible(true);
        }

        /// <summary> 跑得快 重连 </summary>
        private void pdkConnect(ByteBuffer data)
        {
            var panel = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
            var room = Room.room;
            if (room.isStatus(Room.STATE_MATCH))
            {
                PDKMatch.match = new PDKMatch();
                PDKMatch.match.setPlayers(room.players, room.indexOf());
                PDKMatch.match.setRoomRule(room.roomRule);
                PDKMatch.match.bytesRead(data);
            }
            else if (room.isStatus(Room.STATUS_INIT))
            {
                panel.refreshWaitView();
            }
            panel.setVisible(true);
        }

        /// <summary> 斗地主 重连 </summary>
        private void ddzConnect(ByteBuffer data)
        {
            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            var room = Room.room;
            if (room.isStatus(Room.STATE_MATCH))
            {
                DDZMatch.match = new DDZMatch();
                DDZMatch.match.setPlayers(room.players, room.indexOf());
                DDZMatch.match.setRoomRule(room.roomRule);
                DDZMatch.match.bytesRead(data);
            }
            else if (room.isStatus(Room.STATUS_INIT))
            {
                panel.refreshWaitView();
            }
            panel.setVisible(true);
        }
    }
}
