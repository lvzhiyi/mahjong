using basef.share;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 分享房间信息到钉钉
    /// </summary>
    public class ShareRoomToDDProcess:MouseClickProcess
    {
        public override void execute()
        {
            ShareManager.getInstance().shareRoomToDD();
            this.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
