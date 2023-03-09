namespace platform.poker
{
    public class DDZReplayControlView : UnrealLuaSpaceObject
    {
        public PDDZReplayTimer timer { get; private set; }

        protected override void xinit()
        {
            base.xinit();
            timer = transform.parent.parent.GetComponent<PDDZReplayTimer>();
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

        public void controlReplay(int type, DDZReplay replay)
        {
            switch (type)
            {
                case PDDZReplayTimer.SLOWER:
                    timer.slower();
                    break;
                case PDDZReplayTimer.PAUSE:
                    timer.pause();
                    break;
                case PDDZReplayTimer.QUICKEN:
                    timer.quicken();
                    break;
                case PDDZReplayTimer.STOP:
                    timer.stop();
                    break;
                case PDDZReplayTimer.FALLBACK:
                    replay.fallback();
                    break;
                default: break;
            }
        }
    }
}
