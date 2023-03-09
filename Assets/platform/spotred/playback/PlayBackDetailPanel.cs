using cambrian.unreal.scroll;

namespace platform.spotred.playback
{
    public class PlayBackDetailPanel: UnrealViewPanel
    {
        ScrollContainer playbackinfo;

        private Record record;
        protected override void xinit()
        {
            base.xinit();
            this.playbackinfo = this.transform.Find("Canvas").Find("content").Find("container").GetComponent<ScrollContainer>();
            this.playbackinfo.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        /// <summary>
        /// 显示指定牌局
        /// </summary>
        public void showPlaybackInfo(Record record)
        {
            this.record = record;
            int num = this.record.replayids.Length;
            this.playbackinfo.updateData(updatePlayBackInfoData);
            this.playbackinfo.resize(num);
        }

        public void updatePlayBackInfoData(ScrollBar bar, int i)
        {
            ((ReplayBar)bar).setInfo(i, record.players, record.scores[i], record.replayids[i]);
            bar.refresh();
        }
    }
}
