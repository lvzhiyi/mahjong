using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJSettingDeskBgBar:UnrealLuaSpaceObject
    {
        private Transform check;

        private SpritesList list;

        private Text bgtext;

        private string bgname;

        protected override void xinit()
        {
            base.xinit();
            check = transform.Find("selected");
            list = transform.Find("img").GetComponent<SpritesList>();
            bgtext = transform.Find("text").Find("text").GetComponent<Text>();
        }

        public void setData(bool b,string bgname)
        {
            check.gameObject.SetActive(b);
            this.bgname = bgname;
        }

        protected override void xrefresh()
        {
            list.ShowIndex(index_space, false);
            bgtext.text = bgname;
        }
    }
}
