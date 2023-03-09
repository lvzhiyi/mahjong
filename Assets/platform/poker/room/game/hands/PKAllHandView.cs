namespace platform.poker
{
    /// <summary> 管理手牌界面 </summary>
    public class PKAllHandView : UnrealLuaSpaceObject
    {
        protected PKHandsDown down;

        protected PKHandsRight right;

        protected PKHandsLeft left;

        protected PKHandsRight top;

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<PKHandsDown>();
            down.init();

            right = transform.Find("right").GetComponent<PKHandsRight>();
            right.init();

            left = transform.Find("left").GetComponent<PKHandsLeft>();
            left.init();

            if (transform.Find("top") != null)
            {
                top = transform.Find("top").GetComponent<PKHandsRight>();
                top.init();
            }
        }

        protected override void xrefresh()
        {
            down.setVisible(false);
            right.setVisible(false);
            left.setVisible(false);
            if (top != null)
                top.setVisible(false);
        }

        /// <summary> 刷新左边手牌 </summary>
        protected virtual void refreshLeft(int[] cards, bool isShow, bool dizhu) { }

        /// <summary> 刷新右边手牌 </summary>
        protected virtual void refreshRight(int[] cards, bool isShow, bool dizhu) { }

        /// <summary> 刷新自己手牌 </summary>
        protected virtual void refreshDown(int[] cards, bool isShow, bool dizhu) { }

        protected virtual void refreshTop(int[] cards,bool isShow,bool dizhu) { }

        /// <summary> 刷新手牌 不带明牌 </summary>
        public virtual void refresHands(int pos, int[] cards) { }

        /// <summary> 刷新手牌 带明牌 </summary>
        public virtual void refresHands(int pos, int[] cards, bool isShow, bool dizhu) { }

        /// <summary> 刷新手牌 隐藏其他玩家手牌身份 </summary>
        public virtual void refresHands(bool isShow, int pos, int[] cards) { }

        /// <summary> 明牌隐藏牌身份 </summary>
        protected virtual void refreshLeft(int[] cards) { }

        /// <summary> 明牌隐藏牌身份 </summary>
        protected virtual void refreshRight(int[] cards) { }

        /// <summary> 明牌隐藏牌身份 </summary>
        protected virtual void refreshDown(int[] cards) { }

        protected virtual void refreshTop(int[] cards){}

        /// <summary> 全部取消选择 </summary>
        public virtual void cancelAllSelect() { }

        /// <summary> 重置明牌状态 </summary>
        public virtual void resetMingPai() { }

        /// <summary> 全部选择遮罩 </summary>
        public virtual void resetMask() { }

        /// <summary> 全部选择遮罩 </summary>
        public virtual void refreshSelectHands(int start, int end) { }

        /// <summary> 提示手牌 </summary>
        public virtual void tipsCards(int[] cards) { }
    }
}
