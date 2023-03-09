using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    public class SlipCardBar:UnrealLuaSpaceObject
    {
        private Image img;

        private Text text;

        private Image bg_1;


        [HideInInspector] public int card;

        [HideInInspector] public int count;

        [HideInInspector] public bool isselected;


        protected override void xinit()
        {
            this.img = transform.Find("bg").GetComponent<Image>();
            this.text = transform.Find("text").GetComponent<Text>();
            this.bg_1 = transform.Find("bg_1").GetComponent<Image>();
        }

        public void setData(int card,int count)
        {
            this.card = card;
            this.count = count;
        }

        protected override void xrefresh()
        {
            this.img.sprite = WidgetManager.getInstance().getNowHandCard(card);
            this.text.text = count + "张";
            this.bg_1.gameObject.SetActive(false);
        }

        public void setSelected(bool b)
        {
            this.bg_1.gameObject.SetActive(b);
            isselected = b;
        }
    }
}
