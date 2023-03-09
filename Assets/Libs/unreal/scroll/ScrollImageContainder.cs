using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace cambrian.unreal.scroll
{
    /// <summary>
    /// 图片滚动容器
    /// </summary>
    [Hotfix]
    public class ScrollImageContainder : UnrealLuaSpaceObject
    {

        private Image bg;

        private Sprite sprite;

        ScrollRect scroll;

        protected override void xinit()
        {
            this.scroll = this.transform.Find("scrollrect").GetComponent<ScrollRect>();
            this.bg = this.scroll.transform.Find("bg").GetComponent<Image>();
           
        }

        public void setSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        protected override void xrefresh()
        {
            if(sprite!=null)
            {
                this.bg.sprite = sprite;
                this.bg.SetNativeSize();
                if (this.scroll.vertical)
                    this.scroll.verticalNormalizedPosition = 1;
                else
                    this.scroll.horizontalNormalizedPosition = 1;
            }
        }
    }
}
