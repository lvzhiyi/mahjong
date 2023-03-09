namespace platform.poker
{
    /// <summary> 右边手牌（明牌） </summary>
    public class PKHandsLeft : UnrealLuaSpaceObject
    {
        UnrealDivTableContainer container;

        int[] cards;

        bool mingpai;

        bool dizhu;

        protected override void xinit()
        {
            mingpai = false;
            cards = new int[0];

            container = this.transform.Find("single").GetComponent<UnrealDivTableContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            int len = cards.Length;
            container.resize(len);
            for (int i = 0; i < cards.Length; i++)
            {
                var bar = (PKOtherHandBar)container.bars[i];
                if (i == cards.Length - 1)
                {
                    bar.setMingPai(mingpai);
                    bar.setDiZhu(dizhu);
                }
                bar.setCard(cards[i]);
                bar.index_space = i;
                bar.refresh();
            }
            if (!gameObject.activeInHierarchy) this.setVisible(mingpai);
        }

        /// <summary> 设置明牌 </summary>
        public void setMingPai(bool mingpai)
        {
            this.mingpai = mingpai;
        }

        public void setDiZhu(bool dizhu)
        {
            this.dizhu = dizhu;
        }

        public void setCards(int[] cards)
        {
            this.cards = cards;
        }
    }
}