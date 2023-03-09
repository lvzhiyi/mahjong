namespace platform.mahjong
{
    /// <summary>
    /// 所有人替换完后
    /// </summary>
    public class AYMJMatchAllReplaceRecording : Process
    {
        MJMatchAllReplaceOperate operate;

        int selfOperate;

        public AYMJMatchAllReplaceRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchAllReplaceOperate)operate;
            selfOperate = operate.getSelfOperate();
        }

        ReplayAYMJRoomPanel panel;

        public override void execute()
        {

            AYMJMatch match = AYMJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.addHuanSZCards(operate.cards);//添加后，已经排好序

            panel = UnrealRoot.root.getDisplayObject<ReplayAYMJRoomPanel>();
            panel.setOperate(selfOperate);
            panel.refreshCardNum();
            panel.showCountTime(operate.round);


            //这里要考虑换进来的牌有点动画效果
            panel.refreshHandCard(0, null, false, true);//刷新手牌

            panel.gameView.getHuanSZView().setVisible(false);
            panel.gameView.getHSZOverView().showMode(operate.replaceMode, null);
            showOperate();
        }


        private void showOperate()
        {
            AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).handCardSort(true);
            panel.refreshHandCard(0, null, false, true);//刷新手牌
            int card = AYMJMatch.match.getPlayerCardIndex<AYMJPlayerCards>(AYMJMatch.match.banker).mocard;
            panel.showOperates(operate.operates, card);
            operate.playOver();
        }
    }
}
