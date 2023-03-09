using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 单局结束
    /// </summary>
    public class MJMatchOverProcess:Process
    {
        MJMatchOverOperate operate;

        int selfOperate;

        public MJMatchOverProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchOverOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            Room.tempMatchs.add(operate.cloneMatch);

            MahjongRoomPanel panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();

            panel.gameView.showDisCardRedmine(false);

            match.setStage(operate.stage);
            Room cloneRoom = Room.room.clone();

            operate.cloneMatch.banker = match.banker;
            operate.cloneMatch.mindex = match.mindex;

            Room.room.addSpotRedCount(operate.cloneMatch.getSpotRedCounts());
            MJOverPanel overpanel = UnrealRoot.root.getDisplayObject<MJOverPanel>();
            overpanel.refrshData(cloneRoom, operate.cloneMatch);
            overpanel.setVisible(true);
            
            operate.playOver();

            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
        }
    }
}
