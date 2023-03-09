using cambrian.common;

namespace basef.arena
{
    public class DisbandArenaRoomProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaRoom room = getRoot<ArenaSingleRoomInfoPanel>().arenaRoom;
            CommandManager.addCommand(new DisbandArenaRoomCommand(Arena.arena.getId(), room.roomid, room.createtime), back);
        }
        void back(object obj)
        {
            if (obj == null) return;
            Alert.autoShow("解散成功!");
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.deskView.refresh();
            this.root.setCVisible(false);
        }
    }
}
