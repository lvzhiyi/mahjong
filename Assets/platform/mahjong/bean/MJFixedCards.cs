using cambrian.common;
using platform.spotred;
using System;

namespace platform.mahjong
{
    /// <summary>
    /// 固定牌区的牌
    /// </summary>
    public class MJFixedCards:FixedCards
    {
        public MJFixedCards() : base()
        {

        }

        public MJFixedCards(int type,int[] cards) : base(type,cards)
        {

        }

        /// <summary>
        /// 增加一张相同的牌(巴杠的时候)
        /// </summary>
        public override void addSameCards(int card)
        {
            int len = this.cards.Length + 1;
            int oldCard = cards[0];
            cards = new int[len];
            for (int i = 0; i < len-1; i++)
            {
                this.cards[i] = oldCard;
            }
            cards[len - 1] = card;
            this.type = MJKONG_SUP;
        }

        public virtual void updateLastCard(int card,int type)
        {
            if (cards == null) return;
            cards[cards.Length - 1] = card;
            this.type = type;
        }

        public override bool isSameCard(int card, int count)
        {
            if (cards.Length != count) return false;
            if (MJCard.isSigned(card, MJCard.SIGN_HU))
                card = MJCard.cancelSign(card, MJCard.SIGN_HU);
            for (int i = 0; i < cards.Length; i++)
            {
                int c = cards[i];
                if (MJCard.isSigned(c, MJCard.SIGN_HU))
                    c = MJCard.cancelSign(card, MJCard.SIGN_HU); 
                if (c != card) return false;
            }

            return true;
        }

        /// <summary>
        /// 杠牌，变成碰牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="num"></param>
        public virtual void deleteCard(int card,int num)
        {
            int[] c = new int[this.cards.Length-num];
            Array.Copy(cards,0,c,0,c.Length);
            cards = c;
            type = MJPONG;
        }

        /// <summary>
        /// 是否是暗杠
        /// </summary>
        /// <returns></returns>
        public override bool isKongPri()
        {
            return type == MJKONG_PRI;
        }

        public override void bytesRead(ByteBuffer data)
        {
            type = data.readInt();
            source = data.readInt();
            int len = data.readInt();
            cards = new int[len];

            for (int i = 0; i < this.cards.Length; i++)
            {
                cards[i] = data.readInt();
            }
        }

        public override FixedCards clone()
        {
            MJFixedCards fix= new MJFixedCards();
            fix.type = type;
            fix.source = source;
            int len = cards.Length;
            fix.cards = new int[len];
            for (int i=0;i<len;i++)
            {
                fix.cards[i] = cards[i];
            }
            return fix;
        }


        public override string ToString()
        {
            return "type=" + type + ",source=" + source + ",len=" + cards.Length + ",card=" + (cards.Length == 0 ? "没有牌" : MJCard.getName(cards[0]));
        }
    }
}
