using UnityEngine;

namespace platform.mahjong
{
    public class AYMJReplayControlView : UnrealLuaSpaceObject
    {
        [HideInInspector] public AYMJReplayTimer timer;

        protected override void xinit()
        {
            base.xinit();
            this.timer = this.transform.parent.parent.GetComponent<AYMJReplayTimer>();
        }
        protected override void xrefresh()
        {
           
        }

        public void reset()
        {
            setVisible(false);
        }

        public void show()
        {
            setVisible(true);
        }

        public void replayOver(bool b)
        {
        }

        public void controlReplay(int type, AYMJReplay replay)
        {
            if (type == AYMJReplayTimer.SLOWER)
            {
                timer.slower();
            }
            else if (type == AYMJReplayTimer.PAUSE)
            {
                timer.pause();
            }
            else if (type == AYMJReplayTimer.QUICKEN)
            {
                timer.quicken();
            }
            else if (type == AYMJReplayTimer.STOP)
            {
                timer.stop();
            }
            else if (type == AYMJReplayTimer.FALLBACK)
            {
                replay.fallback();
            }
        }
    }
}
