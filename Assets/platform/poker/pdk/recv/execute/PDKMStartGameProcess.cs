namespace platform.poker
{
    /// <summary> 游戏开始 具体操作 </summary>
    public class PDKMStartGameProcess : Process
    {
        private PDKMStartGameOperate operate;

        public PDKMStartGameProcess(object operate)
        {
            this.operate = (PDKMStartGameOperate)operate;
        }

        public override void execute()
        {
            var room = Room.room;
            var match = PDKMatch.match;
            var panel = UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
            if (panel.fzlview != null)
                panel.fzlview.setVisible(false);

            room.setStatus(Room.STATE_MATCH, true);
            room.roomRule.setGameNum(operate.tray);
            room.cancelReady();

            match.multiple.reset();
            match.setRoomRule(room.roomRule);
            match.step = operate.operateData.step;
            match.setStage(operate.operateData.stage);
            match.setPlayers(room.players, room.indexOf());

            panel.refreshGameView();
            panel.gameView.recorder.setVisible(false);
            panel.gameView.clock.showClock(operate.operateData.round);
            panel.gameView.desktop.hideCards();
            panel.gameView.hand.resetMingPai();

            operate.playOver();
        }
    }
}
