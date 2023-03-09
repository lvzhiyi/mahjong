using scene.game;
using basef.prop;
using UnityEngine.UI;

namespace basef.bag
{
    public class BagBar : UnrealLuaSpaceObject
    {
        /// <summary> 道具图标 </summary>
        Image img;

        /// <summary> 数量 </summary>
        Text num;

        /// <summary> 选择图标是否显示 </summary>
        Image selectImg;

        /// <summary> 道具 </summary>
        public Prop prop;

        protected override void xinit()
        {
            this.img = this.transform.Find("img").GetComponent<Image>();
            this.num = this.transform.Find("num").GetComponent<Text>();
            this.selectImg = this.transform.Find("selected").GetComponent<Image>();
        }

        public void setProp(Prop prop)
        {
            this.prop = prop;
        }

        protected override void xrefresh()
        {
            this.img.gameObject.SetActive(false);
            this.num.gameObject.SetActive(false);
            if (prop != null)
            {
                this.img.sprite = CacheManager.shopimg[prop.img];
                this.img.gameObject.SetActive(true);
                this.num.text = prop.count + "";
                this.num.gameObject.SetActive(true);
            }

        }

        public void select(bool b)
        {
            this.selectImg.gameObject.SetActive(b);
        }
    }
}
