using UnityEngine.UI;
using UnityEngine;
using System;
using DG.Tweening;

namespace platform.mahjong
{
    /// <summary>
    /// 换三张结束(显示换牌的方向)
    /// </summary>
    public class MJHuanSZOverView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 逆时针
        /// </summary>
        public const int clockWise = 1;
        /// <summary>
        /// 对家换牌
        /// </summary>
        public const int antiClockWise=2;
        /// <summary>
        /// 顺时针
        /// </summary>
        public const int upAndDown = 3;
        /// <summary>
        /// 换牌方式
        /// </summary>
        Text huanMode;
        /// <summary>
        /// 顺时针视图
        /// </summary>
        UnrealLuaSpaceObject clockWiseview;
        /// <summary>
        /// 逆时针视图
        /// </summary>
        UnrealLuaSpaceObject antiClockWiseview;
        /// <summary>
        /// 对家换牌视图
        /// </summary>
        MJHszUpAndDownView upAndDownview;
        /// <summary>
        /// 显示时间
        /// </summary>
        [HideInInspector] public CountdownVisibleProcess visibleProcess;

        protected override void xinit()
        {
            huanMode = transform.Find("textinfo").GetComponent<Text>();
            clockWiseview = transform.Find("clockWise").GetComponent<UnrealLuaSpaceObject>();
            antiClockWiseview = transform.Find("antiClockWise").GetComponent<UnrealLuaSpaceObject>();
            upAndDownview = transform.Find("upAndDown").GetComponent<MJHszUpAndDownView>();
            upAndDownview.init();
            visibleProcess = GetComponent<CountdownVisibleProcess>();
        }

        protected override void xrefresh()
        {
            hide();
        }

        private void hide()
        {
            clockWiseview.setVisible(false);
            antiClockWiseview.setVisible(false);
            upAndDownview.setVisible(false);
        }

        public void showMode(int type,Action callback)
        {
            hide();
            string str = "本局对家换牌";
            if (Room.room.players.Length == 4)
            {
                switch (type)
                {
                    case clockWise:
                        str = "本局顺时针换牌";
                        clockWiseview.setVisible(true);
                        clockWiseview.transform.DORotate(new Vector3(0, 0, -360), 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
                        break;
                    case antiClockWise:
                        str = "本局对家换牌";
                        upAndDownview.setVisible(true);
                        upAndDownview.refresh();
                        break;
                    case upAndDown:
                        str = "本局逆时针换牌";
                        antiClockWiseview.setVisible(true);
                        antiClockWiseview.transform.DORotate(new Vector3(0, 0, 360), 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
                        break;
                }
            }
            else if(Room.room.players.Length==3)
            {
                if (type == 2)
                    type = 3;
                switch (type)
                {
                    case clockWise:
                        str = "本局顺时针换牌";
                        clockWiseview.setVisible(true);
                        clockWiseview.transform.DORotate(new Vector3(0, 0, -360), 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
                        break;
                    case upAndDown:
                        str = "本局逆时针换牌";
                        antiClockWiseview.setVisible(true);
                        antiClockWiseview.transform.DORotate(new Vector3(0, 0, 360), 10, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
                        break;
                }
            }
            else
            {
                str = "本局对家换牌";
                upAndDownview.setVisible(true);
                upAndDownview.refresh();
            }

            huanMode.text = str;
            this.visibleProcess.setAction(callback);
        }
    }
}
