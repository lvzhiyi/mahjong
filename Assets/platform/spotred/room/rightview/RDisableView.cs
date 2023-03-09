using UnityEngine;

namespace platform.spotred.room
{
    public class RDisableView: BaseDisableView
    {
        private UnrealDivTableContainer container;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("right").GetComponent<UnrealDivTableContainer>();
            this.container.init();
        }

        protected override void xrefresh()
        {
            if (cards == null)
            {
                this.container.resize(0);
                return;
            }
            this.container.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                RDisableBar bar = (RDisableBar) this.container.bars[i];
                bar.index_space = i;
                bar.card = cards[i];
                bar.setLocalRotation(-113.5f);
                bar.refresh();
                bar.setVisible(true);
            }
        }

        public override Vector3 getLastCardPos()
        {
            Vector3 v = this.container.bars[cards.Length - 1].transform.localPosition;
            return this.transform.localPosition + v;
        }

        public override void hideLastCards()
        {
            if (cards == null)
            {
                this.container.resize(0);
                return;
            }
            this.container.resize(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                RDisableBar bar = (RDisableBar)this.container.bars[i];
                bar.index_space = i;
                bar.card = cards[i];
                bar.setLocalRotation(-113.5f);
                bar.refresh();
                if (i == cards.Length - 1)
                    bar.setVisible(false);
            }
        }
    }
}
