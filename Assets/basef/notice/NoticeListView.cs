using cambrian.common;
using UnityEngine;
using UnityEngine.UI;

namespace basef.notice
{
    public class NoticeListView : UnrealLuaSpaceObject
    {
        [HideInInspector]
        public ArrayList<ScrollNotice> notics;

        UnrealTableContainer container;

        Image bg;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.bg = this.transform.Find("bg").GetComponent<Image>();
        }
        public void setNotics(ArrayList<ScrollNotice> notics)
        {
            this.notics = notics;
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            this.container.cols = this.notics.count;
            this.container.resize(this.notics.count);
            if (this.notics.count == 0)
            {
                this.bg.gameObject.SetActive(false);
                return;
            }
            this.bg.gameObject.SetActive(true);
            for (int i = 0; i < this.notics.count; i++)
            {
                NoticsBar bar = (NoticsBar) this.container.bars[i];
                bar.setNotice(this.notics.get(i));
                bar.refresh();
            }

            this.container.resizeAutoDelta();
            this.container.relayoutAuto();

            this.GetComponent<NoticePositionTweener>().setMinX();
        }
    }
}
