using basef.arena;
using basef.player;
using cambrian.common;
using cambrian.game;
using Libs;
using platform.mahjong;
using platform.poker;
using scene.game;
using System;

namespace platform.spotred.playback
{
    public class RePlayProcess: MouseClickProcess
    {
        ReplayBar bar;
        public override void execute()
        {
            if (Room.room != null&&!Room.room.isStatus(Room.STATE_DESTORY))
            {
                Alert.show("你当前处于房间中,不能操作!");
                return;
            }

            this.bar = this.transform.parent.GetComponent<ReplayBar>();
            SpotRedRoot.dataLoading.refresh();
            SoundManager.playButton();
            CommandManager.addCommand(new PlayBackCommand("?id=" + this.bar.id + "&type=2"), this.onDataInit);
        }


        public ByteBuffer getUnCompress(ByteBuffer source,byte[] bytes)
        {
            ByteBuffer data = new ByteBuffer();
            bool b = source.readBoolean();
            data.writeBoolean(b);
            if (b)
            {
                source.readLong();
                source.readInt();
                source.readInt();
                
                RoomRule rule = new RoomRule();
                rule.bytesRead(source);

                int len = rule.rule.playerCount;
                SimplePlayer[] players = new SimplePlayer[len];
                for (int i = 0; i < len; i++)
                {
                    players[i] = new SimplePlayer();
                    players[i].bytesRead(source);
                }

                int start = source.getOffset();

                byte[] unDecompress= GZipKit.unDecompress(source.toArray()).toArray();
                byte[] dest = new byte[start+unDecompress.Length];
                Array.Copy(bytes,dest,start);
                Array.Copy(unDecompress,0,dest,start,unDecompress.Length);
                data.clear();
                data.write(dest);
            }

            return data;
        }

        private void onDataInit(object obj)
        {
            ByteBuffer data = (ByteBuffer)obj;
            bool b = data.readBoolean();
            if (b)
            {
                data.readLong();
                int roomid = data.readInt();
                int time = data.readInt();
                RoomRule rule = new RoomRule();
                rule.bytesRead(data);

                int plat = rule.rule.getPlatFormValue();
                if (plat == GamePlatform.CP_GAME)
                {
                    cpReplay(roomid, time, rule, data);
                }
                else if (plat == GamePlatform.MJ_GAME)
                {
                    showMjReplay(roomid,time,rule,data);
                }
                else if (plat == GamePlatform.PK_GAME)
                {
                    PKGAME.ReplayRoom(roomid, time, rule, data);
                }
            }
            SpotRedRoot.dataLoading.hidden();
            
        }

        public void showMjReplay(int roomid,int time, RoomRule rule,ByteBuffer data)
        {
            int gametype = GamePanelData.getInstance().getGamePanel(rule.rule.sid);

            switch (gametype)
            {
               
                case GamePanelData.MY_GAME:
                    mjReplay(roomid, time, rule, data);
                    break;
              
                case GamePanelData.AY_GAME:
                    AyReplay(roomid, time, rule, data);
                    break;
            }
        }

        private void AyReplay(int roomid, int time, RoomRule rule, ByteBuffer data)
        {
            AYMJReplay replay = new AYMJReplay();
            replay.setData(roomid, time, rule);
            ReplayAYMJRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            UnrealLuaPanel p = UnrealRoot.root.checkDisplayObject<ArenaRecordPanel>();
            panel.setLastPanel(p);
            panel.setRePlay(replay);
            replay.bytesRead(data);
            replay.start();
        }

        private void cpReplay(int roomid,int time,RoomRule rule,ByteBuffer data)
        {
            Replay replay = new Replay();

            replay.setData(roomid, time, rule);
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            UnrealLuaPanel p = UnrealRoot.root.checkDisplayObject<ArenaRecordPanel>();
            panel.setLastPanel(p);
            panel.setRePlay(replay);
            replay.bytesRead(data);
            replay.start();
        }

        private void mjReplay(int roomid, int time, RoomRule rule, ByteBuffer data)
        {
            MJReplay replay = new MJReplay();

            replay.setData(roomid, time, rule);
            ReplayMahjongRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            UnrealLuaPanel p = UnrealRoot.root.checkDisplayObject<ArenaRecordPanel>();
            panel.setLastPanel(p);
            panel.setRePlay(replay);
            replay.bytesRead(data);
            replay.start();
        }
    }
}
