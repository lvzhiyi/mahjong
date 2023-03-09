using cambrian.common;
using platform.spotred;
using System;

namespace platform.mahjong
{
    public class AYMJPlayerCards: MJPlayerCards
    {
        public int[] baoKangCard;

        public AYMJPlayerCards() : base()
        {
            baoKangCard = new int[0];
        }

        public void setBaoKong(int[] cards)
        {
            baoKangCard = cards;
            removeHandCard(cards,3);
        }

        public void removeBaoKong(int card)
        {
            if (baoKangCard.Length == 1)
            {
                baoKangCard = new int[0];
                return;
            }

            if (baoKangCard[0] == MJCard.CIN)
            {
                int[] array = new int[baoKangCard.Length-1];
                Array.Copy(baoKangCard,0, array,0,array.Length);
                baoKangCard = array;
                return;
            }


            ArrayList<int> list = new ArrayList<int>();
            for (int i=0;i<baoKangCard.Length;i++)
            {
                if (card != baoKangCard[i])
                {
                    list.add(baoKangCard[i]);
                }
            }

            baoKangCard = list.toArray();
        }

        public void setBaoCard()
        {
            setStatus(STATUS_BAOPAI);
        }

        public override void dealPlayerCards(int[] cards)
        {
            handcards.add(sortCards(cards, false));
        }

