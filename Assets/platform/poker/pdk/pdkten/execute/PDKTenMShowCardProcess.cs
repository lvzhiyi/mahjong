using platform.bean;

namespace platform.poker
{
    public class PDKTenMShowCardProcess : Process
    {
        private PDKTenMShowCardOperate operate;

        private PDKTenRoomPanel panel;

        private PDKTenMatch match;

        public PDKTenMShowCardProcess(object operate)
        {
            this.operate = (PDKTenMShowCardOperate)operate;
        }

        public PDKTenMShowCardProcess(OperateData operateData, int index)
        {
            operate = new PDKTenMShowCardOperate(null);
            operate.operateData = operateData;
            operate.index = index;
        }

        public override void execute()
        {
            if (operate.operateData.stage != PKSTAGE.MATCH) return;
            panel = (PDKTenRoomPanel)PKRoomPanel.Panel;
            match = PDKTenMatch.match;
            match.setStage(operate.operateData.stage);
            match.paidui = operate.operateData.paidui;
            match.step = operate.operateData.step;
            special();
            panel.gameView.clock.showClock(operate.operateData.round);
            if ((operate!=null&&operate.cardsinfo!=null && operate.cardsinfo.cards.Length != 10)||(operate != null && operate.cardsinfo == null))
                panel.gameView.desktop.hideCards(operate.operateData.round);

            if (Room.room.indexOf() == operate.index)
            {
                match.selectCard.clear();
                panel.gameView.hand.cancelAllSelect();
            }
            panel.gameView.operate.showOperateView(panel.operate);
            operate.playOver();
        }

        private void special()
        {
            if (operate.isType(DDZMatchMsg.CANCEL))
            {
                match.deskCard.states[operate.index] = -1;
                panel.gameView.stage.showStageStatus(operate.index, PKStageStatus.PASS);
                endstage();
            }
            else
            {
                var playerCards = match.getPCardIndex(operate.index);

                if (match.mindex != operate.index && !match.recorder.getMingPai(operate.index))
                {
                    match.recorder.incrCount(operate.cardsinfo.cards);
                    panel.gameView.recorder.setData(match.recorder.getRemainCount());
                }

                match.multiple.changeBoom(operate.cardsinfo);
                match.deskCard.add(operate.cardsinfo);

                playerCards.delHandCards(operate.cardsinfo.cards);
                panel.gameView.status.showCardWarning(operate.index, playerCards.handcards.count);
                int[] cards = playerCards.getSortHandCards();

                panel.topView.setMuitplier(match.multiple.boomPoint);
                panel.gameView.status.setCardNum(operate.index, cards.Length);
                panel.gameView.hand.refresHands(operate.index, cards);
            }
        }

        private void endstage()
        {
            int num = 0;
            for (int i = 0; i < match.playerCount; i++)
            {
                if (operate.operateData.round != i)
                {
                    num = (match.deskCard.getStates(i) == 0) ? num + 1 : num;
                    if (num >= 2)
                    {
                        panel.gameView.stage.hidePStatus(true);
                        match.deskCard.statesClaer();
                        return;
                    }
                }
            }
        }
    }
}
