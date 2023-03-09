using cambrian.common;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将定缺视图
    /// </summary>
    public class MJDQView : UnrealLuaSpaceObject
    {
        Image right;

        Image top;

        Image left;

        MJSelfDQView down;

        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<MJSelfDQView>();
            down.init();
            right = transform.Find("loc1").Find("bg").GetComponent<Image>();
            top = transform.Find("loc2").Find("bg").GetComponent<Image>();
            left = transform.Find("loc3").Find("bg").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            show(false);
        }

        private void show(bool b)
        {
            right.gameObject.SetActive(b);
            top.gameObject.SetActive(b);
            left.gameObject.SetActive(b);
            down.gameObject.SetActive(b);
        }

        /// <summary>
        /// 显示定缺中
        /// </summary>
        /// <param name="distypes">定缺的类型</param>
        public void showDingQue(int[] distypes)
        {
            down.setDisTypes(distypes);
            down.refresh();
            down.setVisible(true);
        }

        public void refreshSingleStatus(int index)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.showDQOver();
                    down.setVisible(true);
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

        /// <summary>
        /// 刷新玩家换牌状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isReplace"></param>
        public void refreshStatus(int index, int status)
        {
            bool isDing = StatusKit.hasStatus(status, MJPlayerCards.STATUS_DISTYPE);
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    if (isDing)
                    {
                        down.showDQOver();
                    }
                    else
                    {
                        down.showDq();
                    }

                    down.setVisible(true);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(isDing);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(isDing);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(isDing);
                    break;
            }
        }
    }
}
