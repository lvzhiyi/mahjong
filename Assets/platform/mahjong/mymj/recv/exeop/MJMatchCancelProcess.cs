namespace platform.mahjong
{
    /// <summary>
    /// 取消
    /// </summary>
    public class MJMatchCancelProcess:Process
    {
        MJMatchCancelOperate operate;

        int selfOperate;

        public MJMatchCancelProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchCancelOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        MahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
            panel.setOperate(selfOperate);

            if (selfOperate > 0) //无操作
            {
                panel.refreshHandCard(match.mindex, match.getSelfTingCards(MJMatch.match.mindex), false);
            }
                
            else
                panel.gameView.getOperateView().setVisible(false);

            operate.playOver();
        }
    }
}
