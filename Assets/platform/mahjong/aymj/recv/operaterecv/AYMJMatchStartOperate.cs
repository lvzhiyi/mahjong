using cambrian.common;

namespace platform.mahjong
{
    public class AYMJMatchStartOperate : MJBaseOperate
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

        public AYMJMatchStartOperate(int type,int selfIndex,bool isreplay=false) : base(type,selfIndex,isreplay)
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

        ReplayAYMJRoomPanel panel;
        public void execute1()
        {
            Room room = Room.room;
            room.roomRule.setGameNum(tray);
            room.cancelReady();

            AYMJMatch.match = new AYMJMatch(room.getPlayerCount(), step, banker, room.getMasterIndex(), paidui);

            AYMJMatch.match.setPlayers(room.players, room.indexOf());
            AYMJMatch.match.setRoomRule(room.roomRule);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(getSelfOperate());
            panel.refresh();
            panel.showGameView();
            panel.refreshCardNum();
            panel.gameView.getMjIndexCenterView().setJuShu(room.roomRule.getNowsPlayNum(), room.roomRule.getMatchCount());
            panel.showCountTime(round);
            panel.setVisible(true);

            UnrealRoot.root.showPanel<ReplayAYMJRoomPanel>();

            //showOperate();
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
                panel.gameView.getDQView().showDingQue(AYMJMatch.match.getRecommendType());
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
