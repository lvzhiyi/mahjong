using cambrian.common;
using platform.spotred.waitroom;

namespace platform
{
    /// <summary>
    /// 投票解散（解散房间）
    /// </summary>
    public class VotingDisbandProcess:MouseClickProcess
    {
        public bool isAgree;

        public int type;

        public override void execute()
        {
            CommandManager.addCommand(new RoomDisbandCommand(isAgree,type,Room.room.getRoomIndex()));
        }
    }
}
