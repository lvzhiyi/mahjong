using cambrian.common;
using UnityEngine.UI;

namespace basef.activity
{
    public class NoticeBoardPanel : UnrealLuaPanel
    {

        ArrayList<NoticeBoard> noticeList;

        UnrealTableContainer container;

        public GongGaoView gongGaoView;

        public Image noNotice;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.content.Find("content").Find("left").Find("mask").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.gongGaoView = this.content.Find("content").Find("gonggao").GetComponent<GongGaoView>();
            this.gongGaoView.init();
            this.noNotice = this.content.Find("content").Find("nonotice").GetComponent<Image>();

        }

        public void setData(ArrayList<NoticeBoard> noticeList)
        {
            this.noticeList = noticeList;
        }

        protected override void xrefresh()
        {
            this.gongGaoView.setVisible(false);

            if (noticeList.count > 0)
            {
                this.container.resize(this.noticeList.count);

                for (int i = 0; i < noticeList.count; i++)
                {
                    NoticeBoardBar bar = (NoticeBoardBar)this.container.bars[i];
                    bar.setNotice(noticeList.get(i));
                    bar.index_space = i;
                    bar.refresh();
                    bar.check(i == 0);
                }

                this.gongGaoView.setNotice(noticeList.get(0));
                this.gongGaoView.refresh();
                this.gongGaoView.setVisible(true);
                this.noNotice.gameObject.SetActive(false);
            }
            else
            {
                this.container.resize(0);
                this.noNotice.gameObject.SetActive(true);
            }

            this.container.resizeDelta();
        }

        /// <summary> 改变左边容器bar其他的checkbox状态 </summary>
        public void checkContainerBar(int index)
        {
            for (int i = 0; i < this.container.count; i++)
            {
                NoticeBoardBar bar = (NoticeBoardBar)this.container.bars[i];
                bar.check(i == index);
            }
        }
    }
}
