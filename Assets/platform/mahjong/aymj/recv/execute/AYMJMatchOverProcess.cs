using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 单局结束
    /// </summary>
    public class AYMJMatchOverProcess:Process
    {
        AYMJMatchOverOperate operate;

        int selfOperate;

        public AYMJMatchOverProcess(MJBaseOperate operate)
        {
            this.operate = (AYMJMatchOverOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            Room.tempMatchs.add(operate.cloneMatch);

            AYMJRoomPanel panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();

            panel.gameView.showDisCardRedmine(false);

            match.setStage(operate.stage);
            Room cloneRoom = Room.room.clone();

            operate.cloneMatch.banker = match.banker;
            operate.cloneMatch.mindex = match.mindex;

            Room.room.addSpotRedCount(operate.cloneMatch.getSpotRedCounts());
            AYMJOverPanel overpanel = UnrealRoot.root.getDisplayObject<AYMJOverPanel>();
            overpanel.refrshData(cloneRoom, operate.cloneMatch);
            overpanel.setVisible(true);
            overpanel.showIsReplay(false);

            operate.playOver();

            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
        }
    }
}
