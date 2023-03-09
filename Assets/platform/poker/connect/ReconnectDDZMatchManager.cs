using basef.player;
using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    /// <summary> 斗地主重连 操作 </summary>
    public class ReconnectDDZMatchManager : BytesSerializable
    {
        private static ReconnectDDZMatchManager sceneManager;

        public static ReconnectDDZMatchManager manager
        {
            get
            {
                if (sceneManager == null)
                    sceneManager = new ReconnectDDZMatchManager();
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
            DDZCardType.CardRuleSet(Room.room.getRule());
        }

        public override void bytesRead(ByteBuffer data)
        {
            panel = PKRoomPanel.GetPanel<PDDZRoomPanel>();
            match = DDZMatch.match;

            match.deskCard.bytesRead(data);
            match.recorder.bytesRead(data);
            match.multipleBean.bytesRead(data);

            playercards = match.getPCards();

            refreshPanel();
        }

        private DDZMatch match;
        private PDDZRoomPanel panel;
        private PlayerCards[] playercards;

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
            panel.gameView.desktop.hideCards(round);
            panel.gameView.landlordcards.setData(match.lordCards);
            panel.topView.setMuitplier(match.grossMuitiplier);
            for (int i = 0; i < match.players.Length; i++)
            {
                bool isMingPai = playercards[i].hasStatus(PlayerStatus.MINGPAI);
                panel.gameView.status.setCardNum(i, playercards[i].handcards.count);
                panel.gameView.hand.refresHands(i, playercards[i].getSortHandCards(), isMingPai, i == match.diZhuIndex);

                if (isMingPai || i == match.mindex)
                {
                    match.recorder.addMPUserIndex(i);
                    match.recorder.incrCount(playercards[i].handcards.toArray());
                }
                panel.gameView.status.showCardWarning(i, playercards[i].handcards.count);
            }
            panel.setVisible(true);
            panel.gameView.recorder.setData(match.recorder.getRemainCount());
            panel.gameView.clock.showClockConnect(round, roundTime);
        }

        private void matchStageShow()
        {
            if (match.isstage(DDZSTAGE.MINGPAI))
            {
                panel.gameView.clock.setVisible(false);
                CommandManager.addCommand(new DDZSendMatchCommand(PKSendMatch.CANCEL_MING, match.step, null));
            }
            if (match.isstage(DDZSTAGE.JIAODIZHU) || match.isstage(DDZSTAGE.QIANGDIZHU))
            {
                qiangDiZhuEXE();
            }
            else if (match.isstage(DDZSTAGE.JIAOFEN))
            {
                jiaoFenEXE();
            }
            else if (match.isstage(DDZSTAGE.JIABEI))
            {
                jiaBeiShowEXE();
            }
            else if (match.isstage(DDZSTAGE.MATCH))
            {
                showMatchStatus();
            }
        }

        private void showMatchStatus()
        {
            DDZCardInfo info = null;
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
                panel.gameView.animator.animationPlay(info.type, info.cards, info.index);
            }

            panel.gameView.status.showBanker(match.diZhuIndex);
        }

        private void jiaBeiShowEXE()
        {
            for (int i = 0; i < playercards.Length; i++)
            {
                if (playercards[i].hasStatus(PlayerStatus.JIABEI))
                {
                    showStatus(i, PKStageStatus.JIA_BEI);
                }
                else if (playercards[i].hasStatus(PlayerStatus.NOJIABEI))
                {
                    showStatus(i, PKStageStatus.NO_JIA_BEI);
                }
            }
            panel.gameView.clock.showClockConnect(Room.room.indexOf(), roundTime);
            panel.gameView.status.showBanker(match.diZhuIndex);
        }

        private void qiangDiZhuEXE()
        {
            for (int i = 0; i < playercards.Length; i++)
            {
                if (operate != 0 && Room.room.getPlayerIndex(Player.player.userid) == i) continue;
                if (round == i) continue;

                if (playercards[i].hasStatus(PlayerStatus.QIANGDIZHU))
                {
                    showStatus(i, PKStageStatus.QIANG_DI_ZHU);
                }
                else if (playercards[i].hasStatus(PlayerStatus.NOQIANGDIZHU))
                {
                    showStatus(i, PKStageStatus.NO_QIANG_DI_ZHU);
                }
                else if (playercards[i].hasStatus(PlayerStatus.JIAODIZHU))
                {
                    showStatus(i, PKStageStatus.JIAO_DI_ZHU);
                }
                else if (playercards[i].hasStatus(PlayerStatus.NOJIAODIZHU))
                {
                    showStatus(i, PKStageStatus.NO_JIAO_DIZHU);
                }
                else if (playercards[i].hasStatus(PlayerStatus.MINGPAI))
                {
                    showStatus(i, PKStageStatus.MING_PAI);
                }
            }
        }

        private void jiaoFenEXE()
        {
            int score = 0;
            for (int i = 0; i < playercards.Length; i++)
            {
                if (operate != 0 && Room.room.getPlayerIndex(Player.player.userid) == i) continue;
                if (round == i) continue;
                if (playercards[i].hasStatus(PlayerStatus.CALLSCORE_ONE))
                {
                    showStatus(i, PKStageStatus.CALLSCORE_ONE);
                    if (score < 1) score = 1;
                }
                else if (playercards[i].hasStatus(PlayerStatus.CALLSCORE_TWO))
                {
                    showStatus(i, PKStageStatus.CALLSCORE_TWO);
                    if (score < 2) score = 2;
                }
                else if (playercards[i].hasStatus(PlayerStatus.CALLSCORE_THREE))
                {
                    showStatus(i, PKStageStatus.CALLSCORE_THREE);
                    if (score < 3) score = 3;
                }
                else if (playercards[i].hasStatus(PlayerStatus.NOJIAODIZHU))
                {
                    showStatus(i, PKStageStatus.NO_JIAO_DIZHU);
                }
            }
            match.setCallScore(score);
        }

        private void showStatus(int pos, int posType)
        {
            panel.gameView.stage.showStageStatus(pos, posType);
        }
    }
}
