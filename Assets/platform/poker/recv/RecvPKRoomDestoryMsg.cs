using basef.arena;
using basef.lobby;
using cambrian.common;
using platform.spotred;
using platform.spotred.room;
using scene.game;

namespace platform.poker
{
    /// <summary> 斗地主-房间销毁 </summary>
    public class RecvPKRoomDestoryMsg : RecvMsgHandle
    {
        public RecvPKRoomDestoryMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            var cachId = data.readInt();

            var b = data.readBoolean();            //true 表示是投票解散
            var c = data.readBoolean();            //是否是已经开始比赛了。
            long[][] scores = null;
            TotalScore[] totalsores = null;
            long ticket = 0;
            if (c)
            {
                int jushu = data.readInt();        //实际多少局
                int rulejushu = data.readInt();    //规则局数
                long time = data.readLong();       //结算时间
                int playercount = data.readInt();  //玩家人数

                totalsores = new TotalScore[playercount];
                scores = new long[jushu][];

                for (int i = 0; i < playercount; i++)
                {
                    totalsores[i] = new TotalScore();
                    totalsores[i].bytesRead(data);
                }
                ticket = data.readLong();
                for (int i = 0; i < jushu; i++)
                {
                    scores[i] = new long[playercount];
                    for (int j = 0; j < playercount; j++)
                    {
                        scores[i][j] = data.readLong();
                    }
                }
            }
            else
            {
                openLobby();
                return;
            }

            Room.room.scores = scores;
            Room.room.cacheId = cachId;
            Room.room.setRoomResult(totalsores, ticket);

            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ:
                    destoryDDZRoom(b, c);
                    break;
                case PKGAME.PDK:
                    destoryPDKRoom(b, c);
                    break;
                case PKGAME.PDK_10:
                    destoryPDKTenRoom(b, c);
                    break;
                case PKGAME.PDK_ANYUE:
                    destoryPDKAnYueRoom(b, c);
                    break;
                case PKGAME.ZSY:
                    break;
            }

            UnrealRoot.root.getDisplayObject<PKRoomSettingPanel>().setVisible(false);
            UnrealRoot.root.getDisplayObject<ExitPanel>().setVisible(false);

        }

        /// <summary> 销毁跑得快房间 </summary>
        private void destoryPDKAnYueRoom(bool b, bool c)
        {
            if (c || b)
            {
                var panel = (PDKAnYueRoomPanel)PKRoomPanel.Panel;
                panel.gameView.operate.hideOperateView();
                panel.gameView.clock.refresh();

                if (PDKAnYueMatch.match != null && Room.room.isStatus(Room.STATE_MATCH))
                {
                    Room.room.setStatus(Room.STATE_DESTORY, true);
                    var singlePanel = UnrealRoot.root.getDisplayObject<PDKAnYueOverSinglePanel>();
                    singlePanel.refresh();
                    singlePanel.changeLayer();
                }
                else
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else openLobby();
            var dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            dpanel.clear();
            dpanel.setVisible(false);
        }

        /// <summary> 销毁跑得快房间 </summary>
        private void destoryPDKTenRoom(bool b, bool c)
        {
            if (c || b)
            {
                var panel = (PDKTenRoomPanel)PKRoomPanel.Panel;
                panel.gameView.operate.hideOperateView();
                panel.gameView.clock.refresh();

                if (PDKTenMatch.match != null && Room.room.isStatus(Room.STATE_MATCH) &&panel.waitView.getVisible())
                {
                    Room.room.setStatus(Room.STATE_DESTORY, true);
                    var singlePanel = UnrealRoot.root.getDisplayObject<PDKTenOverSinglePanel>();
                    singlePanel.refresh();
                }
                else
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else openLobby();
            var dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            dpanel.clear();
            dpanel.setVisible(false);
        }

        /// <summary> 销毁跑得快房间 </summary>
        private void destoryPDKRoom(bool b, bool c)
        {
            if (c || b)
            {
                var panel = (PPDKRoomPanel)PKRoomPanel.Panel;
                panel.gameView.operate.hideOperateView();
                panel.gameView.clock.refresh();

                if (PDKMatch.match != null && Room.room.isStatus(Room.STATE_MATCH))
                {
                    Room.room.setStatus(Room.STATE_DESTORY, true);
                    var singlePanel = UnrealRoot.root.getDisplayObject<PDKOverSinglePanel>();
                    singlePanel.refresh();
                    singlePanel.changeLayer();
                }
                else
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else openLobby();
            var dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            dpanel.clear();
            dpanel.setVisible(false);
        }

        /// <summary> 销毁斗地主房间 </summary>
        private void destoryDDZRoom(bool b, bool c)
        {
            if (c || b)
            {
                var panel = (PDDZRoomPanel)PKRoomPanel.Panel;
                panel.gameView.operate.hideOperateView();
                panel.gameView.clock.refresh();

                if (DDZMatch.match != null && Room.room.isStatus(Room.STATE_MATCH))
                {
                    Room.room.setStatus(Room.STATE_DESTORY, true);
                    UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>().refresh();
                    //var singlePanel = UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>();
                    //singlePanel.refresh();
                    //singlePanel.changeLayer();
                }
                else
                {
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().show(Room.room);
                    UnrealRoot.root.getDisplayObject<PKAllOverPanel>().setVisible(true);
                }
            }
            else openLobby();
            var dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            dpanel.clear();
            dpanel.setVisible(false);
        }

        /// <summary> 打开大厅 </summary>
        private void openLobby()
        {
             if (Room.room.isType(Room.ARENA) && Room.room.getBind() > 0)
            {
                CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
            }
            else
            {
                ShowLobbyPanel.showLobbyPanel();
                Room.clear();
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
            Room.clear();
        }
    }
}
