namespace platform.mahjong
{
    /// <summary>
    /// 单局结束
    /// </summary>
    public class MJMatchOverRecording : Process
    {
        MJMatchOverOperate operate;

        int selfOperate;

        public MJMatchOverRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchOverOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            Room.tempMatchs.add(operate.cloneMatch);

            ReplayMahjongRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            match.setStage(operate.stage);
            Room.room.addSpotRedCount(operate.cloneMatch.getSpotRedCounts());
            Room cloneRoom = Room.room.clone();

            operate.cloneMatch.banker = match.banker;
            operate.cloneMatch.mindex = match.mindex;

           
            ReplayMJOverPanel overpanel = UnrealRoot.root.getDisplayObject<ReplayMJOverPanel>();
            overpanel.refrshData(cloneRoom, operate.cloneMatch);
            overpanel.setVisible(true);
            
            operate.playOver();
        }
    }
}
