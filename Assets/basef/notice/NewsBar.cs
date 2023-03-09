using cambrian.unreal.display;
using UnityEngine;

namespace basef.notice
{
    public class NewsBar: UnrealLuaSpaceObject
    {
        [HideInInspector]
        public News News;

        UnrealTextField text;

        UnrealTextField time;

        UnrealTextField title;

        protected override void xinit()
        {
            base.xinit();
            this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
            this.time = this.transform.Find("time").GetComponent<UnrealTextField>();
            this.title = this.transform.Find("title").GetComponent<UnrealTextField>();
        }

        public void Notice(News News)
        {
            this.News = News;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.text.text = this.News.getInfo();
            this.time.text = this.News.getTime();
            this.title.text = this.News.getTitle();
        }
    }
}
