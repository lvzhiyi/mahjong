using cambrian.common;

namespace basef.player
{
    public class DisbindPromoterRoomProcess:MouseClickProcess
    {
        public override void execute()
        {
            OpenRoomListBar bar=this.transform.parent.GetComponent<OpenRoomListBar>();

            CommandManager.addCommand(new DisBindPromoterRoomCommand(bar.roomEntry),callback);
        }

        public void callback(object obj)
        {
            if (obj == null) return;
            bool b = (bool)obj;

            if (b)
            {
                CommandManager.addCommand(new PromoterRoomListCommand(),callback_1);
            }
        }

        public void callback_1(object obj)
        {
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
