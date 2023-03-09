namespace platform.poker
{
    public class ReplayPDKAnYueRefreshProcess : Process
    {
        private PDKAnYueMatch scene;

        public ReplayPDKAnYueRefreshProcess(PDKAnYueMatch scene)
        {
            this.scene = scene;
        }

        private PPDKAnYueReplayRoomPanel panel;

        public override void execute()
        {
            panel = UnrealRoot.root.getDisplayObject<PPDKAnYueReplayRoomPanel>();

            scene.replay = true;

            PDKAnYueMatch.match = PDKAnYueMatch.match.cloneCardMatch(scene);

            var match = PDKAnYueMatch.match;

            var cards = match.getPlayerCardss<PDKPlayerCards>();

            var deskCards = match.deskCard.deskCacheCard;

            panel.topView.refresh();
            panel.topView.setMuitplier(match.multiple.boomPoint);                   
            panel.gameView.hand.refresh();
            panel.gameView.recorder.setData(match.recorder.getRemainCount());     
            panel.gameView.clock.StopAllCoroutines();

            for (int i = 0; i < match.players.Length; i++)                         
            {
                panel.gameView.status.setCardNum(i, cards[i].handcards.count);
                if (cards[i].handcards.count != 0)
                {
                    panel.gameView.hand.refresHands(true, i, CardSort.LTSCards(cards[i].handcards.toArray()));
                }
                if (match.isstage(PDKSTAGE.MATCH))
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
            panel.gameView.clock.showClock(match.rounds);
            panel.gameView.desktop.hideCards(match.rounds);
        }

        public int indexs(int round)
        {
            return round - 1 < 0 ? 2 : round - 1;
        }
    }
}