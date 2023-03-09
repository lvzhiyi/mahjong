using basef.rule;
using cambrian.common;
using cambrian.game;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 闹钟视图
    /// </summary>
    public class CountDownView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 倒计时
        /// </summary>
        public static int COUNT_TIME = 30;

        private Text count;

        private int time = COUNT_TIME;

        [HideInInspector] public bool isPlay = false;
        protected override void xinit()
        {
            this.count = transform.Find("time").GetComponent<Text>();
        }

        public void showTime(bool isplaysound)
        {
            time = COUNT_TIME;

            if (Room.room!=null)
            {
               Rule rule= Room.room.roomRule.rule;
                if (rule.getTrusteeship() == 1)//代表托管
                {
                    time = rule.getTrusteeTime()/1000;
                }
            }

            this.count.text = time+"";
            time--;
            this.isPlay = isplaysound;
            curtime = TimeKit.currentTimeMillis;
        }

        public void InvokeTime()
        {
            if (time <= 5&&isPlay)
            {
                if (time == 5)
                {
                    WXManager.getInstance().vibrator(1000);
                }
                SoundManager.playClickSound();
            }


            this.count.text = time+"";
            if (time <= 0)
            {
                canInvokeTime();
            }
            else
            {
                time--;
            }
        }


        public void canInvokeTime()
        {
            curtime = 0;
            isPlay = false;
        }

        private long curtime;
        protected override void xupdate()
        {
            long time= TimeKit.currentTimeMillis;
            if (curtime == 0) return;

            if (time - curtime > TimeKit.SECOND_MILLS)
            {
                curtime = time;
                InvokeTime();
            }
        }
    }
}
