using cambrian.common;
using platform.spotred;

namespace basef.arena
{
    public class GetArenaSingleRoomProcess:MouseClickProcess
    {
        private ArenaDeskBar bar;
        public override void execute()
        {
            bar = this.transform.parent.parent.GetComponent<ArenaDeskBar>();
            CommandManager.addCommand(new GetArenaRoomSingleInfoCommand(bar.room.roomid, bar.room.createtime), showPlayer);
        }
        void showPlayer(object obj)
        {
            if (obj == null) return;
            MatchPlayer[] players = (MatchPlayer[])obj;
            ArenaSingleRoomInfoPanel panel = UnrealRoot.root.getDisplayObject<ArenaSingleRoomInfoPanel>();
            panel.setDatas(bar.room,players);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
