using basef.arena;
using basef.lobby;
using basef.setting;
using cambrian.common;
using platform.spotred;
using platform.spotred.room;
using scene.game;

namespace platform.mahjong
{
    /// <summary>
    /// 接收绵阳麻将房间摧毁消息
    /// </summary>
    public class RecvAYMJDestoryMsg : Process
    {
        ByteBuffer data;

        public RecvAYMJDestoryMsg(ByteBuffer data)
        {
            this.data = data;
        }

        /// <summary>
        /// /true 表示是投票解散
        /// </summary>
        bool b;
        /// <summary>
        /// 是否是已经开始比赛了。
        /// </summary>
        bool c;

        MahJongPanel panel;


        public override void execute()
        {
            panel = MahJongPanel.getPanel();


            int cachId = data.readInt();

            b = data.readBoolean();

            c = data.readBoolean();

            long[][] scores = new long[0][];

            if (c)
            {
                matchStart(scores);
            }

            Room.room.scores = scores;
            Room.room.cacheId = cachId;

            if (b) //投票解散
            {
                voteDisbandRoom();
            }
            else
            {
                normalDisband();
            }


            SettingPanel spanel = UnrealRoot.root.getDisplayObject<SettingPanel>();
            spanel.gameObject.SetActive(false);

            ExitPanel apanel = UnrealRoot.root.getDisplayObject<ExitPanel>();
            apanel.setVisible(false);

            DisbandPanel dpanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            dpanel.clear();
            dpanel.setVisible(false);

        }

        private void matchStart(long[][] scores)
        {
            int jushu = data.readInt(); //实际多少局
            int rulejushu = data.readInt();

            long time = data.readLong(); //结算时间
            int playercount = data.readInt(); //玩家人数



            TotalScore[] score = new TotalScore[playercount];
            for (int i = 0; i < playercount; i++)
            {
                score[i] = new TotalScore();
                score[i].bytesRead(data);
            }

            long ticket = data.readLong();
            Room.room.setRoomResult(score, ticket);


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

            MahjongCount[] mjcount = new MahjongCount[playercount];
            for (int i = 0; i < mjcount.Length; i++)
            {
                mjcount[i] = new MahjongCount();
                mjcount[i].bytesRead(data);
            }

            MJAllOverPanel.counts = mjcount;
        }

        private void normalDisband()
        {

            if (!Room.room.roomRule.isOver())
                panel.setVisible(false);
        }

        /// <summary>
        /// 投票解散房间
        /// </summary>
        private void voteDisbandRoom()
        {
            if (Room.room.roomRule.getGameNum() == -1 && Room.room.isStatus(Room.STATUS_INIT))
            {
                panel.setVisible(false);
                if (Room.room.isType(Room.ARENA) && Room.room.getBind() > 0 && !c)
                {
                    CommandManager.addCommand(new GetArenaInfoCommand(Room.room.getBind()), arenaBack);
                }
                else
                {
                    showLobby();
                }
            }
            else if (Room.room.roomRule.getGameNum() > -1 || panel.getVisible())
            {
                Room.room.roomRule.setGameNum(Room.room.roomRule.rule.matchCount - 1);
                MJOverPanel opanel = UnrealRoot.root.getDisplayObject<MJOverPanel>();

                if (panel.getWaitView().getVisible() || opanel.getVisible())
                    new AYMJShowAllOverProcess().execute();
                else
                    opanel.button.text.text = "结算";
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

        public void arenaBack(object obj)
        {
            if (obj == null)
            {
                showLobby();
                return;
            }
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();
        }

        private void showLobby()
        {
            ShowLobbyPanel.showLobbyPanel();
        }
    }
}

