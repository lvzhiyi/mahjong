using cambrian.common;

namespace platform.poker
{
    public class ReconnectPDKMatchManager : BytesSerializable
    {
        private static ReconnectPDKMatchManager sceneManager;
        public static ReconnectPDKMatchManager manager
        {
            get
            {
                if (sceneManager == null)
                    sceneManager = new ReconnectPDKMatchManager();
                return sceneManager;
            }
        }

        private int round, operate;

        private long roundTime;

        public void setData(int round, long roundTime, int operate)
        {
            this.round = round;
            this.operate = operate;
            this.roundTime = roundTime;
        }

        private PDKMatch match;

        private PPDKRoomPanel panel;

        private PlayerCards[] playercards;

        public override void bytesRead(ByteBuffer data)
        {
            panel = PKRoomPanel.GetPanel<PPDKRoomPanel>();
            match = PDKMatch.match;

            match.deskCard.bytesRead(data);
            match.recorder.bytesRead(data);
            match.multiple.bytesRead(data);
            match.firstCard = data.readInt();

            playercards = match.getPCards();

            refreshPanel();
        }

        private void refreshPanel()
        {
            Room.room.cancelReady();
            panelShow();
            matchStageShow();
            panel.gameView.desktop.hideCards(round);
            panel.gameView.operate.showOperateView(operate);
        }

        private void panelShow()
        {
            panel.headView.refresh();
            panel.clearBaseOperate();
            panel.refreshGameView();
            panel.operate = operate;
            panel.topView.setMuitplier(match.multiple.boomPoint);
            for (int i = 0; i < match.players.Length; i++)
            {
                panel.gameView.status.setCardNum(i, playercards[i].handcards.count);
                panel.gameView.hand.refresHands(i, playercards[i].getSortHandCards(), false, false);
                panel.gameView.status.showCardWarning(i, playercards[i].handcards.count);
            }
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            panel.setVisible(true);
            panel.gameView.clock.showClockConnect(round, roundTime);
        }

        private void matchStageShow()
        {
            if (match.isstage(DDZSTAGE.MATCH))
            {
                showMatchStatus();
            }
        }

        private void showMatchStatus()
        {
            PDKCardInfo info = null;
            for (int i = 0; i < match.deskCard.deskCacheCard.Length; i++)
            {
                if (match.deskCard.deskCacheCard[i] != null)
                {
                    if (playercards[i].hasStatus(PlayerStatus.PASS))
                    {
                        showStatus(i, PKStageStatus.PASS);
                    }
                    else if (match.deskCard.deskCacheCard[i].cards.Length != 0 || match.deskCard.states[i] == 1)
                    {
                        match.deskCard.add(match.deskCard.deskCacheCard[i], false);
                        info = match.deskCard.deskCacheCard[i];
                    }
                }
            }
            if (info != null)
            {
                panel.gameView.animator.animationPlay(info.type, info.cards, info.master);
            }
        }

        private void showStatus(int pos, int posType)
        {
            panel.gameView.stage.showStageStatus(pos, posType);
        }
    }
}
