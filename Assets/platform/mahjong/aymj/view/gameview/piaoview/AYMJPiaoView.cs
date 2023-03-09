using cambrian.common;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class AYMJPiaoView: MJPiaoView
    {
        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<AYMJSelfPiaoView>();
            down.init();
            right = transform.Find("loc1").Find("bg").GetComponent<Image>();
            top = transform.Find("loc2").Find("bg").GetComponent<Image>();
            left = transform.Find("loc3").Find("bg").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
        }

        /// <summary>
        /// 刷新玩家，选择飘
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isReplace"></param>
        public override void refreshStatus(int index, int status)
        {
            bool isPiao = StatusKit.hasStatus(status, MJPlayerCards.STATUS_PIAO_SELECT);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    if (isPiao)
                    {
                        down.showPiaoOver();
                    }
                    else
                    {
                        down.showPiao();
                    }
                    down.setVisible(true);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(isPiao);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(isPiao);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(isPiao);
                    break;
            }
        }
    }
}
