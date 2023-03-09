using basef.prop;
using scene.game;
using UnityEngine.UI;
using UnityEngine;

namespace platform.spotred.room
{
    public class PlayerPropInfoBar:UnrealLuaSpaceObject
    {
        Image img;

        Text num;

        private Text nickname;

        [HideInInspector] public Prop prop;

        public bool imgisReset;

        public string prefix;

        protected override void xinit()
        {
            this.img = this.transform.Find("img").GetComponent<Image>();
            if (transform.Find("num") != null)
                this.num = this.transform.Find("num").GetComponent<Text>();
            if (transform.Find("name") != null)
                this.nickname = this.transform.Find("name").GetComponent<Text>();
        }

        public void setProp(Prop prop)
        {
            this.prop = prop;
        }

        protected override void xrefresh()
        {
            
            this.img.sprite = CacheManager.shopimg[prop.img];
            if(imgisReset)
                this.img.SetNativeSize();
            if (num != null)
                this.num.text = prefix+prop.count;
            if (nickname != null)
                this.nickname.text = prop.name;
        }
    }
}
