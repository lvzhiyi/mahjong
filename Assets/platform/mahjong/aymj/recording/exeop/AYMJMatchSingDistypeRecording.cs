namespace platform.mahjong
{
    /// <summary>
    /// 单个人定缺
    /// </summary>
    public class AYMJMatchSingDistypeRecording : Process
    {
        MJMatchSingleDistypeOperate operate;

        int selfOperate;

        public AYMJMatchSingDistypeRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleDistypeOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            
            panel.refreshSingleDingQue(operate.index);

            AYMJPlayerCards playerCards = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
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
