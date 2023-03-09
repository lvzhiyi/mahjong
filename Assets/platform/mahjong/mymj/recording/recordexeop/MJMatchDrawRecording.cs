namespace platform.mahjong
{
    /// <summary>
    /// 摸牌
    /// </summary>
    public class MJMatchDrawRecording : Process
    {
        MJMatchDrawOperate operate;

        public MJMatchDrawRecording(MJBaseOperate baseOperate)
        {
            operate = (MJMatchDrawOperate) baseOperate;
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
            panel.refreshCardNum();
            panel.showCountTime(operate.round);

            match.isTianhu = false;

            if (match.kongIndex != operate.index) //杠牌人的索引不是自己
            {
                match.resetKongStatus();
            }

            match.ResetPlayerCard();
            match.setLastPlayerCard(operate.index, operate.card);

            MJPlayerCards playerCards = match.getPlayerCardIndex<MJPlayerCards>(operate.index);
            playerCards.setMOCard(operate.card);


            panel.refreshHandCard(operate.index, null, false);

            showOperate();
        }

        private void showOperate()
        {
            panel.showOperates(operate.operates,operate.card);
            operate.playOver();
        }
    }
}
