using UnityEngine;

namespace platform.mahjong
{
    public class MJReplayControlView:UnrealLuaSpaceObject
    {
        [HideInInspector] public MJReplayTimer timer;

        protected override void xinit()
        {
            base.xinit();
            this.timer = this.transform.parent.parent.GetComponent<MJReplayTimer>();
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

        public void controlReplay(int type, MJReplay replay)
        {
            if (type == MJReplayTimer.SLOWER)
            {
                timer.slower();
            }
            else if (type == MJReplayTimer.PAUSE)
            {
                timer.pause();
            }
            else if (type == MJReplayTimer.QUICKEN)
            {
                timer.quicken();
            }
            else if (type == MJReplayTimer.STOP)
            {
                timer.stop();
            }
            else if (type == MJReplayTimer.FALLBACK)
            {
                replay.fallback();
            }
        }
    }
}
