using scene.game;
using basef.prop;
using UnityEngine;
using UnityEngine.UI;

namespace basef.bag
{
    public class PropInfoView : UnrealLuaSpaceObject
    {
        /// <summary> 图标 </summary>
        Image propicon;
  
        /// <summary> 名称 </summary>
        Text propname;

        /// <summary> 数量 </summary>
        Text propnum;

        /// <summary> 描述 </summary>
        Text info;

        /// <summary> 使用 </summary>
        UnrealTextScaleButton usebtn;
  
        /// <summary> 出售 </summary>
        UnrealTextScaleButton salebtn;

        /// <summary> 道具背景 </summary>
        Image prop_bg;

        public Prop prop;

        private float usebtnWidth;

        private float salebtnWidth;

        protected override void xinit()
        {
            this.propicon = this.transform.Find("img").GetComponent<Image>();
            this.propname = this.transform.Find("name").GetComponent<Text>();
            this.propnum = this.transform.Find("num").GetComponent<Text>();
            this.info = this.transform.Find("info").GetComponent<Text>();
            this.usebtn = this.transform.Find("use").GetComponent<UnrealTextScaleButton>();
            usebtnWidth = this.usebtn.transform.GetComponent<RectTransform>().rect.width / 2;
            this.salebtn = this.transform.Find("sale").GetComponent<UnrealTextScaleButton>();
            salebtnWidth = this.salebtn.transform.GetComponent<RectTransform>().rect.width / 2;
            this.prop_bg = this.transform.Find("bg").GetComponent<Image>();
        }

        public void setProp(Prop prop)
        {
            this.prop = prop;
        }

        protected override void xrefresh()
        {
            this.prop_bg.gameObject.SetActive(false);
            if (prop != null) this.prop_bg.gameObject.SetActive(true);
            this.propicon.sprite = CacheManager.shopimg[prop.img];
            this.propname.text = prop.name;
            this.propnum.text = prop.count + "";
            this.info.text = prop.getDescription();

            this.usebtn.setVisible(prop.isFunctional());
            this.salebtn.setVisible(!prop.isUnrecyclable());
            //            if (this.usebtn.getVisible() && this.salebtn.getVisible())
            //            {
            //                DisplayKit.setLocalX(this.usebtn,-salebtnWidth);
            //                DisplayKit.setLocalX(this.salebtn,salebtnWidth);
            //            }
            //            else
            //            {
            //                DisplayKit.setLocalX(this.usebtn, 0);
            //                DisplayKit.setLocalX(this.salebtn, 0);
            //            }
        }
    }
}
