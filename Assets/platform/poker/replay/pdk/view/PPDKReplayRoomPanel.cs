namespace platform.poker
{
    public class PPDKReplayRoomPanel : PPDKRoomPanel
    {
        private PDKReplay replay;

        public PDKReplayControlView rcview;

        private UnrealCheckBox switchbox;

        private UnrealTextField speed;

        private UnrealTextField schedule;

        protected override void xinit()
        {
            base.xinit();

            rcview = content.Find("replay").GetComponent<PDKReplayControlView>();
            rcview.init();

            switchbox = content.Find("switch").GetComponent<UnrealCheckBox>();
            switchbox.init();

            speed = downView.transform.Find("speed").GetComponent<UnrealTextField>();
            speed.init();

            schedule = downView.transform.Find("schedule").GetComponent<UnrealTextField>();
            schedule.init();
        }

        public void setRePlay(PDKReplay replay)
        {
            this.replay = replay;
        }

        public PDKReplay getRePlay()
        {
            return replay;
        }

        public void doReplay()
        {
            if (PDKMatch.match == null) return;
            replay.doOrder();
            refreshInfo();
        }

        public void controlReplay(int type)
        {
            rcview.controlReplay(type, replay);
            refreshInfo();
        }

        protected override void xrefresh()
        {
            switchbox.setState(UnrealCheckObject.NORMAL);
            rcview.setVisible(true);
            headView.refresh();
            topView.refresh();
            refreshInfo();
        }

        private void refreshInfo()
        {
            schedule.text = string.Concat(replay.index, '/', replay.list.count);
            speed.text = string.Concat(rcview.timer.space);
        }
    }
}
