namespace platform.poker
{
    /// <summary> 玩家状态管理类 </summary>
    public class PKPlayerStatusView : UnrealLuaSpaceObject
    {
        protected PKPlayerDetailStatusView down, left, right,top;

        public const int CardsWarningNum = 1;

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<PKPlayerDetailStatusView>();
            down.init();

            left = transform.Find("left").GetComponent<PKPlayerDetailStatusView>();
            left.init();

            right = transform.Find("right").GetComponent<PKPlayerDetailStatusView>();
            right.init();
            if(transform.Find("top")!=null)
            {
                top = transform.Find("top").GetComponent<PKPlayerDetailStatusView>();
                top.init();
            }
           
        }

        protected override void xrefresh()
        {
            down.refresh();
            left.refresh();
            right.refresh();
            if (top != null)
                top.refresh();
        }

        /// <summary> 隐藏地主身份 </summary>
        public virtual void hideBanker() { }

        /// <summary> 显示地主身份 </summary>
        public virtual void showBanker(int pos) { }

        /// <summary> 设置牌的数量 </summary>
        public virtual void setCardNum(int pos, int num) { }

        /// <summary> 显示手牌提醒 </summary>
        public virtual void showCardWarning(int pos, bool isShow) { }

        public virtual void showCardWarning(int pos, int num) { }
    }
}
