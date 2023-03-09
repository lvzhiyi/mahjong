using cambrian.common;
using platform.spotred.waitroom;

namespace platform.spotred.room
{
    /// <summary>
    /// 比赛开始
    /// </summary>
    public class MatchStartOperate: BaseOperate
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

        public MatchStartOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui, bool isRelay = false) : base(type, step, operates, stage, round, paidui, isRelay)
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
            if (!isRelay)
                init();
        }

        public void init()
        {
            Room room= Room.room;
            room.roomRule.setGameNum(tray);
            room.cancelReady();

            CPMatch.match = new CPMatch(room.getPlayerCount(),step,banker,dangjia,xiaojia,paidui);

            CPMatch.match.setPlayers(room.players,selfindex);
            CPMatch.match.setRoomRule(room.roomRule);

            SpotRoomPanel panel= UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            clearBaseOperate(panel);
            panel.refresh();
            panel.refreshCardNum();
            panel.ShowMatchPlayerInfo();

            panel.headView.hideAllPiao();

            panel.setOperate(operate);
            panel.operateView.showButton(operate,paidui);

            if (room.roomType != Room.JBC)
                UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>().vview.soundrecordOver();

            UnrealRoot.root.showPanel<SpotRoomPanel>();
        }

        public void clearBaseOperate(SpotRoomPanel panel)
        {
            panel.clearBaseOperate();
        }
    }
}
