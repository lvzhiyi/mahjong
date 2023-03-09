using cambrian.common;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家报牌视图
    /// </summary>
    public class AYMJTingCardView : UnrealLuaSpaceObject
    {
        private Image down;

        private Image right;

        private Image top;

        private Image left;

        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<Image>();
            right = transform.Find("loc1").GetComponent<Image>();
            top = transform.Find("loc2").GetComponent<Image>();
            left = transform.Find("loc3").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            down.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
        }

        public void setBaoPai(int[] baotatus)
        {
            for (int i = 0; i < baotatus.Length; i++)
            {
                int index = MahJongPanel.getPlayerIndex(i);
                bool isBao = StatusKit.hasStatus(baotatus[i], MJPlayerCards.STATUS_BAOPAI);
                switch (index)
                {
                    case MahJongPanel.LOC_DOWN:

                        down.gameObject.SetActive(isBao);
                        break;
                    case MahJongPanel.LOC_RIGHT:

                        right.gameObject.SetActive(isBao);
                        break;
                    case MahJongPanel.LOC_TOP:

                        top.gameObject.SetActive(isBao);
                        break;
                    case MahJongPanel.LOC_LEFT:
                        left.gameObject.SetActive(isBao);
                        break;
                }
            }
        }

        /// <summary>
        /// 显示单个玩家报牌图标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="distype"></param>
        public void setSingleDistype(int index)
        {
           index = MahJongPanel.getPlayerIndex(index);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                   
                    down.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_RIGHT:
                   
                    right.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_TOP:
                   
                    top.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_LEFT:
                    
                    left.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
