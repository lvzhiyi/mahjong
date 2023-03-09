using scene.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 手里具体的牌
    /// </summary>
    public class HandCardBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 当前该列牌的数量
        /// </summary>
        public int card_count;

        /// <summary>
        /// 第几列
        /// </summary>
        public int column;

        /// <summary>
        /// 牌
        /// </summary>
        public int card_value;

        /// <summary>
        /// 不能出(false:不能出)
        /// </summary>
        bool disabelchu;

        /// <summary>
        /// 是否已经展开
        /// </summary>
        [HideInInspector] public bool isUp;

        protected Image image;

        /// <summary>
        /// 不能出牌的背景
        /// </summary>
        private Image bg;

        public Image check;

        /// <summary>
        /// 该牌是否被选中
        /// </summary>
        [HideInInspector] public bool ischecked;

        /// <summary>
        /// 间隔的高度
        /// </summary>
        public int height_1;

        protected override void xinit()
        {
            this.image = transform.Find("card").GetComponent<Image>();
            if (transform.Find("bg") != null)
                this.bg = transform.Find("bg").GetComponent<Image>();
            if (transform.Find("bg_11") != null)
                this.check = transform.Find("bg_11").GetComponent<Image>();
        }

        public virtual void refreshCard(int value, int count, bool disabelchu)
        {
            this.card_value = value;
            this.card_count = count;
            this.disabelchu = disabelchu;

            this.image.sprite = WidgetManager.getInstance().getNowHandCard(value);


            DisplayKit.setLocalXY(this.image.transform, 0, -56);

            DisplayKit.setLocalScaleXY(this.image.transform, 1);

            DisplayKit.setLocalXY(this.transform, 0, height_1 * (card_count - index_space - 1));
            if (this.bg != null)
                this.bg.gameObject.SetActive(false);
            if (!disabelchu)
            {
                if (this.bg != null)
                    this.bg.gameObject.SetActive(true);
            }

            hideCheck();
        }

        public Transform getCardImge()
        {
            return image.transform;
        }

        public void hideCheck()
        {
            if (this.check != null)
                this.check.gameObject.SetActive(false);
            this.ischecked = false;
        }

        public void showCheck()
        {
            if (this.check != null)
                this.check.gameObject.SetActive(true);
            this.ischecked = true;

        }

        public bool getDiableChu()
        {
            return disabelchu;
        }

        /// <summary>
        /// 展开
        /// </summary>
        public void UpCard()
        {
            this.isUp = true;
        }

        public void resize()
        {
            this.isUp = false;
            if (this.check != null)
                this.check.gameObject.SetActive(false);
            this.ischecked = false;
        }
    }
}
