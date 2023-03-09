using cambrian.common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家飘的icon
    /// </summary>
    public class AYMJPiaoStatusView : MJPiaoStatusView
    {
        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<Image>();
            downpos = down.transform.localPosition;
            right = transform.Find("loc1").GetComponent<Image>();
            rightpos = right.transform.localPosition;
            top = transform.Find("loc2").GetComponent<Image>();
            toppos = top.transform.localPosition;
            left = transform.Find("loc3").GetComponent<Image>();
            leftpos = left.transform.localPosition;
        }

        protected override void xrefresh()
        {
            down.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
        }

        /// <summary>
        /// 只有自己选择的时候，才会显示自己的飘
        /// </summary>
        /// <param name="index"></param>
        public override void showIndexPiao(int i)
        {
            int index = MahJongPanel.getPlayerIndex(i);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.gameObject.SetActive(true);
                    down.transform.localPosition = downpos;
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(true);
                    right.transform.localPosition = rightpos;
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(true);
                    top.transform.localPosition = toppos;
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(true);
                    left.transform.localPosition = leftpos;
                    break;
            }
        }

        /// <summary>
        /// 直接显示有无飘，(没有动画)
        /// </summary>
        /// <param name="piao"></param>
        public override void showpiao(int[] piao)
        {
            for (int i = 0; i < piao.Length; i++)
            {
                int index = MahJongPanel.getPlayerIndex(i);
                bool isPiao = StatusKit.hasStatus(piao[i], MJPlayerCards.STATUS_PIAO);
                switch (index)
                {
                    case MahJongPanel.LOC_DOWN:
                        down.gameObject.SetActive(isPiao);
                        down.transform.localPosition = downpos;
                        break;
                    case MahJongPanel.LOC_RIGHT:
                        right.gameObject.SetActive(isPiao);
                        right.transform.localPosition = rightpos;
                        break;
                    case MahJongPanel.LOC_TOP:
                        top.gameObject.SetActive(isPiao);
                        top.transform.localPosition = toppos;
                        break;
                    case MahJongPanel.LOC_LEFT:
                        left.gameObject.SetActive(isPiao);
                        left.transform.localPosition = leftpos;
                        break;
                }
            }
        }
    }
}
