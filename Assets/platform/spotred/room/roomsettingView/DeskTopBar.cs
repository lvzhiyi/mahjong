using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class DeskTopBar : UnrealLuaSpaceObject
    {
        private Text text;

        private Image selected;

        [HideInInspector] public int type;

        protected override void xinit()
        {
            this.text = this.transform.Find("text").GetComponent<Text>();
            this.selected = this.transform.Find("normal").GetComponent<Image>();
        }

        public void setData(string text, int type, bool ischeck)
        {
            this.text.text = text;
            this.type = type;
            this.selected.gameObject.SetActive(ischeck);
        }

        public void checkedImg(bool b)
        {
            this.selected.gameObject.SetActive(b);
        }
    }
}
