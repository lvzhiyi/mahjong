using cambrian.common;

namespace platform.mahjong
{
    public class MJMatchStartOperate:MJBaseOperate
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
        /// 操作
        /// </summary>
        public int operate;

        public MJMatchStartOperate(int type,int selfIndex,bool isreplay=false) : base(type,selfIndex,isreplay)
        {

        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            tray = data.readInt();
            banker = data.readInt();

            if (isRelay)
            {
                execute1();
            }
        }

        ReplayMahjongRoomPanel panel;
        public void execute1()
        {
            Room room = Room.room;
            room.roomRule.setGameNum(tray);
            room.cancelReady();

            MJMatch.match = new MJMatch(room.getPlayerCount(), step, banker, room.getMasterIndex(), paidui);

            MJMatch.match.setPlayers(room.players, room.indexOf());
            MJMatch.match.setRoomRule(room.roomRule);

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.setOperate(getSelfOperate());
            panel.refresh();
            panel.showGameView();
            panel.refreshCardNum();
            panel.gameView.getMjIndexCenterView().setJuShu(room.roomRule.getNowsPlayNum(), room.roomRule.getMatchCount());
            panel.showCountTime(round);

            UnrealRoot.root.showPanel<ReplayMahjongRoomPanel>();

            showOperate();
        }

        private void showOperate()
        {
            if (MJOperate.isCanReplace(getSelfOperate()))
            {
                panel.refreshHuanSZ(0, true);
            }
            else if (MJOperate.isDisType(getSelfOperate()))
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshDingQue(0, true);
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else
            {
                otherOperate();
            }
        }

        public void otherOperate()
        {
        }

    }
}
