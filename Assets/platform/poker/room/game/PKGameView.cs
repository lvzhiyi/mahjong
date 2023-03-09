namespace platform.poker
{
    /// <summary> 扑克 游戏中视图 </summary>
    public class PKGameView : UnrealLuaSpaceObject
    {
        /// <summary> 闹钟刷新 </summary>
        public PKClockView clock { get; protected set; }

        /// <summary> 操作区 </summary>
        public PKOperateView operate { get; protected set; }

        /// <summary> 手牌刷新 </summary>
        public PKAllHandView hand { get; protected set; }

        /// <summary> 发牌动画 </summary>
        public PKDealPokersView dealpoker { get; protected set; }

        /// <summary> 桌面刷新 </summary>
        public PKDesktopView desktop { get; protected set; }

        /// <summary> 玩家状态显示 </summary>
        public PKPlayerStatusView status { get; protected set; }

        /// <summary> 阶段状态显示 </summary>
        public PKStageStatusView stage { get; protected set; }

        /// <summary> 记牌器显示 </summary>
        public PKRecorderView recorder { get; protected set; }

        /// <summary> 记牌器显示 </summary>
        public PKLandlordPokerView landlordcards { get; protected set; }

        /// <summary> 动画播放 </summary>
        public PKAnimationPlayView animator { get; protected set; }

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
    }
}
