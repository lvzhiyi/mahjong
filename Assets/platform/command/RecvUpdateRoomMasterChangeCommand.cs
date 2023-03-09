using cambrian.common;
using platform.mahjong;
using platform.poker;
using platform.spotred;
using platform.spotred.waitroom;
using scene.game;

namespace platform
{
    /// <summary>
    /// 接收房主变化
    /// </summary>
    public class RecvUpdateRoomMasterChangeCommand : RecvPort
    {
        public RecvUpdateRoomMasterChangeCommand()
        {
            this.id = RecvConst.COMMAND_CLIENT_ROOM_MASTER_CHANGE;
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;

            Room room = Room.room;

            room.masterid = data.readLong();

            room.instanceData();

            room.fulltime = 0;

            var len = data.readInt();

            long userid = 0;

            MatchPlayer[] players = room.players;

            for (int i = 0; i < len; i++)
            {
                userid = data.readLong();
                if (userid != 0)
                {
                    players[i] = new MatchPlayer();
                    players[i].bytesRead(userid, data);
                }
                else
                    players[i] = null;
            }


            var platform = room.getRule().getPlatFormValue(); //0长牌，1麻将，2扑克,3广安
            if (platform == 0)
            {
                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                panel.refresh();
            }
            else if (platform == 1)
            {
                MahJongPanel panel = MahJongPanel.getPanel();
                if (panel != null)
                {
                    panel.refresh();
                    panel.showWaitView();
                }
                
            }
            else if (platform == 2)
            {
                var panel = PKRoomPanel.Panel;
                panel.headView.refresh();
                panel.waitView.refresh();
                panel.fzlview.setVisible(false);
            }
        }
    }
}
