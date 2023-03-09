using basef.joinroom;
using cambrian.common;
using cambrian.game;
using platform;
using platform.spotred;
using platform.spotred.waitroom;
using System;

namespace basef.share
{
    public class RecvShareRoomProcess: Process
    {
        int roomid;
        public void setRoomid(int roomid)
        {
            this.roomid = roomid;
        }
        public override void execute()
        {
            base.execute();
            if (this.roomid >= 100000)
            {
                Action<object> onCommand = s =>
                {
                    Room room = (Room)s;
                    if (room == null)
                    {
                        Alert.show("房号[" + roomid + "]的房间人数已满或房主已解散。");
                        return;
                    }

                    SpotWaitRoomPanel panel = UnrealRoot.root.getDisplayObject<SpotWaitRoomPanel>();
                    panel.refresh();
                    panel.setVisible(true);
                    WXManager.getInstance().getGPSLocation("Root", "call_back_gps");
                };
                CommandManager.addCommand(new AddRoomCommand(this.roomid), onCommand);
                
            }
        }
    }
}
