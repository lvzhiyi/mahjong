using cambrian.unreal.display;

namespace platform.spotred.room
{
    public class SelectKongCardPanel: UnrealLuaPanel
    {
        /// <summary>
        /// 牌
        /// </summary>
        int[] card;

        private UnrealDivTableContainer container;

        protected override void xinit()
        {
            this.container = transform.Find("Canvas").Find("group").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        public void setData(int[] card)
        {
            this.card = card;
        }

        protected override void xrefresh()
        {
            this.container.resize(card.Length);

            for (int i = 0; i < card.Length; i++)
            {
                KongCardBar bar=(KongCardBar)this.container.bars[i];
                bar.setData(card[i]);
                bar.index_space = i;
                bar.refresh();
            }
        }
    }
}
