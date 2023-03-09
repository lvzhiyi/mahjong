using UnityEngine;

namespace platform.poker
{
    public class ReplayDDZRefreshProcess : Process
    {
        private DDZMatch scene;

        public ReplayDDZRefreshProcess(DDZMatch scene)
        {
            this.scene = scene;
        }

        private PDDZReplayRoomPanel panel;

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();

            scene.replay = true;

            DDZMatch.match = DDZMatch.match.cloneCardMatch(scene);

            var match = DDZMatch.match;
            var cards = match.getPlayerCardss<DDZPlayerCards>();
            var deskCards = match.deskCard.deskCacheCard;

            panel.topView.setMuitplier(match.grossMuitiplier);                      
            panel.gameView.hand.refresh();
            panel.gameView.landlordcards.setData(match.lordCards);
            panel.gameView.recorder.setData(match.recorder.getRemainCount());     
            panel.gameView.clock.StopAllCoroutines();
   
            for (int i = 0; i < match.players.Length; i++)                           
            {
                panel.gameView.status.setCardNum(i, cards[i].handcards.count);
                if (cards[i].handcards.count != 0)
                {
                    panel.gameView.hand.refresHands(true, i, CardSort.LTSCards(cards[i].handcards.toArray()));
                }
                if (match.isstage(DDZSTAGE.MATCH))
                {
                    if (deskCards[i].cards != null && deskCards[i].cards.Length != 0)
                    {
                        panel.gameView.desktop.showCards(i, deskCards[i].cards);
                    }
                    else if (match.deskCard.states[i] == -1)
                    {
                        panel.gameView.stage.showStageStatus(indexs(match.rounds), PKStageStatus.PASS, true);
                    }
                    else
                    {
                        panel.gameView.desktop.hideCards(i);
                    }
                }
            }
            panel.topView.refresh();
            panel.gameView.clock.showClock(match.rounds);
            panel.gameView.desktop.hideCards(match.rounds);
        }

        public int indexs(int round)
        {
            return round - 1 < 0 ? 2 : round - 1;
        }   
    }
}
