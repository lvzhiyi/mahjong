using System.Collections;

namespace platform.poker
{
    /// <summary> 桌面界面 </summary>
    public class PKDesktopView : UnrealLuaSpaceObject
    {
        /// <summary> 下方玩家出牌 </summary>
        protected PKHandsBean down;

        /// <summary> 左方玩家出牌 </summary>
        protected PKHandsBean left;

        /// <summary> 右方玩家出牌 </summary>
        protected PKHandsBean right;
        /// <summary>
        /// 上架出的牌
        /// </summary>
        protected PKHandsBean top;

        public float delayTime = 0.3f;

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<PKDownHandCard>();
            down.init();

            left = transform.Find("left").GetComponent<PKLeftHandCard>();
            left.init();

            right = transform.Find("right").GetComponent<PKRightHnadCard>();
            right.init();

            if (transform.Find("top") != null)
            {
                top = transform.Find("top").GetComponent<PKRightHnadCard>();
                top.init();
            }
          
        }

        protected override void xrefresh()
        {
            down.setCards(null);
            left.setCards(null);
            right.setCards(null);
            if (top != null)
            {
                top.setCards(null);
                top.refresh();
            }

            down.refresh();
            left.refresh();
            right.refresh();
        }

        /// <summary> 显示玩家出的牌 </summary>
        public virtual void showCards(int pos, int[] cards)
        {
            switch (PKGAME.GetIndex(pos))
            {
                case PKGAME.DOWN:
                    down.setCards(cards);
                    down.refresh();
                    break;
                case PKGAME.LEFT:
                    left.setCards(cards);
                    left.refresh();
                    break;
                case PKGAME.RIGHT:
                    right.setCards(cards);
                    right.refresh();
                    break;
                case PKGAME.TOP:
                    top.setCards(cards);
                    top.refresh();
                    break;
            }                            
        }

        /// <summary> 隐藏所有人的出牌 要不起 等等 </summary>
        public virtual void hideCards(bool isDelay = false) { }

        /// <summary> 隐藏对应玩家位置的出牌 同时隐藏要不起 等等 </summary>
        public virtual void hideCards(int pos) { }

        /// <summary> 延迟 </summary>
        protected virtual IEnumerator delayHideCards() { yield break; }
    }
}
