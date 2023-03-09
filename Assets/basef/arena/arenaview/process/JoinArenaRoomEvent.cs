using cambrian.common;
using platform;

namespace basef.arena
{
    public class JoinArenaRoomEvent:MouseClickProcess
    {
        public override void execute()
        {
            ArenaDeskBar bar = this.transform.parent.parent.GetComponent<ArenaDeskBar>();

            CommandManager.addCommand(new JoinArenaRoomCommand(bar.room.roomid),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            new ShowWaiteRoomCallBackProcess().execute();
        }
    }
}
