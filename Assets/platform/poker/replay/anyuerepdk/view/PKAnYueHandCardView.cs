using UnityEngine;

namespace platform.poker
{
    /// <summary> 下方出牌显示 </summary>
    public class PKAnYueHandCardView : PKRightHnadCard
    {
        protected override void xinit()
        {
            base.xinit();
        }

        protected override void xrefresh()
        {
         
            if (cards == null) cards = new int[0];      
            float point = -((cards.Length - 1) / 2.0f);
            container.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                var bar = (PKShowPlayCardBar)container.bars[i];
                bar.setImg(cards[i]);
                float pos = point + i;
                bar.index_space = Mathf.FloorToInt(pos * ShowHandSpace);
                bar.setDiZhu(i == cards.Length - 1,PKGAME.DOWN);
                bar.refreshPos();
            }
            container.resizeDelta();
            if (!gameObject.activeSelf) setVisible(true);
            if (cards.Length > 15) transform.localScale = Vector3.one * scaleFour;
            else if (cards.Length > 12) transform.localScale = Vector3.one * scaleThree;
            else if (cards.Length > 10) transform.localScale = Vector3.one * scaleTwo;
            else transform.localScale = Vector3.one * scaleOne;
        }
    }
}
