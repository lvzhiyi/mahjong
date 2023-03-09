
using cambrian.game;
using basef.share;

namespace platform.spotred.over
{
    public class ShareIconToWeiXinProcess : MouseClickProcess
    {
        private bool b = false;

        public override void execute()
        {
            b = true;
        }

        //回调的数据
        public void onCallBack(byte[] obj)
        {
            ShareManager.getInstance().sharePic(obj);
            SoundManager.playButton();
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
