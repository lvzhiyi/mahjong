using platform.spotred;
using System;
using UnityEngine;

namespace platform.spotred.room
{
    public class BaseFixedView:UnrealLuaSpaceObject
    {
        protected FixedCards[] cards;

        protected OperateCardsView operateCardsView;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("operatecard") != null)
            {
                this.operateCardsView = this.transform.Find("operatecard").GetComponent<OperateCardsView>();
                this.operateCardsView.init();
                this.operateCardsView.setVisible(false);
            }
        }

        public virtual void setOperateCards(FixedCards[] fixedcards, Action action)
        {
            int[] cards = fixedcards[fixedcards.Length - 1].cards;
            this.operateCardsView.setCards(cards);
            this.operateCardsView.startAnimation(refreshCard(fixedcards), action);
            this.operateCardsView.setVisible(true);

        }

        protected int getFixedCardsCount(FixedCards[] fixedcards)
        {
            int count = 0;
            for (int i = 0; i < fixedcards.Length; i++)
            {
                count += fixedcards[i].cards.Length;
            }

            return count;
        }

        public virtual void setCards(FixedCards[] cards)
        {
            this.cards = cards;
        }

        public virtual Vector2 getLastCardsPostion(int index)
        {
            return Vector2.zero;
        }

        public virtual Vector2 refreshCard(FixedCards[] fixedCards)
        {
            return Vector2.zero;
        }
    }
}
