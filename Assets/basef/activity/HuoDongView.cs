using cambrian.common;
using cambrian.game;
using cambrian.unreal.scroll;
using cambrian.uui;
using UnityEngine;

namespace basef.activity
{
    public class HuoDongView : UnrealLuaSpaceObject
    {
        private ScrollImageContainder scroll;

        private Activity activity;

        /// <summary> 报名 </summary>
        private UnrealScaleButton baoming;

        /// <summary> 排行查询 </summary>
        private UnrealScaleButton search;

        protected override void xinit()
        {
            this.baoming = this.transform.Find("baoming").GetComponent<UnrealScaleButton>();
            this.search = this.transform.Find("search").GetComponent<UnrealScaleButton>();
            this.scroll = GetComponent<ScrollImageContainder>();
            this.scroll.init();
        }

        public void setActivity(Activity activity)
        {
            this.activity = activity;
        }

        public Activity getActivity()
        {
            return this.activity;
        }

        protected override void xrefresh()
        {
            isOpenSearchRank(false);
            isOpenBaoming(true);
            long currtime = TimeKit.currentTimeMillis;
            if (currtime > activity.getOverTime() && currtime < activity.getRemoveTime())
            {
                isOpenSearchRank(true);
                isOpenBaoming(false);
            }
            if (currtime > activity.getRemoveTime() || currtime < activity.getReleaseTime())
                isOpenBaoming(false);

            if (IconManager.getHeadPic(activity.sid) != null)
            {
                this.scroll.setSprite(IconManager.getHeadPic(activity.sid));
                this.scroll.refresh();
            }
            else
                Loader.getLoader().load<long,Sprite>(new Url(activity.getImg()),activity.sid,this.refreshIcon);

        }

        /// <summary> 是否开启报名 </summary>
        public void isOpenBaoming(bool b)
        {
            this.baoming.setVisible(b);
        }

        public void isOpenSearchRank(bool b)
        {
            this.search.setVisible(b);
        }

        void refreshIcon(long sids,Sprite icon)
        {
            if (sids == activity.sid)
            {
                // IconManager.saveHeadPic(activity.sid, icon);
                this.scroll.setSprite(icon);
                this.scroll.refresh();
            }
        }
    }
}
