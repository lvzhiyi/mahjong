namespace platform.mahjong
{
    /// <summary>
    /// 更新比赛状态
    /// </summary>
    public class AYMJMatchUpdateStatesProcess:Process
    {
        MJMatchUpdateStateslOperate operate;

        int selfOperate;

        public AYMJMatchUpdateStatesProcess(MJBaseOperate operate)
        {
            this.operate=(MJMatchUpdateStateslOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        AYMJRoomPanel panel;
        public override void execute()
        {
            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            panel = UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.showCountTime(operate.round);

            if (selfOperate > 0) //无操作
            {
                AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(match.mindex).handCardSort(true);
                int card = match.getSelfPlayerCards<AYMJPlayerCards>().mocard;
                panel.refreshHandCard(match.mindex, match.getSelfTingCards(match.mindex), false);
                panel.gameView.getOperateView().showOperate(AYMJMatch.match.getMJOperateInfos(selfOperate, card, false, AYMJMatch.match.mindex));
                panel.gameView.setTingView(null);
            }

            else
                panel.gameView.getOperateView().setVisible(false);

            operate.playOver();
        }
    }
}
