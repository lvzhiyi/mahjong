using cambrian.common;
using scene.game;
using basef.prop;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.bag
{
    public class PropUsePanel:UnrealLuaPanel
    {
        [HideInInspector] public SliderComponent slider;

        [HideInInspector] public Prop prop;

        Image img;

        Text propname;

        Text num;

        private SpritesList title;

        private Text salePrice;

        [HideInInspector] public UseType type = UseType.SALE;

        /// <summary> 使用类型 </summary>
        public enum UseType
        {
            USE,//使用
            SALE,//出售
            EXCHANGE//兑换
        }

        protected override void xinit()
        {
            base.xinit();
            this.slider = this.content.Find("content").Find("select").GetComponent<SliderComponent>();
            this.slider.init();
            this.img = this.content.Find("content").Find("img").GetComponent<Image>();
            this.propname = this.content.Find("content").Find("name").GetComponent<Text>();
            this.num = this.content.Find("content").Find("num").GetComponent<Text>();
            this.title = this.content.Find("title").GetComponent<SpritesList>();
            this.salePrice = this.content.Find("saleprice").GetComponent<Text>();
        }

        public void setProp(Prop prop,UseType type)
        {
            this.prop = prop;
            this.type = type;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            this.img.sprite = CacheManager.shopimg[prop.img];
            this.propname.text = prop.name;
            this.num.text = prop.count+"";
            this.slider.setValue(1, prop.count);
            
            if (type == UseType.USE)
            {
                this.title.ShowIndex(0,true);
                this.salePrice.gameObject.SetActive(false);
            }
            else if (type == UseType.EXCHANGE)
            {
                this.title.ShowIndex(1, true);
                this.salePrice.text = "将获得金豆:" + StringKit.toStringMoney(prop.salePrice);
                this.slider.setAction(callback);
            }
            else
            {
                this.title.ShowIndex(2, true);
                this.salePrice.gameObject.SetActive(true);
                this.salePrice.text = "将获得金豆:"+StringKit.toStringMoney(prop.salePrice);
                this.slider.setAction(callback);
            }

        }

        public void callback(long value)
        {
            this.salePrice.text = "将获得金豆:" + StringKit.toStringMoney(prop.salePrice*value);
        }
    }
}
