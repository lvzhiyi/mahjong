using cambrian.game;
using System.Collections;
using UnityEngine;

namespace platform.poker
{
    public class PPDKClockView : PKClockView
    {
        private PDKMatch match;

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

        private void OnBecameInvisible()
        {
            StopAllCoroutines();
        }

        protected override void InvokeTime(int times)
        {
            if (!count.gameObject.activeInHierarchy) count.setVisible(true);
            if (!gameObject.activeInHierarchy) setVisible(true);
            match = PDKMatch.match;
            if (Room.room.getRule().getTrusteeship() != -1)
                PK_COUNT_TIME = Room.room.getRule().getTrusteeTime() / 1000;
            else PK_COUNT_TIME = CLOCK_TIME;
            StopAllCoroutines();
            StartCoroutine(match.replay ? "clocktimeReplay" : "clocktime", times);
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

        private IEnumerator startClock(int times)
        {
            if (!UnrealRoot.root.getDisplayObject<PDDZReplayRoomPanel>().rcview.timer.getPause())
            {
                StartCoroutine("clocktimeReplay", times);
                yield break;
            }
        }

        public override void showClock()
        {
            base.showClock();
        }

        public override void showClock(int index)
        {
            base.showClock(index);
        }

        public override void showClockConnect(int index, long startTimes)
        {
            base.showClockConnect(index, startTimes);
        }

        protected override void setClockPos(int index)
        {
            base.setClockPos(index);
        }

        public override void setIndex(int index)
        {
            this.index = index;
        }
    }
}