        /// <summary>
        /// 获取可以报杠的牌
        /// </summary>
        /// <returns></returns>
        public int[] getCanBaoKongCard()
        {
            int[][] counts= new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            int card = -1;
            for (int i = 0; i < handcards.count; i++)
            {
                card = handcards.get(i);
                counts[MJCard.getType(card)][MJCard.getIndex(card)]++;
            }

            ArrayList<int> cards = new ArrayList<int>();

            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i].Length; j++)
                {
                    if (counts[i][j] == 3)
                    {
                        cards.add(MJCard.CARDS[i][j]);
                    }
                }
            }

            return cards.toArray();
        }





        /// <summary>
        /// 增加牌到手上(已经排好序)
        /// </summary>
        public override ArrayList<int> AddHandCards(int[] cards)
        {
            handcards.add(cards);
            cards =sortCards(handcards.toArray(),false);
            handcards.clear();
            handcards.add(cards);
            return handcards;
        }

        protected override void sortHandCard(int[] cards)
        {
            cards = sortCards(cards, true);
            handcards.clear();
            handcards.add(cards);
        }

        /// <summary>
        /// 将摸的牌 加入到手牌中
        /// </summary> 
        protected override void addMoCard()
        {
            //将 摸的拍加入到手牌 
            if (mocard != MJCard.CNO)
                handcards.add(mocard);
            mocard = MJCard.CNO;
            
            int[] temps = sortCards(handcards.toArray(), true);
            handcards.clear();
            handcards.add(temps);
        }

        /// <summary>
        /// 将指定的牌移动到摸牌区
        /// </summary>
        /// <param name="index"></param>
        public override void moveCardIndexToMoCard(int index)
        {
            int card = 0;
            for (int i = 0; i < handcards.count; i++)
            {
                if (i == index)
                {
                    card = handcards.removeAt(i);
                    break;
                }   
            }

            if (card != 0)
            {
                addMoCard();
                mocard = card;
            }
        }

        public override void moveCardToMoCard(int card)
        {
            if (card == mocard) return;
            for (int i = 0; i < handcards.count; i++)
            {
                if (card == handcards.get(i))
                {
                    handcards.removeAt(i);
                    break;
                }
            }
            addMoCard();
            mocard = card;
        }

        /// <summary>
        /// 移除牌
        /// </summary>
        /// <param name="card">牌</param>
        /// <param name="num">数量</param>
        public override ArrayList<int> delHandCard(int card, int num)
        {
            if (MJCard.isSigned(card, MJCard.SIGN_HU))
                card = MJCard.cancelSign(card, MJCard.SIGN_HU);

            int tempc = card;

            if (handcards.get(0) == MJCard.CIN)
            {
                tempc = MJCard.CIN;
            }

            if (mocard == MJCard.CIN)
            {
                tempc = MJCard.CIN;
            }
            addMoCard();
            for (int i = handcards.count - 1; i >= 0; i--)
            {
                if (num == 0) break;
                if (handcards.get(i) == tempc)
                {
                    handcards.remove(tempc);
                    num--;
                }
            }
            return handcards;
        }

        /// <summary>
        /// 获取摸牌区的牌应该移动的位置
        /// </summary>
        /// <param name="card">打的牌</param>
        /// <param name="index">牌的索引</param>
        /// <returns></returns>
        public override int[] getMoCardMoveToDestIndex(int card, int index)
        {
            if (index >= handcards.count)
            {
                if (mocard == card)
                    mocard = MJCard.CNO;
                return null;
            }
            else
            {
                handcards.removeAt(index);
                int mc = mocard;
                addMoCard();
                for (int i= handcards.count-1; i>=0;i--)
                {
                    if(handcards.get(i) == mc)
                        return new int[] { i, mc };
                }
            }
            return null;
        }

        public override ArrayList<int> delHandCards(int[] cards)
        {
            //这个会在 补杠 和 暗杠时触发  
            if (mocard != MJCard.CNO)
            {
                addMoCard();
                mocard = Card.NO_CARD;
            }
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j=0;j<handcards.count;j++)
                {
                    if (cards[i] == handcards.get(j))
                    {
                        handcards.removeAt(j);
                        break;
                    }
                }
            }
            return handcards;
        }

        public override  bool isHu()
        {
            if (hasStatus(STATUS_HU)) return true;
            return false;
        }

        public override void addFixCard(int type, int[] card)
        {
            MJFixedCards cards = new MJFixedCards(type, card);
            this.fixCards.add(cards);
        }

        public override FixedCards getSameFixedCards(int card, int count)
        {
            for (int i = 0; i < fixCards.count; i++)
            {
                FixedCards fixs = fixCards.get(i);
                if (fixs.isSameCard(card, count)) return fixs;
            }
            return null;
        }

        /// <summary>
        /// 移除手牌
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="num">数量</param>
        private void removeHandCard(int[] cards,int num)
        {
            int count = 0;
            for (int i=0; i<cards.Length;i++)
            {
                count = 0;
                for (int j=handcards.count-1; j>=0;j--)
                {
                    if (cards[i] == handcards.get(j))
                    {
                        handcards.removeAt(j);
                        count++;
                        if (count == num)
                            break;
                    } 
                }
            }
        }

        public void removeBaoKongFixed()
        {
            for (int i = fixCards.count-1; i >=0; i--)
            {
                if (fixCards.get(i).type == MJFixedCards.MJ_BAO_KONG)
                {
                    fixCards.removeAt(i);
                }
            }
        }


        public override void bytesRead(ByteBuffer data)
        {
            handcards.clear();
            disableCards.clear();
            fixCards.clear();
            hutype = data.readInt();
            if (!hasStatus(STATUS_HU))
            {
                hutype = -1;
            }
           
            huIndex = data.readInt();

            huSource = data.readInt();

            int len = data.readInt();

            ArrayList<int> bk = new ArrayList<int>();

            for (int i = 0; i < len; i++)
            {
                MJFixedCards fix = new MJFixedCards();
                fix.bytesRead(data);
                if (fix.type != FixedCards.MJ_BAO_KONG)
                {
                    this.fixCards.add(fix);
                }
                else
                {
                    bk.add(fix.cards[0]);
                }
            }
            baoKangCard = bk.toArray();

            int mo = data.readInt();
            if (mo != MJCard.CNO)
                this.mocard = mo;
            len = data.readInt();
            int[] cards = new int[len];
            
            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }

            sortHandCard(cards);

            len = data.readInt();

            for (int i=0;i<len;i++)
            {
                disableCards.add(data.readInt());
            }
            
           
            removeHandCard(baoKangCard,3);
        }

        public override PlayerCards clone(PlayerCards pc)
        {
            AYMJPlayerCards mj = (AYMJPlayerCards)pc;
            mj.point = point;
            mj.status = status;
            mj.distype = distype;
            mj.hutype = hutype;

            mj.huIndex = huIndex;

            mj.huSource =huSource;
            mj.mocard = mocard;
            int[] baoKong=baoKangCard;
            mj.baoKangCard = new int[baoKong.Length];
            for (int i = 0; i < baoKong.Length; i++)
            {
                mj.baoKangCard[i] = baoKangCard[i];
            }

            for (int i = 0; i < handcards.count; i++)
            {
                mj.handcards.add(handcards.get(i));
            }
            for (int i = 0; i < fixCards.count; i++)
            {
                AYMJFixedCards fixedCards = new AYMJFixedCards();

                FixedCards fc = fixCards.get(i);

                fixedCards.type = fc.type;
                fixedCards.source = fc.source;

                fixedCards.cards = new int[fc.cards.Length];
                for (int j = 0; j < fixedCards.cards.Length; j++)
                {
                    fixedCards.cards[j] = fc.cards[j];
                }
                mj.fixCards.add(fixedCards);
            }
            for (int i = 0; i < disableCards.count; i++)
            {
                mj.disableCards.add(disableCards.get(i));
            }
            if (count != null)
            {
                mj.count = new SpotRedCount();
                mj.count.copy(count);
            }
            return mj;
        }

        public override void bytesReadSingleOver(ByteBuffer data)
        {
            totalPoint = data.readLong();
            point = data.readLong();//这里是单局结算的分数

            count = new SpotRedCount();
            count.score = totalPoint;

            status = data.readInt();
            distype = data.readInt();
            hutype = data.readInt();
            if (!hasStatus(STATUS_HU))
            {
                hutype = -1;
            }

            huIndex = data.readInt();

            huSource = data.readInt();
            int len = data.readInt();
            for (int i = 0; i < len; i++)
            {
                AYMJFixedCards fix = new AYMJFixedCards();
                fix.bytesRead(data);
                this.fixCards.add(fix);
            }

            int mo = data.readInt();
            if (mo != MJCard.CNO)
                mocard = mo;

            len = data.readInt();
            int[] cards = new int[len];

            for (int i = 0; i < len; i++)
            {
                cards[i] = data.readInt();
            }

            sortHandCard(cards);

            int clen = data.readInt();

            accos = new AYMJSettle[clen];

            for (int i = 0; i < clen; i++)
            {
                accos[i] = new AYMJSettle();
                accos[i].bytesRead(data);
            }

            mjcount = new MahjongCount();
            mjcount.bytesRead(data);
        }
    }
}
