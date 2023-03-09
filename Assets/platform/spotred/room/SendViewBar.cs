using scene.game;
using platform.spotred.playback;
using UnityEngine.UI;
using cambrian.uui;

namespace platform.spotred.room
{
    /// <summary>
    /// 招碰,招吃,吃吐,........
    /// </summary>
    public class SendViewBar : UnrealLuaSpaceObject
    {
        private int card;

        private int type;

        /// <summary>
        /// 牌图片
        /// </summary>
        private Image img;

        /// <summary>
        /// 招碰
        /// </summary>
        private Image zp;

        /// <summary>
        /// 召吃
        /// </summary>
        private Image zc;

        /// <summary>
        /// 吃吐
        /// </summary>
        private Image ct;

        /// <summary>
        /// 偷4
        /// </summary>
        private Image t4;

        /// <summary>
        /// 掉坎
        /// </summary>
        private Image dk;

        /// <summary>
        /// 撕对
        /// </summary>
        private Image shidui;
        /// <summary>
        /// 巴
        /// </summary>
        private SpritesList ba;

        private Image chow3;
        /// <summary>
        /// 破对
        /// </summary>
        private Image pdimg;
        /// <summary>
        /// 吃成4根
        /// </summary>
        private Image chow4;
        /// <summary>
        /// 后对7
        /// </summary>
        private Image after7;

        protected override void xinit()
        {
            this.img = this.transform.Find("card").GetComponent<Image>();
            this.zp = this.transform.Find("zp").GetComponent<Image>();
            this.zc = this.transform.Find("zc").GetComponent<Image>();
            this.ct = this.transform.Find("ct").GetComponent<Image>();
            this.t4 = this.transform.Find("t4").GetComponent<Image>();
            this.dk = this.transform.Find("dk").GetComponent<Image>();
            this.shidui = this.transform.Find("shidui").GetComponent<Image>();
            this.ba = this.transform.Find("kong").GetComponent<SpritesList>();
            this.chow3 = this.transform.Find("chow3").GetComponent<Image>();
            this.pdimg = this.transform.Find("pd").GetComponent<Image>();
            this.chow4 = this.transform.Find("chow4").GetComponent<Image>();
            this.after7 = this.transform.Find("after7").GetComponent<Image>();
        }

        protected override void xrefresh()
        {
            if (UnrealRoot.root.checkDisplayObject<SpotRoomPanel>() != null|| UnrealRoot.root.checkDisplayObject<ReplaySpotRoomPanel>()!=null)
            {
                this.img.sprite = WidgetManager.getInstance().getHandCard(card);
            }
            else
            {
                this.img.sprite = WidgetManager.getInstance().getJieSanShowCard(card);
            }

            show();
            DisplayKit.setLocalX(this.gameObject,index_space*getWidth());
        }

        public void setData(int card, int type)
        {
            this.card = card;
            this.type = type;
        }

        private void show()
        {
            this.zp.gameObject.SetActive(false);
            this.zc.gameObject.SetActive(false);
            this.ct.gameObject.SetActive(false);
            this.t4.gameObject.SetActive(false);
            this.dk.gameObject.SetActive(false);
            this.shidui.gameObject.SetActive(false);
            this.ba.gameObject.SetActive(false);
            this.chow3.gameObject.SetActive(false);
            this.pdimg.gameObject.SetActive(false);
            this.chow4.gameObject.SetActive(false);
            this.after7.gameObject.SetActive(false);

            switch (type)
            {
                case SendView.DIAOKAN:
                    this.dk.gameObject.SetActive(true);
                    return;
                case SendView.CHOWTU:
                    this.ct.gameObject.SetActive(true);
                    return;
                case SendView.ZHAOPONG:
                    this.zp.gameObject.SetActive(true);
                    return;
                case SendView.ZHAOCHOW:
                    this.zc.gameObject.SetActive(true);
                    return;
                case SendView.COUNT4:
                    this.t4.gameObject.SetActive(true);
                    return;
                case SendView.SHIDUI:
                    this.shidui.gameObject.SetActive(true);
                    return;
                case SendView.KONG:
                    this.ba.ShowIndex(Room.room.roomRule.rule.sid == 1017 ? 1 : 0);// 显示扒还是蹬
                    this.ba.gameObject.SetActive(true);
                    return;
                case SendView.CHOW3:
                    this.chow3.gameObject.SetActive(true);
                    return;
                case SendView.PODUI:
                    this.pdimg.gameObject.SetActive(true);
                    return;
                case SendView.CHOW4:
                    this.chow4.gameObject.SetActive(true);
                    return;
                case SendView.AFTER7:
                    this.after7.gameObject.SetActive(true);
                    return;
            }
        }

    }
}
