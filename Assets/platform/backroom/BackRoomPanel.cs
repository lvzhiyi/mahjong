using cambrian.common;

namespace platform
{
    public class BackRoomPanel:UnrealLuaPanel
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        protected override void xupdate()
        {
            var room = Room.room;
            if (room != null)
            {
                if (room.isStatus(Room.STATE_DISBANDING) || room.isStatus(Room.STATE_DESTORY))
                {
                    setVisible(false);
                    return;
                }
                if (room.isStatus(Room.STATE_MATCH))
                {
                    CommandManager.addCommand(new RoomRfreshDataCommand());
                    setVisible(false);
                    return;
                }
            }
            else
            {
                setVisible(false);
            }

        }
    }
}
