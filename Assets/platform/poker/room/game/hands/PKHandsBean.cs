namespace platform.poker
{
    /// <summary> 手牌管理类 </summary>
    public class PKHandsBean : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 地主牌间隙
        /// </summary>
        public const float LandloarSpace = 65f;

        /// <summary>
        /// 玩家手牌间隙
        /// </summary>
        public const float RightLeftSpace = 20f;

        /// <summary>
        /// 其他玩家手牌间隙
        /// </summary>
        public const float DownUpSpace = 30f;

        /// <summary>
        /// 出牌显示间隙
        /// </summary>
        public const float ShowHandSpace = 50;

        /// <summary>
        /// 玩家选中手牌的提高高度
        /// </summary>
        public const float UpHight = 25f;

        /// <summary>
        /// 牌排序起点 顺序 从左往右 从上往下
        /// </summary>
        protected float point;

        /// <summary>
        /// 中心点起始方向  1:从右往左 从上往下 2:从左往右 从下往上
        /// </summary>
        protected int pointDirection;

        /// <summary>
        /// 缩放
        /// </summary>
        protected float handScale = 1f;

        /// <summary>
        /// 方向 1:横轴 左右 2:纵轴 上下 3:横轴 右左 4:纵轴 下上
        /// </summary>
        protected int direction;

        /// <summary>
        /// 牌值
        /// </summary>
        protected int[] cards { get; set; }

        protected UnrealDivTableContainer container;

        public float scaleOne = 0.7f, scaleTwo = 0.55f, scaleThree = 0.45f, scaleFour = 0.4f;

        protected override void xinit()
        {
            container = GetComponentInChildren<UnrealDivTableContainer>();
            container.init();
        }

        public void setCards(int[] cards)
        {
            this.cards = cards;
        }
    }
}
