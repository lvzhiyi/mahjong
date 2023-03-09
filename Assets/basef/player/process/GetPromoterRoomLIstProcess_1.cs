using cambrian;
using cambrian.common;
using scene.game;

namespace basef.player
{
    public class GetPromoterRoomLIstProcess_1:MouseClickProcess
    {
        public override void execute()
        {
            if (this.getRoot<PlayerInfoPanel>().promoteButton.state == UnrealButton.DISABLE) return;
            SpotRedRoot.dataLoading.refresh();
            CommandManager.addCommand(new PromoterRoomListCommand(), callback);
        }

        public void callback(object obj)
        {
            SpotRedRoot.dataLoading.hidden();

            if (obj == null) return;

            ByteBuffer data = (ByteBuffer)obj;

            string ip = data.readUTF();
            int len = data.readInt();
            ArrayList<RoomEntry> list = new ArrayList<RoomEntry>();
            for (int i = 0; i < len; i++)
            {
                RoomEntry room = new RoomEntry();
                room.bytesRead(data);
                list.add(room);
            }

            PlayerInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerInfoPanel>();
            panel.disabelCurrActiveButton(PlayerInfoPanel.PROMOTER);

            panel.setData(list, ip,false);
            panel.refresh();

            panel.setVisible(true);

        }
    }
}
