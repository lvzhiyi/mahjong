using platform.spotred.playback;
using UnityEngine;

namespace platform.spotred.room
{
    /// <summary>
    /// 右边玩家
    /// </summary>
    public class RightView: BaseHandView
    {
        /// <summary>
        /// 回放界面手牌区
        /// </summary>
        [HideInInspector] public ReplayHandView replayHandView;
        /// <summary>
        /// 固定牌区
        /// </summary>
        public RFixedView fixedView;
        /// <summary>
        /// 弃牌区
        /// </summary>
        public RDisableView disableView;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("rhand") != null)
            {
                this.replayHandView = transform.Find("rhand").GetComponent<ReplayHandView>();
                this.replayHandView.init();
            }

            this.fixedView = this.transform.Find("fixed").GetComponent<RFixedView>();
            this.fixedView.init();

            this.disableView = this.transform.Find("disable").GetComponent<RDisableView>();
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
     
    }
}
