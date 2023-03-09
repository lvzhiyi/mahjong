﻿using cambrian.common;

namespace platform.poker
{
    public class PDKAnYueCardInfo : PDKCardInfo
    {

        public PDKAnYueCardInfo(int master):base(master)
        {
            this.master = master;
        }

        /// <summary> 牌组信息 </summary>
        public PDKAnYueCardInfo(int master, int[] cards):base(master)
        {
            this.cards = cards;
            this.counts = _cardsToCounts(cards);
            this.master = master;
            PDKAnYueCardType.checkCardsType(this, PDKAnYueMatch.match.rule);
            checkLevel();
        }

        public override PDKCardInfo clone()
        {
            PDKAnYueCardInfo bean = new PDKAnYueCardInfo(index);
            bean.type = type;
            bean.level = level;
            bean.cards = new int[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                bean.cards[i] = cards[i];
            }
            return bean;
        }

        public override bool hasCard(int card)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == card) return true;
            }
            return false;
        }

        public override void checkLevel()
        {
            if (index == 1 || index == 2)
                level = index + 13;
            else if (index == 0)
                level = 16;
            else if (index == 14)
                level = 17;
            else
                level = index;
        }

        public override bool hasBoom()
        {
            for (int i=3;i<counts.Length;i++)
            {
                if (counts[i] > 3)
                    return true;
            }

            return false;
        }

        protected override int[] _cardsToCounts(int[] cards)
        {
            int[] counts = Poker.getEmptyCounts();
            if (cards == null || cards.Length == 0) return counts;
            for (int i = 0, v = 0; i < cards.Length; i++)
            {
                v = Poker.getValue(cards[i]);
                if (Poker.getType(cards[i]) == Poker.TYPE_JOKER)
                {
                    if (v == 1) counts[0] += 1;
                    else counts[14] += 1;
                }
                else counts[v] += 1;
            }
            return counts;
        }

        public override bool isSingle()
        {
            return type == PKCardType.TYPE_CARDS_SINGLE;
        }

        public override void reset()
        {
            counts = cards = null;
            level = master = index = -1;
            type = 0;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(index);
            data.writeInt(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                data.writeInt(cards[i]);
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            int t = data.readInt();
            level = data.readInt();
            int len = data.readInt();
            cards = new int[len];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = data.readInt();
            }
            counts = _cardsToCounts(cards);
            PDKAnYueCardType.checkCardsType(this, Room.room.getRule());
            type = t;
            checkLevel();
        }

        public override bool compareTo(PDKCardInfo info)
        {
            if (type == PKCardType.TYPE_ERROR) return false;
            if (info == null || info.type == PKCardType.TYPE_ERROR) return true;
            if (type == PKCardType.TYPE_CARDS_BOMB && info.type != PKCardType.TYPE_CARDS_BOMB) return true;
            if (type != PKCardType.TYPE_CARDS_BOMB && info.type == PKCardType.TYPE_CARDS_BOMB) return false;
            if (type == PKCardType.TYPE_CARDS_BOMB && index == 1) return true;
            if (cards.Length != info.cards.Length) return false;
            if (type == info.type)
            {
                if (index <= Poker.PK_2 && info.index > Poker.PK_2) return true;
                if (index > Poker.PK_2 && info.index <= Poker.PK_2) return false;
                return index > info.index;
            }
            return false;
        }
    }
}
