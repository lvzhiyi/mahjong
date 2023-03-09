using platform.spotred.playback;
using UnityEngine;

namespace platform.spotred.room
{
    public class LeftView: BaseHandView
    {
        /// <summary>
        /// 回放界面手牌区
        /// </summary>
        [HideInInspector] public ReplayHandView replayHandView;

        public LDisableView disableView;

        public LFixedView fixedView;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("rhand") != null)
            {
                this.replayHandView = transform.Find("rhand").GetComponent<ReplayHandView>();
                this.replayHandView.init();
            }

            this.disableView = this.transform.Find("disable").GetComponent<LDisableView>();
            this.disableView.init();

            this.fixedView = this.transform.Find("fixed").GetComponent<LFixedView>();
            this.fixedView.init();
        }
        public ReplayHandView getReplayHandView()
        {
            return this.replayHandView;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if(this.replayHandView!=null)
                this.replayHandView.refresh();

            this.disableView.refresh();
            this.fixedView.refresh();
        }

     
    }
}
