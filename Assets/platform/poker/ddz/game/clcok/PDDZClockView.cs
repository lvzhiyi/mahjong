using System.Collections;
using cambrian.game;
using UnityEngine;

namespace platform.poker
{
    public class PDDZClockView : PKClockView
    {
        private DDZMatch match;

        protected override void xinit()
        {
            index = -1;
            clock = transform.Find("time").transform;
            count = clock.GetComponent<UnrealTextField>();

            down = transform.Find("down").localPosition;
            left = transform.Find("left").localPosition;
            right = transform.Find("right").localPosition;
        }

        protected override void xrefresh()
        {
            count.gameObject.SetActive(false);
        }

        private IEnumerator clocktime(int times)
        {
            isPlay = true;
            remainTime = times;
            count.text = remainTime.ToString();
            for (; remainTime > 0;)
            {
                if (remainTime <= 5 && isPlay)
                {
                    WXManager.getInstance().vibrator(1000);
                    SoundManager.playClickSound();
                }
                yield return new WaitForSeconds(1f);
                --remainTime;
                count.text = remainTime.ToString();
            }
            if (remainTime == 0)
            {
                isPlay = false;
                yield break;
            }
        }

        private IEnumerator clocktimeReplay(int times)
        {
            isPlay = true;
            remainTime = times;
            count.text = remainTime.ToString();
            for (; remainTime > 0;)
            {
                yield return new WaitForSeconds(1f);
                if (UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>().rcview.timer.getPause())
                {
                    StartCoroutine("startClock", remainTime);
                    yield break;
                }
                --remainTime;
                count.text = remainTime.ToString();
            }
            if (remainTime == 0)
            {
                isPlay = false;
                yield break;
            }
        }

        private IEnumerator startClock(int times)
        {
            if (!UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>().rcview.timer.getPause())
            {
                StartCoroutine("clocktimeReplay", times);
                yield break;
            }
        }

        private void OnBecameInvisible()
        {
            StopAllCoroutines();
        }

        protected override void InvokeTime(int times)
        {
            if (!count.gameObject.activeInHierarchy) count.setVisible(true);
            if (!gameObject.activeInHierarchy) setVisible(true);
            match = DDZMatch.match;
            if (Room.room.getRule().getTrusteeship() != -1)
                PK_COUNT_TIME = Room.room.getRule().getTrusteeTime() / 1000;
            else PK_COUNT_TIME = CLOCK_TIME;
            StopAllCoroutines();
            StartCoroutine(match.replay ? "clocktimeReplay" : "clocktime", times);
        }

        /// <summary> 显示闹钟 </summary>
        public override void showClock()
        {
            base.showClock();
        }

        /// <summary> 显示闹钟 </summary>
        public override void showClock(int index)
        {
            base.showClock(index);
        }

        /// <summary> 重连显示闹钟 </summary>
        public override void showClockConnect(int index, long startTimes)
        {
            base.showClockConnect(index, startTimes);
        }

        /// <summary> 设置闹钟位置 </summary>
        protected override void setClockPos(int index)
        {
            base.setClockPos(index);
        }

        /// <summary> 设置位置 </summary>
        public override void setIndex(int index)
        {
            this.index = index;
        }
    }
}
