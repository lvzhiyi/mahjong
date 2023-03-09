namespace platform.mahjong
{
    /// <summary>
    /// 单个人选择飘
    /// </summary>
    public class MJMatchSingPiaoProcess:Process
    {
        MJMatchSinglePiaoOperate operate;

        int selfOperate;

        public MJMatchSingPiaoProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSinglePiaoOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
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
