using basef.arena;
using basef.lobby;
using basef.player;
using cambrian.common;
using platform.spotred.room;
using platform.spotred.waitroom;

namespace platform.spotred
{
    /// <summary>
    /// 川牌-房间-客户端接收-玩家离开房间
    /// </summary>
    public class RecvCPRemovePlayerMsg : RecvMsgHandle
    {
        public RecvCPRemovePlayerMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            bool isKick = data.readBoolean();

            if (Room.room == null) return;
            Room room = Room.room;
            room.fulltime = 0;
            long userid = Room.room.getPlayers()[index].userid;


            if (isKick && !room.isStatus(Room.STATUS_INIT))
            {
                if (UnrealRoot.root.checkDisplayObject<SpotRoomPanel>() != null)
                    UnrealRoot.root.getDisplayObject<SpotRoomPanel>()
                        .addRecvOperate(new RemoveClubGoldPlayerOperate(userid));
            }
            else
            {
                long selfUserid = room.getSelfMatchPlayer().player.userid;
                if (userid == selfUserid && isKick)
                {
                    Alert.fixedContinueShow("未准备，被移出房间");

                }

                SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
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
                }
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
