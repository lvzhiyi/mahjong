namespace platform.poker
{
    public class PDKTenReplayControlView : UnrealLuaSpaceObject
    {
        public PPDKTenReplayTimer timer { get; private set; }

        protected override void xinit()
        {
            base.xinit();
            timer = transform.parent.parent.GetComponent<PPDKTenReplayTimer>();
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

        public void controlReplay(int type, PDKTenReplay replay)
        {
            switch (type)
            {
                case PPDKReplayTimer.SLOWER:
                    timer.slower();
                    break;
                case PPDKReplayTimer.PAUSE:
                    timer.pause();
                    break;
                case PPDKReplayTimer.QUICKEN:
                    timer.quicken();
                    break;
                case PPDKReplayTimer.STOP:
                    timer.stop();
                    break;
                case PPDKReplayTimer.FALLBACK:
                    replay.fallback();
                    break;
                default: break;
            }
        }
    }
}
