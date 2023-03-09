namespace platform.poker
{
    public class SharePKIconToApplicationProcess : MouseClickProcess
    {
        /// <summary>
        /// 分享类型
        /// </summary>
        public int type;
        public override void execute()
        {
            this.transform.parent.gameObject.SetActive(false);
            PKAllOverPanel panel = UnrealRoot.root.checkDisplayObject<PKAllOverPanel>();
            panel.startCaptures(type);
        }
    }
}
