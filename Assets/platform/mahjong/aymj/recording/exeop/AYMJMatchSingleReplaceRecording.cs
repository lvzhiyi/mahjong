namespace platform.mahjong
{
    /// <summary>
    /// 处理单个玩家换牌消息
    /// </summary>
    public class AYMJMatchSingleReplaceRecording : Process
    {
        MJMatchSingleReplaceOperate operate;

        int selfOperate;

        public AYMJMatchSingleReplaceRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.getPCards()[operate.index].setStatus(AYMJPlayerCards.STATUS_REPLACE);
            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();

            AYMJPlayerCards playerCards =match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
            playerCards.delHandCards(operate.replacecard);

            playerCards.setStatus(MJPlayerCards.STATUS_REPLACE);
            panel.refreshHandCard(0, null, false, true);
            panel.showHuanOrDing(operate.index);
            panel.refreshHuanSZ(operate.index, false);
            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }
    }
}
