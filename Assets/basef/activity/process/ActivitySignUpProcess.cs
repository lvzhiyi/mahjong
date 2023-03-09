using basef.share;

namespace basef.activity
{
    /// <summary> 活动报名 </summary>
    public class ActivitySignUpProcess:MouseClickProcess
    {
        private bool b = false;

        public override void execute()
        {
            b = true;
        }
        
        public void onCallBack(byte[] obj)
        {
            ShareManager.getInstance().sharePicToFriend(obj);
        }

        void OnGUI()
        {
            if (b)
            {
                b = false;
                CaptureScreenshot.captures(onCallBack);
            }
        }
    }
}
