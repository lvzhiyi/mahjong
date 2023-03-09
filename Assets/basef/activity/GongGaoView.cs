using cambrian.game;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.activity
{
    /// <summary> 公告 </summary>
    public class GongGaoView : UnrealLuaSpaceObject
    {
        private NoticeBoard notice;

        private ScrollImageContainder container;

        protected override void xinit()
        {
            this.container = GetComponent<ScrollImageContainder>();
            this.container.init();
        }

        public void setNotice(NoticeBoard notice)
        {
            this.notice = notice;
        }


        protected override void xrefresh()
        {
            if (IconManager.getHeadPic(notice.sid) != null)
            {
                this.container.setSprite(IconManager.getHeadPic(notice.sid));
                this.container.refresh();
            }
            else
            {
                Loader.getLoader().load<long,Sprite>(new Url(notice.getImg()),notice.sid,this.refreshIcon);
            }
        }

        void refreshIcon(long sids,Sprite icon)
        {
            if (sids == notice.sid)
            {
                // IconManager.saveHeadPic(notice.sid, icon);
                this.container.setSprite(icon);
                this.container.refresh();
            }
        }
    }
}
