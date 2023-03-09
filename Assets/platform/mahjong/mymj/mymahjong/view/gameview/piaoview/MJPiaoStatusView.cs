using cambrian.common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 玩家飘的icon
    /// </summary>
    public class MJPiaoStatusView:UnrealLuaSpaceObject
    {
        protected Image down;

        protected Vector3 downpos;

        protected Image right;

        protected Vector3 rightpos;

        protected Image top;

        protected Vector3 toppos;

        protected Image left;

        protected Vector3 leftpos;

        protected Image bigPiao;
        /// <summary>
        /// 甩飘视图
        /// </summary>
        protected MJFlutterView flutterView;

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
            bigPiao = transform.Find("bigpiao").GetComponent<Image>();
            flutterView = transform.Find("flutter").GetComponent<MJFlutterView>();
            flutterView.init();
        }

        protected override void xrefresh()
        {
            down.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            bigPiao.gameObject.SetActive(false);
            flutterView.setVisible(false);
        }

        /// <summary>
        /// 只有自己选择的时候，才会显示自己的飘
        /// </summary>
        /// <param name="index"></param>
        public virtual void showIndexPiao(int i)
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
        public virtual void showpiao(int[] piao)
        {
            for (int i = 0; i < piao.Length; i++)
            {
                int index = MahjongRoomPanel.getPlayerIndex(i);
                bool isPiao = StatusKit.hasStatus(piao[i], MJPlayerCards.STATUS_PIAO);
                switch (index)
                {
                    case MahjongRoomPanel.LOC_DOWN:
                        down.gameObject.SetActive(isPiao);
                        down.transform.localPosition = downpos;
                        break;
                    case MahjongRoomPanel.LOC_RIGHT:
                        right.gameObject.SetActive(isPiao);
                        right.transform.localPosition = rightpos;
                        break;
                    case MahjongRoomPanel.LOC_TOP:
                        top.gameObject.SetActive(isPiao);
                        top.transform.localPosition = toppos;
                        break;
                    case MahjongRoomPanel.LOC_LEFT:
                        left.gameObject.SetActive(isPiao);
                        left.transform.localPosition = leftpos;
                        break;
                }
            }
        }


        private int[] piao;
        /// <summary>
        /// 甩飘，第一个骰子的点数
        /// </summary>
        private int point1;
        /// <summary>
        /// 甩飘，第二个骰子的点数
        /// </summary>
        private int point2;

        /// <summary>
        /// 甩飘时使用的动画
        /// </summary>
        /// <param name="piao"></param>
        public void showPiaoAnimation(int[] piao,int point1,int point2)
        {
            this.piao = piao;
            this.point1 = point1;
            this.point2 = point2;
            bigPiao.gameObject.SetActive(true);
            bigPiao.transform.DOScaleX(1, 1f).onComplete = delay;
        }

        public void delay()
        {
            bigPiao.gameObject.SetActive(false);
            flutterView.setVisible(true);
            flutterView.showFlutterImg(point1, point2, movepiao);
        }

        public void movepiao()
        {
            for (int i = 0; i < piao.Length; i++)
            {
                int index = MahjongRoomPanel.getPlayerIndex(i);
                bool isPiao = StatusKit.hasStatus(piao[i], MJPlayerCards.STATUS_PIAO);
                switch (index)
                {
                    case MahjongRoomPanel.LOC_DOWN:
                        down.gameObject.SetActive(isPiao);
                        down.transform.localPosition = Vector3.zero;
                        down.transform.DOLocalMove(downpos, 0.2f);
                        break;
                    case MahjongRoomPanel.LOC_RIGHT:
                        right.gameObject.SetActive(isPiao);
                        right.transform.localPosition = Vector3.zero;
                        right.transform.DOLocalMove(rightpos, 0.2f);
                        break;
                    case MahjongRoomPanel.LOC_TOP:
                        top.gameObject.SetActive(isPiao);
                        top.transform.localPosition = Vector3.zero;
                        top.transform.DOLocalMove(toppos, 0.2f);
                        break;
                    case MahjongRoomPanel.LOC_LEFT:
                        left.gameObject.SetActive(isPiao);
                        left.transform.localPosition = Vector3.zero;
                        left.transform.DOLocalMove(leftpos, 0.2f);
                        break;
                }
            }
        }
    }
}
