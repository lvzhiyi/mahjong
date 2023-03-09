
using basef.share;

namespace platform.spotred.waitroom
{
    public class ShareRoomLineToWeiXinProcess:MouseClickProcess
    {
        public override void execute()
        {
            ShareManager.getInstance().shareRoom(Room.room, ShareManager.WEIXIN);
            this.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
