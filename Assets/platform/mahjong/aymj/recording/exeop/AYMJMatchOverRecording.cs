using cambrian.game;

namespace platform.mahjong
{
    /// <summary>
    /// 单局结束
    /// </summary>
    public class AYMJMatchOverRecording : Process
    {
        AYMJMatchOverOperate operate;

        int selfOperate;

        public AYMJMatchOverRecording(MJBaseOperate operate)
        {
            this.operate = (AYMJMatchOverOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            Room.tempMatchs.add(operate.cloneMatch);

            ReplayAYMJRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();

            panel.gameView.showDisCardRedmine(false);

            match.setStage(operate.stage);
            Room.room.addSpotRedCount(operate.cloneMatch.getSpotRedCounts());
            Room cloneRoom = Room.room.clone();

            operate.cloneMatch.banker = match.banker;
            operate.cloneMatch.mindex = match.mindex;

            AYMJOverPanel overpanel = UnrealRoot.root.getDisplayObject<AYMJOverPanel>();
            overpanel.refrshData(cloneRoom, operate.cloneMatch);
            overpanel.setVisible(true);
            overpanel.showIsReplay(true);


            operate.playOver();

            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
        }
    }
}
