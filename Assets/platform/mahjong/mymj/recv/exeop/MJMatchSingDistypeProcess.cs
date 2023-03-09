namespace platform.mahjong
{
    /// <summary>
    /// 单个人定缺
    /// </summary>
    public class MJMatchSingDistypeProcess:Process
    {
        MJMatchSingleDistypeOperate operate;

        int selfOperate;

        public MJMatchSingDistypeProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleDistypeOperate)operate;
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
            
            panel.refreshSingleDingQue(operate.index);

            if (operate.index == MJMatch.match.mindex)
            {
                MJPlayerCards playerCards= MJMatch.match.getSelfPlayerCards<MJPlayerCards>();
                panel.gameView.showSingleDistypeCard(operate.index,playerCards.distype);
            }

            panel.showHuanOrDing(operate.index);
            showOperate();
        }

        private void showOperate()
        {
            operate.playOver();
        }
    }
}
