using basef.award;
using cambrian.game;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace basef.activity
{
    public class AwardBar : UnrealLuaSpaceObject
    {
        private Award award;

        private UnrealTextField ward_name;
        private UnrealTextField info;

        private Image icon;

        private Transform award_transform;
        protected override void xinit()
        {
            this.ward_name = this.transform.Find("name").GetComponent<UnrealTextField>();
            this.info = this.transform.Find("info").GetComponent<UnrealTextField>();
            this.icon = this.transform.Find("award").Find("icon").GetComponent<Image>();
            this.award_transform = this.transform.Find("award");
        }

        public void setAward(Award award)
        {
            this.award = award;
        }

        protected override void xrefresh()
        {
            this.ward_name.text = this.award.getName();
            this.info.text = this.award.getDescription();

            if (this.award.getImg() == null || "".Equals(this.award.getImg()))
            {
                this.award_transform.gameObject.SetActive(false);
            }
            else
            {
                this.award_transform.gameObject.SetActive(true);
                if (IconManager.getHeadPic(award.sid) != null)
                {
                    this.icon.sprite = IconManager.getHeadPic(award.sid);
                }
                else
                {
                    Loader.getLoader().load<long,Sprite>(new Url(award.getImg()),award.sid,this.refreshIcon);
                }
            }
        }


        void refreshIcon(long userid,Sprite icon)
        {
            if (userid == award.sid)
            {
                IconManager.saveHeadPic(userid,icon);
                this.icon.sprite = icon;
            }
        }
    }
}
