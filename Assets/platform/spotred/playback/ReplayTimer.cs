using cambrian.common;
using UnityEngine;

namespace platform.spotred.playback
{
    public class ReplayTimer:MonoBehaviour
    {
        public static int SLOWER = 1;
        /// <summary>
        /// 暂停
        /// </summary>
        public static int PAUSE = 2;
        /// <summary>
        /// 快进
        /// </summary>
        public static int QUICKEN = 3;
        /// <summary>
        /// 停止
        /// </summary>
        public static int STOP = 4;
        /// <summary>
        /// 后退
        /// </summary>
        public static int FALLBACK = 5;

        public static float BASE_SPEED = 2000;

        float space = 2000;

        bool isPause = false;
        long lasttime;

        public void slower()
        {
            if (this.space >= 2000)
                return;
            this.space = this.space + 300;
        }

        public void pause()
        {
            this.isPause = !this.isPause;
        }

        public void pause(bool b)
        {
            this.isPause = b;
        }

        public void quicken()
        {
            if (this.space <= 300) return;
            this.space = this.space - 300;
        }

        public void stop()
        {
            this.space = BASE_SPEED;
            this.isPause = false;
        }
        /// <summary>
        /// 获取暂停状态
        /// </summary>
        /// <returns></returns>
        public bool getPause()
        {
            return this.isPause;
        }
       

        void Update()
        {
            ReplaySpotRoomPanel panel = UnrealRoot.root.getDisplayObject<ReplaySpotRoomPanel>();
            panel.getRePlay().execute();
            if (this.isPause) return;
            if (TimeKit.currentTimeMillis - this.lasttime < this.space) return;

            this.lasttime = TimeKit.currentTimeMillis;
            
            panel.doReplay();
        }
    }
}
