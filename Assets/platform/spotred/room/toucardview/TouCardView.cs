namespace platform.spotred.room
{
    /// <summary>
    /// 偷牌view
    /// </summary>
    public class TouCardView:UnrealLuaSpaceObject
    {
        private UnrealTableContainer container;

        private int[] cards;


        protected override void xinit()
        {
            this.container = this.transform.Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
        }

        public void setCards(int[] cards)
        {
            this.cards = cards;
        }


        protected override void xrefresh()
        {
            this.container.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                TouCardBar bar = (TouCardBar)this.container.bars[i];
                bar.setCard(cards[i]);
                bar.refresh();
            }
        }
    }
}
