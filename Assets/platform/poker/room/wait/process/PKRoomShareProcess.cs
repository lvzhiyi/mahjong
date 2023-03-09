using basef.share;

namespace platform.poker
{
    /// <summary> 分享界面 </summary>
    public class PKRoomShareProcess : MouseClickProcess
    {
        public override void execute()
        {
            ShareManager.getInstance().shareRoom(Room.room,ShareManager.WEIXIN);
        }
    }
}
