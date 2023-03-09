using UnityEngine;

namespace platform.poker
{
    /// <summary> 右边出牌显示 </summary>
    public class PKRightHnadCard : PKHandsBean
    {
        private RectTransform rect;

        public float width = 140;

        public float imgwidth = 140;

        protected override void xinit()
        {
            base.xinit();
            rect = GetComponent<RectTransform>();
        }

        protected override void xrefresh()
        {
            if (cards == null) cards = new int[0];

            container.resize(cards.Length);
            rect.sizeDelta = new Vector2(imgwidth + (cards.Length * width), rect.sizeDelta.y);
            for (int i = 0; i < cards.Length; i++)
            {
                var bar = (PKShowPlayCardBar)container.bars[i];
                bar.setImg(cards[i]);
                bar.index_space = (int)(i * ShowHandSpace);
                bar.setDiZhu(i == cards.Length - 1, PKGAME.RIGHT);
                bar.refreshPos();
            }

            if (!gameObject.activeSelf) setVisible(true);
            if (cards.Length > 15) transform.localScale = Vector3.one * scaleFour;
            else if (cards.Length > 12) transform.localScale = Vector3.one * scaleThree;
            else if (cards.Length > 10) transform.localScale = Vector3.one * scaleTwo;
            else transform.localScale = Vector3.one * scaleOne;
        }
    }
}
