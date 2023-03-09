using platform.mahjong;

namespace platform.spotred.over
{
    public class ShareIconToApplicationProcess:MouseClickProcess
    {
        /// <summary>
        /// 分享类型
        /// </summary>
        public int type;
        public override void execute()
        {
            this.transform.parent.gameObject.SetActive(false);
            AllOverPanel panel = UnrealRoot.root.checkDisplayObject<AllOverPanel>();
            if (panel != null)
                panel.startCaptures(type);
            else
            {
                UnrealRoot.root.getDisplayObject<MJAllOverPanel>().startCaptures(type);
            }
        }
    }
}
