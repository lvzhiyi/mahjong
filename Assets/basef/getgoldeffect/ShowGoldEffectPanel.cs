using basef.award;
using cambrian.common;
using scene.game;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace basef.getgoldeffect
{
    public class ShowGoldEffectPanel:UnrealLuaPanel
    {
        public Image effect1;

        private Text num;

        private Award award;

        private Image img;

        protected override void xinit()
        {
            base.xinit();
            this.num = this.content.Find("content").Find("num").GetComponent<Text>();
            this.img = this.content.Find("content").Find("gold_icon").GetComponent<Image>();
        }

        public void setNum(long money)
        {
            this.img.sprite = CacheManager.shopimg[19];
            this.num.text = "金豆X"+money;
        }

        public void showAward(Award award)
        {
            this.img.sprite=CacheManager.shopimg[StringKit.parseInt(award.img)];
            this.num.text = award.getCount();
        }

        public void showProp(Sprite sprite,string text)
        {
            this.img.sprite = sprite;
            this.num.text = text;
        }

        protected override void xrefresh()
        {
            effect1.transform.DORotate(new Vector3(0, 0, -360), 10,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}
