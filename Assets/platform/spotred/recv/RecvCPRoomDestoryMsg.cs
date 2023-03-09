using basef.arena;
using basef.lobby;
using basef.setting;
using cambrian.common;
using platform.spotred.room;
using platform.spotred.waitroom;
using scene.game;

namespace platform.spotred
{
    /// <summary>
    /// 前台-后台发送-接收销毁房间的消息
    /// </summary>
    public class RecvCPRoomDestoryMsg: RecvMsgHandle
    {
        public RecvCPRoomDestoryMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            
                int cachId = data.readInt();
                bool b = data.readBoolean(); //true 表示是投票解散

                bool c = data.readBoolean(); //是否是已经开始比赛了。

                long[][] scores = new long[0][];

                if (c)
                {
                    int jushu = data.readInt(); //实际多少局
                    int rulejushu = data.readInt();

                    long time = data.readLong(); //结算时间
                    int playercount = data.readInt(); //玩家人数

                    TotalScore[] spotredcount = new TotalScore[playercount];
                    for (int i = 0; i < playercount; i++)
                    {
                        spotredcount[i] = new TotalScore();
                        spotredcount[i].bytesRead(data);
                    }

                    long ticket = data.readLong();
                    Room.room.setRoomResult(spotredcount, ticket);


                    // int len = data.readInt(); //多少局
                    scores = new long[jushu][];

                    for (int i = 0; i < jushu; i++)
                    {
                        //int count = data.readInt();
                        scores[i] = new long[playercount];
                        for (int j = 0; j < playercount; j++)
                        {
                            scores[i][j] = data.readLong();
                        }
                    }
                }

                SpotRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
                Room.room.scores = scores;
                Room.room.cacheId = cachId;
                if (b)
                {
                    if (Room.room.roomRule.getGameNum() > -1 || panel.getVisible())
                    {
                        Room.room.roomRule.setGameNum(Room.room.roomRule.rule.matchCount - 1);
                        OverPanel opanel = UnrealRoot.root.getDisplayObject<OverPanel>();
                        if (panel.getVisible())
                        {
                            opanel.button.text.text = "结算";
                        }
                        else
                            opanel.button.GetComponent<OperateOverProcess>().execute1();
                    }
                    else
                    {
                        if (Room.room.isType(Room.ARENA) && Room.room.getBind() > 0 && !c)
                        {
                            CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
                        }
                        else
                        {
                            Alert.autoShow("房间已经解散！");
                        }
                    }
                }
                else
                {
                    if (Room.room.isType(Room.ARENA) && Room.room.getBind() > 0 && !c)
                    {
                        CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
                    }
                }

                SettingPanel spanel = UnrealRoot.root.getDisplayObject<SettingPanel>();
                spanel.setVisible(false);

                ExitPanel apanel = UnrealRoot.root.getDisplayObject<ExitPanel>();
                apanel.setVisible(false);

                DisbandPanel dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
                dpanel.clear();
                dpanel.setVisible(false);

                SpotWaitRoomPanel waitpanel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                waitpanel.setVisible(false);

                if (Room.room.getBind() <= 0)
                {
                    ShowLobbyPanel.showLobbyPanel(false);
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
