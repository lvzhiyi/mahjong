using cambrian.common;
using cambrian.game;
using cambrian.uui;
using DG.Tweening;
using UnityEngine.UI;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将中间信息
    /// </summary>
    public class MJIndexCenterView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 剩余牌数量
        /// </summary>
        protected UnrealTextField cardnum;
        /// <summary>
        /// 剩余局数
        /// </summary>
        protected UnrealTextField tray;
        /// <summary>
        /// 到计时
        /// </summary>
        protected Text counttime;
        /// <summary>
        /// 方向(指示该谁的回合)
        /// </summary>
        protected Image down;
        /// <summary>
        /// 方向(指示该谁的回合)
        /// </summary>
        protected Image right;
        /// <summary>
        /// 方向(指示该谁的回合)
        /// </summary>
        protected Image top;

        /// <summary>
        /// 方向(指示该谁的回合)
        /// </summary>
        protected Image left;

        protected SpritesList paizuo;

        protected int time=0;

        protected override void xinit()
        {
            cardnum = transform.Find("cardsnum").GetComponent<UnrealTextField>();
            cardnum.init();
            tray = transform.Find("tray").GetComponent<UnrealTextField>();
            tray.init();
            counttime = transform.Find("time").GetComponent<Text>();
            down = transform.Find("down").GetComponent<Image>();
            right = transform.Find("right").GetComponent<Image>();
            top = transform.Find("top").GetComponent<Image>();
            left = transform.Find("left").GetComponent<Image>();

            paizuo = transform.Find("paizuo_bg").GetComponent<SpritesList>();
            

            right.DOFade(0.2f, 0.8f).SetLoops(-1, LoopType.Yoyo);
            down.DOFade(0.2f, 0.8f).SetLoops(-1, LoopType.Yoyo);
            top.DOFade(0.2f, 0.8f).SetLoops(-1, LoopType.Yoyo);
            left.DOFade(0.2f, 0.8f).SetLoops(-1, LoopType.Yoyo);
        }

        protected override void xrefresh()
        {
            hide();
        }

        public virtual void showInfo(int li, int lz)
        {

        }

        public virtual void showpaizuo(int index)
        {
            this.paizuo.ShowIndex(index);
        }

        public virtual void hide()
        {
            down.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置剩余牌的数量
        /// </summary>
        /// <param name="num"></param>
        public virtual void setCardNum(int num)
        {
            cardnum.text = num.ToString();
        }

        public virtual void showRound(int index,int time=30)
        {
            hide();
            isPlay = false;
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.gameObject.SetActive(true);
                    isPlay = true;
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(true);
                    break;
            }
            counttime.text = time.ToString();
            this.time = time;
            curtime = TimeKit.currentTimeMillis;
        }

        /// <summary>
        /// 显示哪个换牌或者定缺了
        /// </summary>
        /// <param name="index"></param>
        /// <param name="time"></param>
        public virtual void showHuanOrDing(int index, int time = 0)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.gameObject.SetActive(false);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(false);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(false);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(false);
                    break;
            }
        }

        public virtual void showAllDirection()
        {
            down.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            top.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            int len = Room.room.players.Length;
            for (int i = 0; i < len; i++)
            {
                show(MahJongPanel.getPlayerIndex(i));
            }
        }

        protected virtual void show(int index)
        {
            switch (index)
            {
                case MahJongPanel.LOC_DOWN:
                    down.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_LEFT:
                    left.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_TOP:
                    top.gameObject.SetActive(true);
                    break;
                case MahJongPanel.LOC_RIGHT:
                    right.gameObject.SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// 设置局数
        /// </summary>
        /// <param name="str"></param>
        public virtual void setJuShu(int cur,int max)
        {
            tray.text =cur + "/" + max;
            
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
        }

        protected bool isPlay = false;

        public virtual void InvokeTime()
        {
            if (time <= 5 && isPlay)
            {
                if (time == 5)
                {
                    WXManager.getInstance().vibrator(1000);
                }

                SoundManager.playClickSound();
            }

            if (time < 10)
                this.counttime.text = "0" + time;
            else
                this.counttime.text = time + "";
            if (time <= 0)
            {
                canInvokeTime();
            }
            else
            {
                time--;
            }
        }

        public virtual void canInvokeTime()
        {
            curtime = 0;
            isPlay = false;
        }

        protected long curtime;
        protected override void xupdate()
        {
            long time = TimeKit.currentTimeMillis;
            if (curtime == 0) return;

            if (time - curtime > TimeKit.SECOND_MILLS)
            {
                curtime = time;
                InvokeTime();
            }
        }
    }
}
