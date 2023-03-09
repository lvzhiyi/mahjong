namespace platform.mahjong
{
    /// <summary>
    /// 取消
    /// </summary>
    public class AYMJMatchCancelRecording : Process
    {
        MJMatchCancelOperate operate;

        int selfOperate;

        public AYMJMatchCancelRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchCancelOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);

            int card = MJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);

            operate.playOver();
        }
    }
}
