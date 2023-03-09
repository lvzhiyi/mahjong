using System.Collections;

namespace platform.poker
{
    public class PKOperateStatus
    {
        /// <summary> 状态：可以过 </summary> 
        public const int CAN_CANCEL = 1 << 0;

        /// <summary> 状态：可以出牌 </summary> 
        public const int CAN_DISCARD = 1 << 1;

        /// <summary> 状态：可以明牌 </summary> 
        public const int CAN_MING = 1 << 2;

        /// <summary> 状态：可以加倍 </summary> 
        public const int CAN_JIABEI = 1 << 3;

        /// <summary> 状态：叫地主 </summary> 
        public const int CAN_JIAODIZHU = 1 << 4;

        /// <summary> 状态：抢地主 </summary> 
        public const int CAN_QIANGDIZHU = 1 << 5;

        /// <summary> 状态：叫分 </summary> 
        public const int CAN_CALLSCORE = 1 << 6;

        /// <summary> 状态：提示 </summary> 
        public const int CAN_HINT = 1 << 7;


        /// <summary>
        /// 状态：叫朋友叫牌
        /// </summary>
        public const int CAN_JIAODUIYOU = 1 << 9;
        /// <summary>
        /// 状态：叫朋友抢牌
        /// </summary>
        public const int CAN_QIANGPAI = 1 << 10;
        /// <summary>
        /// 状态：叫朋友扯牌
        /// </summary>
        public const int CAN_CHE = 1 << 11;
    }

    /// <summary> 操作组件 </summary>
    public class PKOperateView : UnrealLuaSpaceObject
    {
        /// <summary> 加倍 </summary> 加倍 不加倍
        protected UnrealLuaSpaceObject jiabei;

        /// <summary> 明牌 </summary> 只有明牌
        protected UnrealLuaSpaceObject only_Mingpai;

        /// <summary> 叫地主 </summary> 叫地主 不叫
        protected UnrealLuaSpaceObject call;

        /// <summary> 抢地主 </summary> 抢地主 不抢
        protected UnrealLuaSpaceObject grab;

        /// <summary> 要不起 </summary> 要不起
        protected UnrealLuaSpaceObject only_Pass;

        /// <summary> 叫地主 </summary> 叫地主
        protected UnrealLuaSpaceObject only_call;

        /// <summary> 出牌 </summary> 只能出牌
        protected UnrealLuaSpaceObject only_ShowCards;

        /// <summary> 出牌 </summary> 出牌 明牌
        protected UnrealLuaSpaceObject show_Mingpai;

        /// <summary> 出牌 </summary> 出牌 提示 不要
        protected UnrealLuaSpaceObject show_Normal;

        /// <summary> 出牌 </summary> 出牌 提示
        protected UnrealLuaSpaceObject show_Hint;

        /// <summary> 叫分 </summary> 3分 2分 1分 不要
        protected DDZProcessCallScoreManager call_Score;

        protected virtual T GetComponent<T>(string path) where T : UnrealLuaSpaceObject
        {
            T obj = null;
            if (transform.Find(path))
            {
                obj = transform.Find(path).GetComponent<T>();
                obj.init();
            }
            return obj;
        }

        protected override void xinit() { }

        protected override void xrefresh() { }

        /// <summary> 隐藏操作 </summary>
        public virtual void hideOperateView() { }

        public virtual void showOperateView(int operate, bool isDelay = false) { }

        protected virtual IEnumerator delayShowOperate(int operate) { yield break; }

        /// <summary> 显示操作 </summary>
        protected virtual void showOperateView(int operate) { }
    }
}