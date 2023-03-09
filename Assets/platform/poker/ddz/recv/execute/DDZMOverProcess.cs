using cambrian.common;
using DragonBones;

namespace platform.poker
{
    /// <summary> 游戏结束 具体操作 </summary>
    public class DDZMOverProcess : Process
    {
        private DDZMOverOperate operate;

        public DDZMOverProcess(object operate)
        {
            this.operate = (DDZMOverOperate)operate;
        }

        private DDZOverSinglePanel sPanel;

        private PDDZRoomPanel rPanel;

        private string overAnamationName;

        public override void execute()
        {
            var room = Room.room;
            var match = DDZMatch.match;

            rPanel = UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
            sPanel = UnrealRoot.root.getDisplayObject<DDZOverSinglePanel>();

            if (room.roomRule.num + 1 < room.roomRule.getMatchCount())
            {
                room.roomRule.setGameNum(room.roomRule.num + 1);
                room.setStatus(Room.STATUS_READY, true);
            }
            else room.setStatus(Room.STATE_DESTORY, true);
            match.recorder.recorderClear();
            match.multipleBean.changeChunTian(operate.chuntian);
            for (int i = 0; i < match.players.Length; i++)
            {
                room.getSpotRedCount().getIndexCount(i).score += operate.socre[i];
                rPanel.gameView.status.setCardNum(i, operate.playerCard[i].Length);
            }
            rPanel.topView.setMuitplier(match.multipleBean.points[match.mindex]);
            rPanel.gameView.recorder.setData(match.recorder.getRemainCount());
            rPanel.gameView.operate.hideOperateView();
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