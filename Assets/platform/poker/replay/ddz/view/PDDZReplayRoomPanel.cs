namespace platform.poker
{
    public class PDDZReplayRoomPanel : PDDZRoomPanel
    {
        private DDZReplay replay;

        public DDZReplayControlView rcview;

        private UnrealCheckBox switchbox;

        protected override void xinit()
        {
            base.xinit();

            rcview = content.Find("replay").GetComponent<DDZReplayControlView>();
            rcview.init();

            switchbox = content.Find("switch").GetComponent<UnrealCheckBox>();
            switchbox.init();
        }

        public void setRePlay(DDZReplay replay)
        {
            this.replay = replay;
        }

        public DDZReplay getRePlay()
        {
            return replay;
        }

        public void doReplay()
        {
            if (DDZMatch.match == null) return;
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
