namespace platform.mahjong
{
    /// <summary>
    /// 处理单个玩家换牌消息
    /// </summary>
    public class MJMatchSingleReplaceProcess:Process
    {
        MJMatchSingleReplaceOperate operate;

        int selfOperate;

        public MJMatchSingleReplaceProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.getPCards()[operate.index].setStatus(MJPlayerCards.STATUS_REPLACE);
            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();

            MJPlayerCards playerCards = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(operate.index);
            playerCards.delHandCards(operate.replacecard);

            if(operate.index==match.mindex)
                playerCards.handCardSort(false);

            int[] c = new int[operate.replacecard.Length];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = MJCard.CIN;
            }

            panel.refreshSelfHuanSuccessHandCard(operate.index, c);
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
