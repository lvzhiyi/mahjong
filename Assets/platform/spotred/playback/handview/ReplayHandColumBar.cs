namespace platform.spotred.playback
{
    public class ReplayHandColumBar:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 下,右,上,左
        /// </summary>
        public const int BOTTOM = 0, RIGHT = 1, TOP = 2, LEFT = 3;
        /// <summary>
        /// 牌容器
        /// </summary>
        UnrealDivTableContainer container;

        private int[] cards;

        public int postion;

        protected override void xinit()
        {
            this.container = this.transform.Find("single_1").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        public void setData(int[] cards)
        {
            this.cards = cards;
        }

        protected override void xrefresh()
        {
            if (postion == RIGHT || postion == LEFT)
                DisplayKit.setLocalRoateXYZ(this.transform, 0, 0, -90);

            if (postion == TOP || postion == LEFT)
            {
                DisplayKit.setLocalXY(this.transform, -(getWidth() + 5) * index_space, 0);
            }
            else
            {
                DisplayKit.setLocalXY(this.transform, (getWidth() + 5) * index_space, 0);
            }
            
            this.container.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                ReplayHandCardBar bar=(ReplayHandCardBar)this.container.bars[i];
                bar.index_space = i;
                bar.setData(cards[i],cards.Length-1);
                bar.refresh();
            }
        }
    }
}
