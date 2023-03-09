using cambrian.common;
using cambrian.game;
using platform.spotred;
using platform.spotred.room;

namespace platform.mahjong
{
    /// <summary>
    /// 接收麻将加入房间
    /// </summary>
    public class RecvMJAddRoomMsg : RecvMsgHandle
    {
        public RecvMJAddRoomMsg()
        {
            this.gamePlatform = GamePlatform.MJ_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            long score = data.readLong();


            Room room = Room.room;
            if (room == null) return;

            room.getSpotRedCount().getIndexCount(index).score = score;

            MatchPlayer player = new MatchPlayer();
            player.bytesRead(data);

            room.fulltime = data.readLong();

            if (Room.room != null)
            {
                Room.room.players[index] = player;

                MahJongPanel panel = MahJongPanel.getPanel();
                panel.refresh();
                panel.showWaitView();
                if (Room.room.getRoomRealPlayerCount() > 1)
                    panel.showIPView();
                showFZLView(room);
            }
            DisbandPanel disbandPanel = UnrealRoot.root.getDisplayObject<DisbandPanel>();
            disbandPanel.clear();
            disbandPanel.setVisible(false);
        }

        /// <summary>
        /// 显示防暂离视图
        /// </summary>
        public void showFZLView(Room room)
        {
            if (!room.isType(Room.CLUB)) return;
            if (room.fulltime != 0)
            {
                int leaveTimeOut = room.getRule().getIntAtribute("leaveTimeout");
                if (leaveTimeOut == 0) return;
                MahJongPanel panel = MahJongPanel.getPanel();
                panel.fzlView.refresh();
                panel.fzlView.setVisible(true);
                panel.getWaitView().ready.setVisible(!room.players[room.indexOf()].isReady());
                WXManager.getInstance().vibrator(1000);
            }
        }
    }
}
