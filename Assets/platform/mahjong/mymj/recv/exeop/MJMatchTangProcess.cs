namespace platform.mahjong
{
    /// <summary>
    /// 执行躺牌操作
    /// </summary>
    public class MJMatchTangProcess:Process
    {
        MJMatchTangOperate operate;

        int selfOperate;

        public MJMatchTangProcess(MJBaseOperate operate)
        {
            this.operate = (MJMatchTangOperate)operate;
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

            MJPlayerCards playerCards = match.getPlayerCardIndex<MJPlayerCards>(operate.index);
            if (operate.index == match.mindex)//自己躺牌
            {
                playerCards.signTang(operate.cards);
                playerCards.setStatus(MJPlayerCards.STATUS_TANG);
                panel.gameView.showTingDeng(match.getDengTingCards(MJMatch.match.mindex) != null);
            }
            else
            {
                int[] c = new int[operate.cards.Length];
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = MJCard.CIN;
                }
                playerCards.delHandCards(c);
                for (int i = 0; i < operate.cards.Length; i++)
                {
                    operate.cards[i] = MJCard.sign(operate.cards[i], MJCard.SIGN_TANG);
                }

                playerCards.AddHandCards(operate.cards);
                playerCards.handCardSort(false);
                playerCards.setStatus(MJPlayerCards.STATUS_TANG);
            }

            panel.refreshTangImage(operate.index);


            panel.refreshHandCard(operate.index,null,false);

            showOperate();
        }

        private void showOperate()
        {
            if (selfOperate > 0)
            {
                int card = MJMatch.match.getLastPlayerCard();
                panel.gameView.getOperateView().showOperate(MJMatch.match.getMJOperateInfos(selfOperate, card, false, MJMatch.match.mindex));
            }
            operate.playOver();
        }
    }
}
