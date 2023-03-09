namespace platform.mahjong
{
    /// <summary>
    /// 单个人定缺
    /// </summary>
    public class AYMJMatchSingDistypeProcess:Process
    {
        MJMatchSingleDistypeOperate operate;

        int selfOperate;

        public AYMJMatchSingDistypeProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchSingleDistypeOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);

            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            
            panel.refreshSingleDingQue(operate.index);

            if (operate.index ==match.mindex)
            {
                AYMJPlayerCards playerCards=match.getSelfPlayerCards<AYMJPlayerCards>();
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
