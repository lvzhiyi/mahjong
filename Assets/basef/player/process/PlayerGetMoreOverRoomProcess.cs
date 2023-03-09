using cambrian;
using cambrian.common;
using scene.game;

namespace basef.player
{
    /// <summary>
    /// 获取更多的房间
    /// </summary>
    public class PlayerGetMoreOverRoomProcess : Process
    {
        private int index;

        public PlayerGetMoreOverRoomProcess(int index)
        {
            this.index = index;
        }

        public override void execute()
        {
            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PromoterOverRoomListCommand(index), callback);
        }

        public void callback(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            PlayerInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerInfoPanel>();
            panel.disabelCurrActiveButton(PlayerInfoPanel.PROMOTEROVER);
            if (obj == null) return;

            ByteBuffer data = (ByteBuffer)obj;


//            string ip = data.readUTF();
//            int size = data.readInt();
            data.readUTF();
            data.readInt();

            int len = data.readInt();
            ArrayList<RoomEntry> list = new ArrayList<RoomEntry>();
            for (int i = 0; i < len; i++)
            {
                RoomEntry room = new RoomEntry();
                room.bytesRead(data);
                list.add(room);
            }

            panel.addOpenRoomList(list);
            panel.setVisible(true);

        }
    }
}
