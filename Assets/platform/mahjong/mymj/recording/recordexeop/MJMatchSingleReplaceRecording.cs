namespace platform.mahjong
{
    /// <summary>
    /// 处理单个玩家换牌消息
    /// </summary>
    public class MJMatchSingleReplaceRecording : Process
    {
        MJMatchSingleReplaceOperate operate;

        int selfOperate;

        public MJMatchSingleReplaceRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.getPCards()[operate.index].setStatus(MJPlayerCards.STATUS_REPLACE);
            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();

            MJPlayerCards playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(operate.index);
            playerCards.delHandCards(operate.replacecard);
            playerCards.setStatus(MJPlayerCards.STATUS_REPLACE);

           // panel.refreshSelfHuanSuccessHandCard(operate.index, operate.replacecard);
            panel.refreshHandCard(0,null,false,true);
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
