using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    public class PPDKReplayTimer : MonoBehaviour
    {
        /// <summary> 减速 </summary>
        public const int SLOWER = 1;

        /// <summary> 暂停 </summary>
        public const int PAUSE = 2;

        /// <summary> 快进 </summary>
        public const int QUICKEN = 3;

        /// <summary> 停止 </summary>
        public const int STOP = 4;

        /// <summary> 后退 </summary>
        public const int FALLBACK = 5;

        /// <summary> 播放速度 </summary>
        public static float BASE_SPEED = 2000;

        public float space { get; private set; }

        private bool isPause = false;

        private long lasttime;

        private void Awake()
        {
            space = BASE_SPEED;
        }

        public void slower()
        {
            if (space >= 2000)
                return;
            space += 300;
        }

        public void pause()
        {
            isPause = !isPause;
        }

        public void pause(bool b)
        {
            isPause = b;
        }

        public void quicken()
        {
            if (space <= 300) return;
            space -= 300;
        }

        public void stop()
        {
            space = BASE_SPEED;
            isPause = false;
        }

        public bool getPause()
        {
            return isPause;
        }

        private void Update()
        {
            var panel = UnrealRoot.root.getDisplayObject<PPDKReplayRoomPanel>();
            panel.getRePlay().execute();
            if (isPause) return;
            if ((TimeKit.currentTimeMillis - lasttime) < space) return;
            lasttime = TimeKit.currentTimeMillis;
            panel.doReplay();
        }
    }
}