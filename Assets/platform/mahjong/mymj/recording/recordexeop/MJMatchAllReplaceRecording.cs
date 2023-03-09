namespace platform.mahjong
{
    /// <summary>
    /// 所有人替换完后
    /// </summary>
    public class MJMatchAllReplaceRecording : Process
    {
        MJMatchAllReplaceOperate operate;

        public MJMatchAllReplaceRecording(MJBaseOperate operate)
        {
            this.operate = (MJMatchAllReplaceOperate)operate;
        }

        ReplayMahjongRoomPanel panel;

        public override void execute()
        {
            MJMatch match = MJMatch.match;
            match.setData(operate.stage, operate.step, operate.paidui, operate.round);
            match.addHuanSZCards(operate.cards);//添加后，已经排好序

            panel = UnrealRoot.root.getDisplayObject<ReplayMahjongRoomPanel>();
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
            if (MJMatch.match.isstage(MJMatch.STAGE_DISTYPE))
            {
                panel.gameView.getMjIndexCenterView().showAllDirection();
                panel.refreshDingQue(0, true);
                panel.gameView.getDQView().showDingQue(MJMatch.match.getRecommendType());
            }
            else
            {
                //定缺后，需要排序
                bool b = false;
                for (int i = 0; i < MJMatch.match.players.Length; i++)
                {
                    b = (i == MJMatch.match.banker);
                    MJMatch.match.getPlayerCardIndex<MJPlayerCards>(i).handCardSort(b);
                }

                panel.refreshHandCard(0, null, false, true);
                int card = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(MJMatch.match.banker).mocard;
                panel.showOperates(operate.operates, card);

            }
            operate.playOver();
        }
    }
}
