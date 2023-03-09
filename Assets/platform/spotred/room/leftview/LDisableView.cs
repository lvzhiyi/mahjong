using UnityEngine;

namespace platform.spotred.room
{
    public class LDisableView: BaseDisableView
    {
        private UnrealDivTableContainer container;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("left").GetComponent<UnrealDivTableContainer>();
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
                LDisableBar bar = (LDisableBar)this.container.bars[i];
                bar.index_space = i;
                bar.card = cards[i];
                bar.setLocalRotation(125);
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
                LDisableBar bar = (LDisableBar)this.container.bars[i];
                bar.index_space = i;
                bar.card = cards[i];
                bar.setLocalRotation(125);
                bar.refresh();
                if(i==cards.Length-1)
                    bar.setVisible(false);
            }
        }
    }
}
