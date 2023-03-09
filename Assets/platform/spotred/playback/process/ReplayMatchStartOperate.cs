using cambrian.common;
using platform.spotred.room;

namespace platform.spotred.playback
{
    public class ReplayMatchStartOperate:BaseOperate
    {
        /// <summary>
        /// 局数
        /// </summary>
        public int tray;
        /// <summary>
        /// 庄家
        /// </summary>
        public int banker;
        /// <summary>
        /// 档家
        /// </summary>
        public int dangjia;
        /// <summary>
        /// 小家
        /// </summary>
        public int xiaojia;

        public int selfindex;
        /// <summary>
        /// 操作
        /// </summary>
        public int operate;

        public ReplayMatchStartOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
        {
            this.selfindex = selfIndex;
            this.operate = operates[selfIndex];
        }

        public override void bytesRead(ByteBuffer data)
        {
            tray = data.readInt();
            banker = data.readInt();
            dangjia = data.readInt();
            xiaojia = data.readInt();
            SpotRoomPanel.isStartMatch = true;
            
                init();
        }

        public void init()
        {
            Room room = Room.room;
            CPMatch.match = new CPMatch(room.roomRule.rule.playerCount);
            CPMatch.match.banker = banker;
            CPMatch.match.dangjia = dangjia;
            CPMatch.match.xiaojia = xiaojia;
            CPMatch.match.setStage(stage);
            CPMatch.match.paidui = paidui;
            room.roomRule.setGameNum(tray);
            CPMatch.match.setPlayers(room.players, room.indexOf());
            CPMatch.match.setRoomRule(room.roomRule);
            ReplaySpotRoomPanel replayRoomPanel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            replayRoomPanel.refresh();
            replayRoomPanel.ShowMatchPlayerInfo();
            replayRoomPanel.refreshClock(round);
            replayRoomPanel.headView.hideAllPiao();
            UnrealRoot.root.showPanel<ReplaySpotRoomPanel>();
        }
    }
}
