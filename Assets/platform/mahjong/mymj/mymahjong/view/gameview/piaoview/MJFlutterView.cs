using System;
using cambrian.uui;
using DG.Tweening;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 甩飘
    /// </summary>
    public class MJFlutterView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 开始甩飘icon
        /// </summary>
        private Image flutterimg;

        /// <summary>
        /// 骰子左
        /// </summary>
        private SpritesList dice1;
        /// <summary>
        /// 骰子右
        /// </summary>
        private SpritesList dice2;
        /// <summary>
        /// 点数1
        /// </summary>
        private int point1;
        /// <summary>
        /// 点数2
        /// </summary>
        public int point2;

        protected override void xinit()
        {
            flutterimg = transform.Find("flutterimg").GetComponent<Image>();
            dice1 = transform.Find("dice1").GetComponent<SpritesList>();
            dice2 = transform.Find("dice2").GetComponent<SpritesList>();
        }


        private Action back;

        public void showFlutterImg(int point1,int point2,Action back)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.back = back;

            flutterimg.gameObject.SetActive(false);
            dice1.resetFade();
            dice2.resetFade();
            dice1.setVisible(true);
            dice2.setVisible(true);
            dice1.ShowIndex(point1 - 1);
            dice2.ShowIndex(point2 - 1);
            dice1.transform.DOScaleX(1, 1f).onComplete = scaleOver;//只是用来延迟时间
        }

        private void scaleOver()
        {
            dice1.setFade(0,0.5f, fadeOver);
            dice2.setFade(0, 0.5f,null);
        }

        private void fadeOver()
        {
            dice1.setVisible(false);
            dice2.setVisible(false);
            flutterimg.gameObject.SetActive(true);
            flutterimg.transform.DOScaleX(1, 2).onComplete = flutterOver;
        }

        private void flutterOver()
        {
            flutterimg.gameObject.SetActive(false);
            if (back != null)
            {
                back();
                back = null;
            }
        }

    }
}
