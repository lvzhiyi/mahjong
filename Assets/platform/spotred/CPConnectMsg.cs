using cambrian.common;
using platform.spotred.room;

namespace platform.spotred
{
    /// <summary>
    /// 长牌重连房间
    /// </summary>
    public class CPConnectMsg : RecvMsgHandle
    {

        public CPConnectMsg()
        {
            gamePlatform = GamePlatform.CP_GAME;
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
            Room room=Room.room;
            if (room.isStatus(Room.STATE_MATCH))
            {
                CPMatch.match=new CPMatch(room.roomRule.rule.playerCount);
                CPMatch.match.setPlayers(room.players, room.indexOf());
                CPMatch.match.setRoomRule(room.roomRule);
                CPMatch.match.bytesRead(data);
            }
            else
            {
                UnrealRoot.root.getDisplayObject<SpotRoomPanel>().setVisible(false);
            }

            if (b)
            {
                dpanel.setVisible(true);
            }
        }
    }
}
