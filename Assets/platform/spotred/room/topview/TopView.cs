using platform.spotred.playback;
using UnityEngine;

namespace platform.spotred.room
{
    public class TopView : BaseHandView
    {
        /// <summary>
        /// 回放界面手牌区
        /// </summary>
        [HideInInspector] public ReplayHandView replayHandView;

        /// <summary>
        /// 固定牌区
        /// </summary>
        public TFixedView fixedView;

        /// <summary>
        /// 弃牌区
        /// </summary>
        public TDisableView disableView;

        protected override void xinit()
        {
            base.xinit();
            if (transform.Find("rhand") != null)
            {
                this.replayHandView = transform.Find("rhand").GetComponent<ReplayHandView>();
                this.replayHandView.init();
            }

            this.fixedView = transform.Find("fixed").GetComponent<TFixedView>();
            this.fixedView.init();
            this.disableView = transform.parent.Find("disable").GetComponent<TDisableView>();
            this.disableView.init();
        }


        public ReplayHandView getReplayHandView()
        {
            return this.replayHandView;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (this.replayHandView != null)
                this.replayHandView.refresh();

            this.fixedView.refresh();
            this.disableView.refresh();
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            disableView.setVisible(b);
        }
    }
}
