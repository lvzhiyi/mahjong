using basef.arena;
using basef.lobby;
using basef.player;
using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家离开房间
    /// </summary>
    public class RecvMJRemovePlayerMsg : RecvMsgHandle
    {
        public RecvMJRemovePlayerMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            bool isKick = data.readBoolean();

            Room room = Room.room;
            if (room == null) return;
            room.fulltime = 0;
            MahJongPanel panel = MahJongPanel.getPanel();
            panel.fzlView.setVisible(false);
            long userid = Room.room.getPlayers()[index].userid;

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
                panel.refresh();
                panel.showWaitView();
            }
        }

        public void arenaBack(object obj)
        {
            if (obj == null)
            {
                ShowLobbyPanel.showLobbyPanel();
                return;
            }

            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
        }
    }
}
