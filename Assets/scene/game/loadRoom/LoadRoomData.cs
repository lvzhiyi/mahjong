using cambrian.common;
using platform;

namespace scene.game
{
    public class LoadRoomData : Process
    {

        ByteBuffer data;

        public LoadRoomData(ByteBuffer data)
        {
            this.data = data;
        }

        public override void execute()
        {
            if (data.readBoolean()) //是否在匹配金币场
            {

            }

            if (data.readBoolean()) //是否有在房间里
            {
                reConnectRoom(data);
            }
            else
            {
                GameDataRoot.gameDataRoot.showDefaultWindow();
            }
        }


        public static void reConnectRoom(ByteBuffer data)
        {
            Room.instance();
            Room.room.bytesRead(data);
            new ReConnectCheckOperateProcess(back, data).execute();
        }

        public static void back(ByteBuffer data)
        {
            new ReConnectOperate(Room.room, data).execute();
            GameDataRoot.gameDataRoot.showDefaultWindow();
        }
    }
}
