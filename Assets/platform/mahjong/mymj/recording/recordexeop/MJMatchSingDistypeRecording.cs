namespace platform.mahjong
{
    /// <summary>
    /// 单个人定缺
    /// </summary>
    public class MJMatchSingDistypeRecording : Process
    {
        MJMatchSingleDistypeOperate operate;

        int selfOperate;

        public MJMatchSingDistypeRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleDistypeOperate)operate;
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
            
            panel.refreshSingleDingQue(operate.index);

            MJPlayerCards playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(operate.index);
            playerCards.setStatus(MJPlayerCards.STATUS_DISTYPE);

            panel.showHuanOrDing(operate.index);
            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }
    }
}
