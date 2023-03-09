using cambrian.common;
using platform;

namespace basef.arena
{
    public class ChangeArenaRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaRoom room = transform.GetComponent<ArenaChangTableBar>().getRoom();
            CommandManager.addCommand(new ArenaChangeRoomCommand(room.roomid, room.createtime), joinRoom);
        }

        public void joinRoom(object obj)
        {
            if (obj == null) return;
            ByteBuffer data = (ByteBuffer)obj;
            int result = data.readInt();
            if (result != 0)
            {
                string str = "";
                if (result == -1)
                    str = "进入房间失败：房间已满";
                else if (result == -2 || result == -3)
                    str = "进入房间失败：房间已解散";
                else if (result == -4)
                    str = "进入房间失败：比赛已开始";
                else if (result == -5)
                    str = "进入房间失败：你和房间内玩家距离较近";
                else if (result == -6)
                        str = "进入房间失败：你的GPS没有打开";
                else if (result == -7)
                        str = "进入房间失败：你被禁止和房间内玩家游戏";
                else if (result == -8)
                    str = "进入房间失败：你被禁止和房间内玩家游戏";
                Alert.show(str);
            }
            Room.instance();
            Room.room.bytesRead(data);
            new ShowWaiteRoomCallBackProcess().execute();
        }
    }
}
