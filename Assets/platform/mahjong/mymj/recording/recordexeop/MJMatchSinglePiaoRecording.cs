namespace platform.mahjong
{
    public class MJMatchSinglePiaoRecording: Process
    {
        MJMatchSinglePiaoOperate operate;

        int selfOperate;

        public MJMatchSinglePiaoRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchSinglePiaoOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
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
