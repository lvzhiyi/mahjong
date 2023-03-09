namespace platform.mahjong
{
    /// <summary>
    /// 处理单个玩家换牌消息
    /// </summary>
    public class AYMJMatchSingleReplaceProcess:Process
    {
        MJMatchSingleReplaceOperate operate;

        int selfOperate;

        public AYMJMatchSingleReplaceProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.getPCards()[operate.index].setStatus(AYMJPlayerCards.STATUS_REPLACE);
            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();

            AYMJPlayerCards playerCards =match.getPlayerCardIndex<AYMJPlayerCards>(operate.index);
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
