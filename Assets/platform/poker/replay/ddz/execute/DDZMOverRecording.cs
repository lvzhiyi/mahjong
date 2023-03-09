using cambrian.common;

namespace platform.poker
{
    public class DDZMOverRecording : Process
    {
        private DDZMOverOperate operate;

        public DDZMOverRecording(object operate)
        {
            this.operate = (DDZMOverOperate)operate;
        }

        private DDZOverSinglePanel sPanel;

        private PDDZReplayRoomPanel rPanel;

        private string overAnamationName;

        public override void execute()
        {
            var room = Room.room;
            var match = DDZMatch.match;

            rPanel = UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>();
            sPanel = UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>();

            room.setStatus(Room.STATE_DESTORY, true);

            match.multipleBean.changeChunTian(operate.chuntian);
            match.setBase(operate.operateData.type,
                   operate.operateData.stage,
                   operate.operateData.paidui,
                   operate.operateData.step,
                   operate.operateData.round,
                   operate.operateData.operates);

            for (int i = 0; i < match.players.Length; i++)
            {
                rPanel.gameView.status.setCardNum(i, operate.playerCard[i].Length);
                if (operate.playerCard[i].Length != 0)
                {
                    rPanel.gameView.hand.refresHands(true, i, CardSort.LTSCards(operate.playerCard[i]));
                }
            }

            rPanel.topView.setMuitplier(match.grossMuitiplier);
            rPanel.gameView.recorder.setData(match.recorder.getRemainCount());
            rPanel.gameView.clock.setVisible(false);
            rPanel.headView.updateSocre();

            sPanel.setData(operate.socre, operate.status, operate.playerCard, operate.handcardsuse, operate.sendcards);
            sPanel.refresh();

            palyAnimation();
            operate.playOver();
        }

        public void palyAnimation()
        {
            if (StatusKit.hasStatus(operate.status[Room.room.indexOf()], PlayerStatus.CHUN_TIAN))
            {
                overAnamationName = "chuntian";
            }
            else if (StatusKit.hasStatus(operate.status[Room.room.indexOf()], PlayerStatus.FAN_CHUN_TIAN))
            {
                overAnamationName = "fanchun";
            }
            else overAnamationName = "";

            if (overAnamationName.Length != 0)
            {
                once(overAnamationName);
            }
            else sPanel.changeLayer();
        }

        public void once(string type)
        {
            sPanel.changeLayer(0.2f);
        }
    }
}