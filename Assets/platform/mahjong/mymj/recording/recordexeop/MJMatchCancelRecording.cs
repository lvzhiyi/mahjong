namespace platform.mahjong
{
    /// <summary>
    /// 取消
    /// </summary>
    public class MJMatchCancelRecording : Process
    {
        MJMatchCancelOperate operate;

        public MJMatchCancelRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchCancelOperate)operate;
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();

            panel.showSingleOperate(operate.index,MJOperate.CAN_CANCEL);

            operate.playOver();
        }
    }
}
