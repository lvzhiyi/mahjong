namespace platform.mahjong
{
    /// <summary>
    /// 取消
    /// </summary>
    public class AYMJMatchCancelProcess:Process
    {
        MJMatchCancelOperate operate;

        int selfOperate;

        public AYMJMatchCancelProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchCancelOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;

        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);

            if (selfOperate > 0) //无操作
            {
                panel.refreshHandCard(match.mindex, match.getSelfTingCards(match.mindex), false);
            }
                
            else
                panel.gameView.getOperateView().setVisible(false);

            operate.playOver();
        }
    }
}
