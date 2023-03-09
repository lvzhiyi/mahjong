using basef.joinroom;
using cambrian.common;
using cambrian.game;
using platform;

namespace basef.lobby
{
    public class AddRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            SpotJoinRoomPanel jrPanel = UnrealRoot.root.getDisplayObject<SpotJoinRoomPanel>();
            jrPanel.setCallBack(setValue);
            jrPanel.setCVisible(true);
        }

        void setValue(long value)
        {
            WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
            CommandManager.addCommand(new AddRoomCommand((int) value), onCommand);
        }

        public void onCommand(object s)
        {
            Room room = (Room) s;
            if (room == null) return;
            new ShowWaiteRoomCallBackProcess().execute();
        }
    }
}
