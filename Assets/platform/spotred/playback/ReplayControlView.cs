using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.playback
{
    public class ReplayControlView:UnrealLuaSpaceObject
    {
        [HideInInspector]public ReplayTimer timer;

        Image wpkc;

        protected override void xinit()
        {
            base.xinit();
          
            this.wpkc = this.transform.Find("wpkc").GetComponent<Image>();
            this.timer = this.transform.GetComponent<ReplayTimer>();
        }
        protected override void xrefresh()
        {
           this.wpkc.gameObject.SetActive(false);
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
            if(!b)
                this.wpkc.gameObject.SetActive(true);
        }

        public void controlReplay(int type, Replay replay)
        {
            if (type == ReplayTimer.SLOWER)
            {
                timer.slower();
            }
            else if (type == ReplayTimer.PAUSE)
            {
                timer.pause();
            }
            else if (type == ReplayTimer.QUICKEN)
            {
                timer.quicken();
            }
            else if (type == ReplayTimer.STOP)
            {
                timer.stop();
            }
            else if (type == ReplayTimer.FALLBACK)
            {
                replay.fallback();
            }
        }
    }
}
