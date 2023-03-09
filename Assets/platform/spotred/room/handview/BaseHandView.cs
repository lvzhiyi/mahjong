using basef.im;
using platform.spotred.waitroom;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 单个玩家操作牌区域基类
    /// </summary>
    public class BaseHandView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 胡的图片
        /// </summary>
        private Image hu;

        /// <summary>
        /// 打的牌
        /// </summary>
        public PlayCardView playCardView;

        Vector3 initvector;

        public TouCardView touCardView;

        /// <summary>
        /// 报牌图标
        /// </summary>
        private Image bao;

        private CountDownView count_down;

        /// <summary>
        /// 招吃,招碰......
        /// </summary>
        private SendView sendView;

        /// <summary>
        /// 快捷聊天
        /// </summary>
        MatchIMQuickMSgBar quickmsg;

        public Vector3 scaleVector = new Vector3(1,1,1);

        protected override void xinit()
        {
            this.hu = transform.Find("hu").GetComponent<Image>();

            this.playCardView = this.transform.Find("playcardview").GetComponent<PlayCardView>();
            this.playCardView.init();
            this.initvector = this.playCardView.transform.localPosition;

            this.touCardView = this.transform.Find("toucardview").GetComponent<TouCardView>();
            this.touCardView.init();

            this.bao = this.transform.Find("bao").GetComponent<Image>();

            this.count_down = transform.Find("countdown").GetComponent<CountDownView>();
            this.count_down.init();

            this.sendView = transform.Find("sendview").GetComponent<SendView>();
            this.sendView.init();

            if (this.transform.Find("quickmsg") != null)
            {
                this.quickmsg = this.transform.Find("quickmsg").GetComponent<MatchIMQuickMSgBar>();
                this.quickmsg.init();
                this.quickmsg.setVisible(false);
            }
        }

        protected override void xrefresh()
        {
            this.hu.gameObject.SetActive(false);
            this.playCardView.setVisible(false);
            this.touCardView.setVisible(false);
            this.bao.gameObject.SetActive(false);
            this.count_down.setVisible(false);
            this.sendView.setVisible(false);
        }

        public void setIMQuickMsg(IMQuickMsg msg)
        {
            if (msg.type == IMQuickMsg.TYPE_TEXT)
            {
                this.quickmsg.setIMQuickMsg(msg);
                this.quickmsg.refresh();
                this.quickmsg.setVisible(true);
            }
        }

        public void setIMTextMsg(IMTextMsg msg)
        {
            this.quickmsg.showIMTextMsg(msg);
            this.quickmsg.setVisible(true);
        }


        public void isShowBaoPai(bool b)
        {
            this.bao.gameObject.SetActive(b);
        }

        public void showHu()
        {
            this.hu.gameObject.SetActive(true);
        }

        public void hideHu()
        {
            this.hu.gameObject.SetActive(false);
        }

        public Vector3 getPlayCardViewLocation()
        {
            return this.playCardView.transform.localPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b">是否显示闹钟</param>
        /// <param name="isPlaySound">是否倒计时播放声音</param>
        public void isShow(bool b,bool isPlaySound)
        {
            this.count_down.setVisible(b);
            if (b)
                this.count_down.showTime(isPlaySound);
            else
            {
                this.count_down.canInvokeTime();
            }
        }

        public void sendCardView(int card, int type)
        {
            this.sendView.sendCard(card, type);
            this.sendView.setVisible(true);
        }

        /// <summary>
        /// 设置打的牌
        /// </summary>
        /// <param name="card"></param>
        public void setPlayCard(int card, bool isAnamition)
        {
            this.playCardView.setCard(card);
            DisplayKit.setLocalRoateXYZ(this.playCardView.transform, 0, 0, 0);
            this.playCardView.setVisible(true);
            this.playCardView.clearAnmation();

            if (isAnamition)
            {
                DisplayKit.setLocalScaleXY(this.playCardView.transform, 1.4f);
                this.playCardView.transform.DOScale(scaleVector, 0.2f );
            }
            else
            {
                DisplayKit.setLocalScaleXY(this.playCardView.transform, 1f);
            }
        }


        /// <summary>
        /// 隐藏打的牌
        /// </summary>
        public void hidePlayCard()
        {
            this.playCardView.setVisible(false);
            DisplayKit.setLocalScaleXY(this.playCardView.card.transform, 1);
            DisplayKit.setLocalXY(this.playCardView.transform, initvector.x, initvector.y);
        }

        public void setTouCard(int card, int index, Action<int> tou)
        {
            this.touCardView.setCards(new int[] { card });
            this.touCardView.refresh();
            this.touCardView.setVisible(true);

            DisplayKit.setLocalScaleXY(this.touCardView.transform, 1.2f);
            this.touCardView.transform.DOScale(scaleVector, 0.2f).OnComplete(() =>
            {
                tou(index);
            });
        }


        public Vector3 getTouCardViewLocation()
        {
            return this.touCardView.transform.localPosition;
        }

        public void hideTouCard()
        {
            this.touCardView.setVisible(false);
            DisplayKit.setLocalScaleXY(this.touCardView.transform, 1);
            DisplayKit.setLocalXY(this.touCardView.transform, initvector.x, initvector.y);
        }
    }
}
