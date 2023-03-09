namespace platform.poker
{
    /// <summary> 游戏开始 具体操作 </summary>
    public class DDZMStartGameProcess : Process
    {
        private DDZMStartGameOperate operate;

        public DDZMStartGameProcess(object operate)
        {
            this.operate = (DDZMStartGameOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = DDZMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            if (panel.fzlview != null)
                panel.fzlview.setVisible(false);

            room.setStatus(Room.STATE_MATCH, true);
            room.roomRule.setGameNum(operate.tray);
            room.cancelReady();

            match.replay = false;
            match.lordCardsInit(3);
            match.multipleBean.resetData();
            match.setRoomRule(room.roomRule);
            match.step = operate.operateData.step;
            match.setStage(operate.operateData.stage);
            match.setPlayers(room.players, room.indexOf());

            panel.refreshGameView();
            panel.gameView.recorder.setVisible(false);
            panel.gameView.clock.showClock(operate.operateData.round);
            panel.gameView.desktop.hideCards();
            panel.gameView.landlordcards.setData(match.lordCards);
            panel.gameView.hand.resetMingPai();

            operate.playOver();
        }
    }
}
