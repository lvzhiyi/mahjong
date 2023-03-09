using cambrian;
using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.player
{
    public class OpenPlayerInfoPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            if (Platform.isOpenGuest()) return;
            PlayerSimpleInfoPanel simpleInfoPanel = UnrealRoot.root.getDisplayObject<PlayerSimpleInfoPanel>();
            simpleInfoPanel.refresh();
            simpleInfoPanel.setCVisible(true);

            //SpotRedRoot.dataLoading.refresh();
            //CommandManager.addCommand(new PromoterRoomListCommand(), callback);
        }

        //public void callback(object obj)
        //{
        //    SpotRedRoot.dataLoading.hidden();

        //    if (obj == null) return;

        //    ByteBuffer data = (ByteBuffer)obj;

        //    string ip = data.readUTF();
        //    int len = data.readInt();
        //    ArrayList<RoomEntry> list = new ArrayList<RoomEntry>();
        //    for (int i = 0; i < len; i++)
        //    {
        //        RoomEntry room = new RoomEntry();
        //        room.bytesRead(data);
        //        list.add(room);
        //    }


        //    if (PlayerToken.instance.isPromoter())
        //    {
        //        PlayerInfoPanel panel = UnrealRoot.root.getDisplayObject<PlayerInfoPanel>();
        //        panel.disabelCurrActiveButton(PlayerInfoPanel.PROMOTER);

        //        panel.setData(list, ip, false);
        //        panel.refresh();

        //        panel.setCVisible(true);
        //    }
        //    else
        //    {
        //        PlayerSimpleInfoPanel simpleInfoPanel = UnrealRoot.root.getDisplayObject<PlayerSimpleInfoPanel>();
        //        simpleInfoPanel.setIp(ip);
        //        simpleInfoPanel.refresh();
        //        simpleInfoPanel.setCVisible(true);
        //    }
        //}
    }
}
