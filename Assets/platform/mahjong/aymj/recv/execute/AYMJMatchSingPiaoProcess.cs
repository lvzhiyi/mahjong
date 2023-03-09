namespace platform.mahjong
{
    /// <summary>
    /// 单个人选择飘
    /// </summary>
    public class AYMJMatchSingPiaoProcess:Process
    {
        MJMatchSinglePiaoOperate operate;

        int selfOperate;

        public AYMJMatchSingPiaoProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSinglePiaoOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();

            panel.refreshSinglePiao(operate.index);

            panel.showHuanOrDing(operate.index);
            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }
    }
}
