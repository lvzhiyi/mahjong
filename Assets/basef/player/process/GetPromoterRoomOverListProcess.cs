using cambrian;
using cambrian.common;
using scene.game;

namespace basef.player
{
    public class GetPromoterRoomOverListProcess:MouseClickProcess
    {
        public override void execute()
        {
            if (this.getRoot<PlayerInfoPanel>().promoteOverButton.state == UnrealButton.DISABLE) return;
            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PromoterOverRoomListCommand(0), callback);
        }

        public void callback(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            PlayerInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerInfoPanel>();
            panel.disabelCurrActiveButton(PlayerInfoPanel.PROMOTEROVER);
            if (obj == null) return;

            ByteBuffer data = (ByteBuffer)obj;


            string ip = data.readUTF();
            int size = data.readInt();
            int len = data.readInt();
            ArrayList<RoomEntry> list = new ArrayList<RoomEntry>();
            for (int i = 0; i < len; i++)
            {
                RoomEntry room = new RoomEntry();
                room.bytesRead(data);
                list.add(room);
            }

            panel.room_count = size;
            panel.setData(list, ip,true);
            panel.setIndex(0);
            panel.refresh();

            panel.setVisible(true);

        }
    }
}
