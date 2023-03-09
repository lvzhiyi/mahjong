namespace platform.mahjong
{
    /// <summary>
    /// 麻将躺牌视图
    /// </summary>
    public class MJLieView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 选择躺牌，还是过,选择打的牌,选择躺的牌
        /// </summary>
        public const int SELECT_PLAY_CARD = 2, SELECT_LIE_CARD = 3;
        /// <summary>
        /// 选择打的牌
        /// </summary>
        private UnrealLuaSpaceObject selectPlayCard;
        /// <summary>
        /// 选择躺的牌
        /// </summary>
        private UnrealLuaSpaceObject selectlieCard;

        protected override void xinit()
        {
            selectPlayCard = transform.Find("seleccard").GetComponent<UnrealLuaSpaceObject>();
            selectlieCard = transform.Find("selecliecard").GetComponent<UnrealLuaSpaceObject>();
        }

        protected override void xrefresh()
        {
            selectPlayCard.setVisible(false);
            selectlieCard.setVisible(false);
        }

        public void showView(int type)
        {
            xrefresh();
            switch (type)
            {
                case SELECT_PLAY_CARD:
                    selectPlayCard.setVisible(true);
                    break;
                case SELECT_LIE_CARD:
                    selectlieCard.setVisible(true);
                    break;
            }
        }
    }
}
