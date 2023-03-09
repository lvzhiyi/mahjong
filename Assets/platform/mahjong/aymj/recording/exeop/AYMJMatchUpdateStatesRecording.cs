namespace platform.mahjong
{
    /// <summary>
    /// 更新比赛状态
    /// </summary>
    public class AYMJMatchUpdateStatesRecording : Process
    {
        MJMatchUpdateStateslOperate operate;

        int selfOperate;

        public AYMJMatchUpdateStatesRecording(MJBaseOperate operate)
        {
            this.operate=(MJMatchUpdateStateslOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;
        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.showCountTime(operate.round);

            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }
    }
}
