using UnityEngine;

namespace platform.poker
{
    /// <summary> 记牌器 </summary>
    public class PKRecorderView : UnrealLuaSpaceObject
    {
        protected Transform content;

        protected UnrealTextField card_king_max;
        protected UnrealTextField card_king_min;
        protected UnrealTextField card_2;
        protected UnrealTextField card_a;
        protected UnrealTextField card_k;
        protected UnrealTextField card_q;
        protected UnrealTextField card_j;
        protected UnrealTextField card_10;
        protected UnrealTextField card_9;
        protected UnrealTextField card_8;
        protected UnrealTextField card_7;
        protected UnrealTextField card_6;
        protected UnrealTextField card_5;
        protected UnrealTextField card_4;
        protected UnrealTextField card_3;

        protected UnrealTextField[] cards;

        protected int[] cardscount;

        protected bool isShow;

        protected override void xinit()
        {
            content = transform.Find("content").transform;

            cards = new UnrealTextField[15];

            cards[14] = card_king_max = GetComponent("card_king_max");
            cards[13] = card_k = GetComponent("card_k");
            cards[12] = card_q = GetComponent("card_q");
            cards[11] = card_j = GetComponent("card_j");
            cards[10] = card_10 = GetComponent("card_10");
            cards[9] = card_9 = GetComponent("card_9");
            cards[8] = card_8 = GetComponent("card_8");
            cards[7] = card_7 = GetComponent("card_7");
            cards[6] = card_6 = GetComponent("card_6");
            cards[5] = card_5 = GetComponent("card_5");
            cards[4] = card_4 = GetComponent("card_4");
            cards[3] = card_3 = GetComponent("card_3");
            cards[2] = card_2 = GetComponent("card_2");
            cards[1] = card_a = GetComponent("card_a");
            cards[0] = card_king_min = GetComponent("card_king_min");
        }

        protected new virtual UnrealTextField GetComponent(string path)
        {
            var tmep = content.Find(path).GetComponent<UnrealTextField>();
            tmep.init();
            return tmep;
        }

        public virtual void setData(int[] cardscount) { }

        public virtual void showHideOff() { }

        protected void claerCards()
        {
            if (cardscount == null) cardscount = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                cardscount[i] = 0;
            }
        }
    }
}
