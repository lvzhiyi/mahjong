namespace platform.poker
{
    public class PPDKTenReplayRoomPanel : PDKTenRoomPanel
    {
        private PDKTenReplay replay;

        public PDKTenReplayControlView rcview;

        private UnrealCheckBox switchbox;

        protected override void xinit()
        {
            base.xinit();

            rcview = content.Find("replay").GetComponent<PDKTenReplayControlView>();
            rcview.init();

            switchbox = content.Find("switch").GetComponent<UnrealCheckBox>();
            switchbox.init();
        }

        public void setRePlay(PDKTenReplay replay)
        {
            this.replay = replay;
        }

        public PDKTenReplay getRePlay()
        {
            return replay;
        }

        public void doReplay()
        {
            if (PDKTenMatch.match == null) return;
            replay.doOrder();
        }

        public void controlReplay(int type)
        {
            rcview.controlReplay(type, replay);
        }

        protected override void xrefresh()
        {
            switchbox.setState(UnrealCheckObject.NORMAL);
            rcview.setVisible(true);
            headView.refresh();
            topView.refresh();
        }
    }
}
