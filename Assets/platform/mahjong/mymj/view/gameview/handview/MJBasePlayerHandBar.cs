using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJBasePlayerHandBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 牌背景
        /// </summary>
        protected Image card_bg;
        /// <summary>
        /// 牌图片
        /// </summary>
        protected Image card;
        /// <summary>
        /// 遮罩
        /// </summary>
        protected Image mask;
        /// <summary>
        /// 听牌图标显示
        /// </summary>
        protected SpritesList ting;
        /// <summary>
        /// 胡牌方向
        /// </summary>
        protected Image huDirection;
        /// <summary>
        /// 牌值
        /// </summary>
        [HideInInspector] public int cardValue;
        /// <summary>
        /// 位置
        /// </summary>
        protected int postion;

        /// <summary>
        /// 是否是被选中
        /// </summary>
        [HideInInspector] public bool isSelect;
        /// <summary>
        /// 听牌信息
        /// </summary>
        [HideInInspector] public TingCardsInfo tinginfo;
        /// <summary>
        /// 定缺的类型
        /// </summary>
        protected int distype;
        /// <summary>
        /// 是否手里有定缺的牌
        /// </summary>
        protected bool hasDistypeCard;
        /// <summary>
        /// 是否胡牌(用于放倒手牌)
        /// </summary>
        protected bool isHu;
        /// <summary>
        /// 摸牌
        /// </summary>
        protected MJMOCard moCard;
       
        /// <summary>
        /// 获取手牌的y坐标(自己手牌用的)
        /// </summary>
        protected float cardy;


        protected override void xinit()
        {
            if (transform.Find("bg") != null)
                card_bg = transform.Find("bg").GetComponent<Image>();
            card = transform.Find("card").GetComponent<Image>();
            cardy = card.transform.localPosition.y;
            if (transform.Find("mask") != null)
                mask = transform.Find("mask").GetComponent<Image>();
            if (transform.Find("ting") != null)
                ting = transform.Find("ting").GetComponent<SpritesList>();
            huDirection = transform.Find("direction").GetComponent<Image>();

        }

        /// <summary>
        /// 设置牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="postion">位置</param>
        /// <param name="isMoCard">是否是摸牌</param>
        public virtual void setCard(bool isHu, int card, int postion, MJMOCard moCard, bool isSelect, TingCardsInfo tinginfo, int distype, bool hasDistypeCard, int sign_tang_count, int sign_index)
        {
            
          
        }
        /// <summary>
        /// 换牌阶段调用该方法
        /// </summary>
        /// <param name="card"></param>
        /// <param name="postion"></param>
        public virtual void setCardRePlace(int card, int postion, int total, int replacenum)
        {
           
        }

        /// <summary>
        /// 总的牌数量
        /// </summary>
        /// <param name="count"></param>
        public virtual void setTotalNum(int count)
        {
            
        }

        /// <summary>
        /// 碰牌后刷新手牌
        /// </summary>
        public virtual void pongRefreshHand()
        {

        }

        public int getCardValue()
        {
            return cardValue;
        }

        protected override void xrefresh()
        {
            
        }

        /// <summary>
        /// 刷新明牌
        /// </summary>
        public virtual void refreshBright() {

        }

        public void cardUp(bool isUp)
        {
            isSelect = isUp;
            float y = 0;
            if (isUp)
                y = 40;
            DisplayKit.setLocalY(gameObject, y);
        }
      

        public virtual int getdirection()
        {
            switch (MahJongPanel.getPlayerIndex(moCard.index))
            {
                case MahJongPanel.LOC_DOWN:
                    return MJPlayerFixedBar.DOWN_DERECTION;
                case MahJongPanel.LOC_RIGHT:
                    return MJPlayerFixedBar.RIGHT_DERECTION;
                case MahJongPanel.LOC_TOP:
                    return MJPlayerFixedBar.TOP_DERCTION;
                case MahJongPanel.LOC_LEFT:
                    return MJPlayerFixedBar.LEFT_DIRECTION;
            }
            return 0;
        }

        protected virtual void down()
        {

        }

        protected virtual void right()
        {
            
        }

        protected virtual void top()
        {
            
        }

        protected virtual void left()
        {
           
        }
    }
}
