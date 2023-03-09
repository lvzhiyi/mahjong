namespace platform.poker
{
    public class PDKAnYueMOverProcess : Process
    {
        private PDKAnYueMOverOperate operate;

        public PDKAnYueMOverProcess(object operate)
        {
            this.operate = (PDKAnYueMOverOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var rPanel = UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();
            var sPanel = UnrealRoot.root.getDisplayObject<PDKAnYueOverSinglePanel>();
            var match = PDKAnYueMatch.match;
            match.remainingCards = operate.surplusCards;
            if (room.roomRule.num + 1 < room.roomRule.getMatchCount())         
            {
                room.roomRule.setGameNum(room.roomRule.num + 1);                 
                room.setStatus(Room.STATUS_READY, true);                         
            }
            else room.setStatus(Room.STATE_DESTORY, true);                        
            for (int i = 0; i < match.players.Length; i++)                        
            {
                room.getSpotRedCount().getIndexCount(i).score += operate.scores[i];
                rPanel.gameView.status.setCardNum(i, operate.playerCard[i].Length);
            }
            rPanel.gameView.recorder.setData(match.recorder.getRemainCount());
            rPanel.gameView.operate.hideOperateView();
            rPanel.gameView.clock.setVisible(false);
            rPanel.headView.updateSocre();
            sPanel.setData(operate.scores, operate.springPoint, operate.bombScores, operate.cardsLength, operate.surplusCards, operate.playerCard, operate.playCards);
            sPanel.refresh();
            sPanel.changeLayer();
            rPanel.topView.refresh(); 
            operate.playOver();
        }
    }
}
