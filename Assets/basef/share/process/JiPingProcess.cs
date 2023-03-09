using cambrian.game;

namespace basef.share
{
    public class JiPingProcess:MouseClickProcess
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
