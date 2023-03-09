using UnityEngine;

namespace basef.notice
{
    public class NewsListPanel: UnrealLuaPanel
    {
        [HideInInspector] public UnrealInputTextField text;

        [HideInInspector] public News[] notics;

        UnrealTableContainer container;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.maskedContainer.GetComponent<UnrealTableContainer>();
            this.container.init();
            this.text = this.container.transform.Find("info").Find("text").GetComponent<UnrealInputTextField>();
            this.text.init();
            this.resizeDelta();
        }
        public void setNotics(News[] notics)
        {
            this.notics = notics;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.container.resize(this.notics.Length);
            for (int i = 0; i < this.notics.Length; i++)
            {
                NewsBar bar = (NewsBar)this.container.bars[i];
                bar.Notice(this.notics[i]);
                bar.refresh();
            }
            this.xrelayout();
        }
    }
}
