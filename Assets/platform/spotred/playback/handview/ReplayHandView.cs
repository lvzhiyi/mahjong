using cambrian.common;
using platform.spotred.room;

namespace platform.spotred.playback
{
    /// <summary>
    /// 回放界面手牌
    /// </summary>
    public class ReplayHandView: UnrealLuaSpaceObject
    {
        /// <summary>
        /// 列容器
        /// </summary>
        UnrealDivTableContainer container;

        private int[] cards;
        protected override void xinit()
        {
            this.container = this.transform.Find("single").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        public void showHandCard(int[] cards)
        {
            this.cards = cards;
            bool b = Room.room.roomRule.rule.getRuleAtribute("is25single");

            ArrayList<int[]> list = CardsManager.getHand(cards, b);

            this.container.resize(list.count);

            for (int i = 0; i < list.count; i++)
            {
                int[] cs = list.get(i);
               
                for (int j = 0; j < cs.Length; j++)
                {
                    ReplayHandColumBar bar = (ReplayHandColumBar)this.container.bars[i];
                    bar.index_space = i;
                    bar.setData(cs);
                    bar.refresh();
                }
            }
        }

        protected override void xrefresh()
        {
            if (cards == null)
            {
                this.container.resize(0);
                return;
            }
        }
    }
}
