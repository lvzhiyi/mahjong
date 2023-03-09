using cambrian.common;
using cambrian.game;
using platform.spotred;

namespace platform.poker
{
    /// <summary> 扑克-房间-接收玩家加入房间 </summary>  后台广播
    public class RecvPKAddRoomMsg : RecvMsgHandle
    {
        public RecvPKAddRoomMsg()
        {
            gamePlatform = GamePlatform.PK_GAME;
        }

        public override void handle(ByteBuffer data)
        {
            int index = data.readInt();
            long score = data.readLong();

            var room = Room.room;
            var player = new MatchPlayer();
            player.bytesRead(data);

            if (Room.room != null)
            {
                Room.room.fulltime = data.readLong();
                room.getSpotRedCount().getIndexCount(index).score = score;
                room.players[index] = player;

                if (room.isType(Room.JBC) && !room.isType(Room.CLUB))
                {
                    Alert.show("金币场暂未实现");
                }
                else
                {
                    PKRoomPanel.Panel.refreshWaitView();
                    showFZLView(room);
                }
            }


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
                PKRoomPanel panel = PKRoomPanel.Panel;
                panel.fzlview.refresh();
                panel.fzlview.setVisible(true);
                panel.waitView.room_card.ready.setVisible(!room.players[room.indexOf()].isReady());
                WXManager.getInstance().vibrator(1000);
            }
        }
    }
}
