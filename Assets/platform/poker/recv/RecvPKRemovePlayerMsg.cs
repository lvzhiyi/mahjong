using basef.arena;
using basef.lobby;
using basef.player;
using cambrian.common;

namespace platform.poker
{
    /// <summary> 斗地主-房间-玩家离开 </summary>
    public class RecvPKRemovePlayerMsg : RecvMsgHandle
    {
        public RecvPKRemovePlayerMsg()
        {
            this.gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            bool isKick = data.readBoolean();

            Room room = Room.room;

            if (room == null) return;

            long userid = room.getPlayers()[index].userid;
            PKRoomPanel panel = PKRoomPanel.Panel;
            panel.fzlview.setVisible(false);
            room.fulltime = 0;

            if (room.isStatus(Room.STATUS_INIT) && isKick)
            {
                long selfUserid = room.getSelfMatchPlayer().player.userid;
                if (userid == selfUserid)
                {
                    Alert.fixedContinueShow("未准备，被移出房间");
                }
            }

            if (Player.player.userid == userid)
            {
                if (Room.room.isType(Room.ARENA) && Room.room.getBind() > 0)
                {
                    CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
                }
                else
                {
                    ShowLobbyPanel.showLobbyPanel();
                }
            }
            else
            {
                Room.room.removePlayer(userid);
                panel.headView.refresh();
                panel.waitView.room_card.refresh();
            }
        }

        public void arenaBack(object obj)
        {
            if (obj == null)
            {
                ShowLobbyPanel.showLobbyPanel();
                return;
            }
            UnrealRoot.root.getDisplayObject<ArenaPanel>().refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
        }
    }
}
