namespace platform.poker
{
    public class PPDKAnYueReplayRoomPanel : PDKAnYueRoomPanel
    {
        private PDKAnYueReplay replay;

        public PDKAnYueReplayControlView rcview;

        private UnrealCheckBox switchbox;

        protected override void xinit()
        {
            base.xinit();

            rcview = content.Find("replay").GetComponent<PDKAnYueReplayControlView>();
            rcview.init();

            switchbox = content.Find("switch").GetComponent<UnrealCheckBox>();
            switchbox.init();
        }

        public void setRePlay(PDKAnYueReplay replay)
        {
            this.replay = replay;
        }

        public PDKAnYueReplay getRePlay()
        {
            return replay;
        }

        public void doReplay()
        {
            if (PDKAnYueMatch.match == null) return;
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
