using cambrian.common;
using UnityEngine;

namespace platform.poker
{

    /// <summary> 闹钟界面 </summary>
    public class PKClockView : UnrealLuaSpaceObject
    {
        /// <summary> 闹钟位置 </summary>
        protected Transform clock;

        /// <summary> 闹钟时间 </summary>
        protected UnrealTextField count;

        /// <summary> 闹钟显示位置 </summary>
        protected Vector3 down, left, right,top;

        /// <summary> 是否开始计时 </summary>
        protected bool isPlay;

        /// <summary> 闹钟剩余时间 </summary>
        protected int remainTime;

        /// <summary> 闹钟位置下标 </summary>
        protected int index;

        public const int CLOCK_TIME = 30;

        protected int PK_COUNT_TIME = 0;

        public const int PK_DELAY_TIME = 5;

        protected virtual void InvokeTime(int times) { }

        /// <summary> 显示闹钟 </summary>
        public virtual void showClock()
        {
            setClockPos(index);
            InvokeTime(PK_COUNT_TIME);
        }

        /// <summary> 显示闹钟 </summary>
        public virtual void showClock(int index)
        {
            this.index = index;
            setClockPos(index);
            InvokeTime(PK_COUNT_TIME);
        }

        /// <summary> 重连显示闹钟 </summary>
        public virtual void showClockConnect(int index, long startTimes)
        {
            setClockPos(index);
            int time = (PK_COUNT_TIME - getSecondsAgo(startTimes));
            time = time > 0 ? time : 0;
            InvokeTime(time);
        }

        /// <summary> 设置闹钟位置 </summary>
        protected virtual void setClockPos(int index)
        {
            switch (PKGAME.GetIndex(index))
            {
                case PKGAME.DOWN:
                    clock.localPosition = down;
                    break;
                case PKGAME.RIGHT:
                    clock.localPosition = right;
                    break;
                case PKGAME.LEFT:
                    clock.localPosition = left;
                    break;
                case PKGAME.TOP:
                    clock.localPosition = top;
                    break;
            }
        }

        /// <summary> 获取多少秒前 </summary>
        public virtual int getSecondsAgo(long time)
        {
            if (time <= 0) return 0;
            long temp = TimeKit.currentTimeMillis - time;
            if (temp <= TimeKit.SECOND_MILLS) return 0;

            temp %= TimeKit.DAY_MILLS;
            temp %= TimeKit.HOUR_MILLS;
            temp %= TimeKit.MIN_MILLS;
            return (int)(temp / TimeKit.SECOND_MILLS);
        }

        /// <summary> 设置位置 </summary>
        public virtual void setIndex(int index)
        {
            this.index = index;
        }
    }
}
