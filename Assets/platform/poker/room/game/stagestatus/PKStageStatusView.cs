using System.Collections;

namespace platform.poker
{
    /// <summary> 斗地主阶段状态 </summary>
    public class PKStageStatus
    {
        /// <summary> 未知错误 </summary>
        public const int ERROR = -1;

        /// <summary> 叫地主 </summary>
        public const int JIAO_DI_ZHU = 0;

        /// <summary> 不叫 </summary>
        public const int NO_JIAO_DIZHU = 1;

        /// <summary> 抢地主 </summary>
        public const int QIANG_DI_ZHU = 2;

        /// <summary> 不抢 </summary>
        public const int NO_QIANG_DI_ZHU = 3;

        /// <summary> 加倍 </summary>
        public const int JIA_BEI = 4;

        /// <summary> 不加倍 </summary>
        public const int NO_JIA_BEI = 5;

        /// <summary> 要不起 </summary>
        public const int PASS = 6;

        /// <summary> 明牌 </summary>
        public const int MING_PAI = 7;

        /// <summary> 叫分 : 1 </summary>
        public const int CALLSCORE_ONE = 8;

        /// <summary> 叫分 : 2 </summary>
        public const int CALLSCORE_TWO = 9;

        /// <summary> 叫分 : 3 </summary>
        public const int CALLSCORE_THREE = 10;
    }

    /// <summary> 阶段状态 文字 </summary> (要不起，叫地主，不叫.....)
    public class PKStageStatusView : UnrealLuaSpaceObject
    {
        protected PKStageDetailStatusView down;

        protected PKStageDetailStatusView right;

        protected PKStageDetailStatusView left;

        protected PKStageDetailStatusView top;

        public float delay = 0.2f;

        protected override void xinit()
        {
            down = transform.Find("down").GetComponent<PKStageDetailStatusView>();
            down.init();
            right = transform.Find("right").GetComponent<PKStageDetailStatusView>();
            right.init();
            left = transform.Find("left").GetComponent<PKStageDetailStatusView>();
            left.init();
            if (transform.Find("top") != null)
            {
                top = transform.Find("top").GetComponent<PKStageDetailStatusView>();
                top.init();
            }
          
        }

        protected override void xrefresh()
        {
            down.setVisible(false);
            right.setVisible(false);
            left.setVisible(false);
            if (top != null)
            {
                top.setVisible(false);
            }
        }

        /// <summary> 显示某位玩家状态 </summary> 
        public virtual void showStageStatus(int pos, int posType, bool replay = false) { }

        /// <summary> 隐藏所有人玩家状态 </summary>
        public virtual void hidePStatus(bool isDelay = false) { }

        /// <summary> 延迟隐藏 </summary>
        protected virtual IEnumerator delayHideStatus() { yield break; }

        /// <summary> 隐藏玩家状态 </summary> 代表某个玩家 这里是位置
        public virtual void hidePStatus(int pos) { }
    }
}
