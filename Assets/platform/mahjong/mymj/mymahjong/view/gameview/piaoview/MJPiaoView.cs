using cambrian.common;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJPiaoView:UnrealLuaSpaceObject
    {
        protected Image right;

        protected Image top;

        protected Image left;

        protected MJSelfPiaoView down;

        protected override void xinit()
        {
            down = transform.Find("loc0").GetComponent<MJSelfPiaoView>();
            down.init();
            right = transform.Find("loc1").Find("bg").GetComponent<Image>();
            top = transform.Find("loc2").Find("bg").GetComponent<Image>();
            left = transform.Find("loc3").Find("bg").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            show(false);
        }

        protected void show(bool b)
        {
            right.gameObject.SetActive(b);
            top.gameObject.SetActive(b);
            left.gameObject.SetActive(b);
            down.gameObject.SetActive(b);
        }

        public void refreshSingleStatus(int index)
        {
            switch (index)
            {
                case MahjongRoomPanel.LOC_DOWN:
                    down.showPiaoOver();
                    down.setVisible(true);
                    break;
                case MahjongRoomPanel.LOC_RIGHT:
                    right.gameObject.SetActive(true);
                    break;
                case MahjongRoomPanel.LOC_TOP:
                    top.gameObject.SetActive(true);
                    break;
                case MahjongRoomPanel.LOC_LEFT:
                    left.gameObject.SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// 刷新玩家，选择飘
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isReplace"></param>
        public virtual void refreshStatus(int index, int status)
        {
            bool isPiao = StatusKit.hasStatus(status, MJPlayerCards.STATUS_PIAO_SELECT);
            switch (index)
            {
                case MahjongRoomPanel.LOC_DOWN:
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
                case MahjongRoomPanel.LOC_RIGHT:
                    right.gameObject.SetActive(isPiao);
                    break;
                case MahjongRoomPanel.LOC_TOP:
                    top.gameObject.SetActive(isPiao);
                    break;
                case MahjongRoomPanel.LOC_LEFT:
                    left.gameObject.SetActive(isPiao);
                    break;
            }
        }
    }
}
