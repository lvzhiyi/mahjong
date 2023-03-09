using cambrian.common;
using platform.spotred.playback;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 自己的视图
    /// </summary>
    public class SelfView : BaseHandView
    {
        /// <summary>
        /// 手牌区
        /// </summary>
        HandView handview;
        /// <summary>
        /// 回放界面手牌区
        /// </summary>
        [HideInInspector]  public ReplayHandView replayHandView;
        /// <summary>
        /// 碰,吃,偷牌区
        /// </summary>
        [HideInInspector] public FixedView fixedView;
        /// <summary>
        /// 出牌区
        /// </summary>
        [HideInInspector] public DisableView disableView;
        private Transform huadong;
        /// <summary>
        /// 手指1
        /// </summary>
        private Image hua_1;

        /// <summary>
        /// 出牌线
        /// </summary>
        private Image line;
        /// <summary>
        /// 滑动即可出牌
        /// </summary>
        private Image huatext;


        protected override void xinit()
        {
            base.xinit();
           
            if (transform.Find("hand") != null)
            {
                this.handview = transform.Find("hand").GetComponent<HandView>();
                this.handview.init();
            }

            if (transform.Find("rhand") != null)
            {
                this.replayHandView = transform.Find("rhand").GetComponent<ReplayHandView>();
                this.replayHandView.init();
            }

            this.fixedView = transform.Find("fixed").GetComponent<FixedView>();
            this.fixedView.init();
            this.disableView = transform.Find("disable").GetComponent<DisableView>();
            this.disableView.init();
           

            if (transform.Find("line") != null)
                this.line = transform.Find("line").GetComponent<Image>();

            if (transform.Find("huadong") != null)
            {
                this.huadong = transform.Find("huadong");
                this.hua_1 = transform.Find("huadong").Find("hua_2").GetComponent<Image>();
                this.huatext = transform.Find("huadong").Find("hua_1").GetComponent<Image>();
            }
        }

        public ReplayHandView getReplayHandView()
        {
            return this.replayHandView;
        }

        public HandView getHandView()
        {
            return this.handview;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            if (this.handview != null)
                this.handview.refresh();
            if (this.replayHandView != null)
                this.replayHandView.refresh();
            this.fixedView.refresh();
            this.disableView.refresh();
            if (this.huadong != null)
                this.huadong.gameObject.SetActive(false);

            if (this.line != null)
                this.line.gameObject.SetActive(false);
        }


        public void setLineStyle(Sprite sprite)
        {
            if (this.line != null)
                this.line.sprite = sprite;
        }

        public void showHuaDong()
        {
            if (this.huadong != null)
            {
                this.huadong.gameObject.SetActive(true);
                this.hua_1.gameObject.SetActive(true);
                this.line.gameObject.SetActive(true);
                DisplayKit.setLocalRoateXYZ(this.hua_1.transform,0,0, 60);
                refreshHuaDong();
            }
        }

        private Tween hua_1rotate;

        private Tween huatext_scale;

        private Tween lineTween;

        public void refreshHuaDong()
        {
            if (hua_1rotate == null)
            {
                hua_1rotate=this.hua_1.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
                hua_1rotate.SetLoops(-1, LoopType.Yoyo);

                huatext_scale = this.huatext.transform.DOScale(new Vector3(0.6f, 0.6f, 1), 1);
                huatext_scale.SetLoops(-1, LoopType.Yoyo);

                lineTween = this.line.DOFade(0.1f,1);
                lineTween.SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                hua_1rotate.Restart();
                huatext_scale.Restart();
                lineTween.Restart();
            }
        }

        public void hideHuaDong()
        {
            if (this.huadong != null)
            {
                this.huadong.gameObject.SetActive(false);
                this.line.gameObject.SetActive(false);
            }
        }


    }
}
