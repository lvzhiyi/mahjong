namespace platform.mahjong
{
    /// <summary>
    /// 麻将躺牌阶段
    /// </summary>
    public class MJTangStage
    {
        /// <summary>
        /// 选择要打的牌，选择躺的牌
        /// </summary>
        public const int SELECT_PLAY_CARD = 0,SELECT_TANG_CARDS=1;

        public static bool isPlayStage(int stage)
        {
            return stage == SELECT_PLAY_CARD;
        }

        public static bool isTangCardsStage(int stage)
        {
            return stage == SELECT_TANG_CARDS;
        }
    }
}
