using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class MJPlayerCards:PlayerCards
    {
        /** 玩家状态：已加飘 */
        public const int STATUS_PIAO = 1 << 0;
        /** 玩家状态：已换牌 */
        public const int STATUS_REPLACE = 1 << 1;
        /** 玩家状态：已定缺 */
        public const int STATUS_DISTYPE = 1 << 2;
        /** 玩家状态：已报牌 */
        public const int STATUS_BAOPAI = 1 << 3;
        /** 玩家状态：已胡牌 */
        public const int STATUS_HU = 1 << 4;
        /** 玩家状态：已选飘 */
        public const int STATUS_PIAO_SELECT = 1 << 7;
        /// <summary>玩家状态：躺牌</summary>
        public const int STATUS_TANG = 1 << 8;
        /// <summary>玩家状态：听牌</summary>
        public const int STATUS_TING = 1 << 9;
        /// <summary> 玩家状态：天地胡(是否有天地胡)</summary>
        public const int STATUS_TIAN_DI_HU = 1 << 10;

        /// <summary> 摸的牌</summary>
        public int mocard = MJCard.CNO;
        /// <summary>定缺的牌</summary>
        public int distype = MJCard.CNO;
        /// <summary>0 表示没胡牌 1 表示第一个胡牌</summary>
        public int huIndex = 0;
        /// <summary>胡牌类型(自摸,还是点炮，抢杠)</summary>
        public int hutype=-1;
        /// <summary>胡牌的来源</summary>
        public int huSource;
        /// <summary>结算的时候使用</summary>
        public MahjongCount mjcount;
        /// <summary>
        /// 换三张
        /// </summary>
        public int[] huansz;

        /// <summary>结算详情</summary>
        public Settle[] accos { get; set; }

        public MJPlayerCards() : base()
        {
            setStatus(STATUS_TIAN_DI_HU);
        }

        /// <summary>
        /// 设置定缺的牌
        /// </summary>
        /// <param name="distype"></param>
        public void setDistype(int distype)
        {
            this.distype = distype;
        }

        public void setMOCard(int card)
        {
            mocard = card;
        }

        public int getMoCard()
        {
            return mocard;
        }

        /// <summary>
        /// 设置飘状态
        /// </summary>
        public void setPiao()
        {
            setStatus(STATUS_PIAO);
        }

        public override void dealPlayerCards(int[] cards)
        {
            handcards.add(sortCards(cards, false));
        }

        public override ArrayList<int> addHszCards(int[] c)
        {
            huansz = sortCards(c, false);
            handcards.add(c);
            int[] cards = sortCards(handcards.toArray(), false);
            handcards.clear();
            handcards.add(cards);
            return handcards;
        }

        /// <summary>
        /// 胡牌
        /// </summary>
        /// <param name="card">牌</param>
        /// <param name="index">索引</param>
        /// <param name="hutype">类型</param>
        public void hu(int card,int index,int hutype)
        {
            mocard = card;
            this.huIndex = index;
            this.hutype = hutype;
        }

        /// <summary>
        /// 自摸亮牌
        /// </summary>
        /// <param name="zimoShowCard"></param>
        /// <returns></returns>
        public virtual MJMOCard getMJMoCard(bool zimoShowCard)
        {
            if (mocard == MJCard.CNO) return null;
            int card = mocard;
            if (MJCard.isSigned(mocard, MJCard.SIGN_HU))
                card = MJCard.cancelSign(mocard,MJCard.SIGN_HU);
            return new MJMOCard(mocard,huIndex,hutype, huSource,zimoShowCard);
        }

        /// <summary>
        /// 是否是定缺的牌  TYPE_DOT=0,TYPE_BAMBOO=1,TYPE_CHARACTER=2; 3-筒，4-条，5-万 
        /// </summary> 
        public bool isDingType(int card)
        {
            if (distype == MJCard.CNO) return false;
            int cardtype = MJCard.getType(card);
            if (distype == cardtype)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否有定缺的牌
        /// </summary>
        /// <returns></returns>
        public bool hasDingCard()
        {
            if (distype == MJCard.CNO)
                return false;
            if (mocard != MJCard.CNO&& distype == MJCard.getType(mocard))
                return true;

            for (int i=0;i<handcards.count;i++)
            {
                if (MJCard.getType(handcards.get(i)) == distype)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 对手牌进行排序 从小到大
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="isDing"></param> 是否需要按定缺排序
        /// <returns></returns>
        protected virtual int[] sortCards(int[] cards, bool isDing)
        {
            cards = sortArray(cards);
            ArrayList<int> ding = new ArrayList<int>();
            if (isDing)
            {
                if (distype != MJCard.CNO)
                    for (int i = 0; i < cards.Length; i++)
                    {
                        if (isDingType(cards[i]))
                        {
                            ding.add(cards[i]);
                            cards[i] = 0;
                        }
                    }
            }

            ArrayList<int> tang = new ArrayList<int>();

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != MJCard.CIN && MJCard.isSigned(cards[i], MJCard.SIGN_TANG))
                {
                    tang.add(cards[i]);
                    cards[i] = 0;
                }
            }

            ArrayList<int> c = new ArrayList<int>();

            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != 0)
                {
                    c.add(cards[i]);
                }
            }

            cards = c.toArray();
            c.clear();
            c.add(cards);

            if (tang.count > 0)
            {
                c.add(tang.toArray());
            }

            if (ding.count > 0)
                c.add(ding.toArray());

            return c.toArray();
        }

        /// <summary>
        /// 手牌排序，摸牌区也要参与。
        /// </summary>
        /// <param name="b">是否将排序好的最后一张牌，加入到摸牌区</param>
        public void handCardSort(bool b)
        {
            addMoCard();
            if (b)
            {
                mocard = handcards.getLast();
                handcards.removeLast();
            }
        }

        public void handCardSort1()
        {
            addMoCard();
            mocard = handcards.getLast();
            handcards.removeLast();
        }
      

        public virtual int[] sortArray(int[] cards)
        {
            int[] temps = cards;
            //先对手牌进行排序 
            for (int i = 0; i < temps.Length; i++)
            {
                for (int j = temps.Length - 1; j > 0; j--)
                {
                    if (temps[j] < temps[j - 1])
                    {
                        int temp = temps[j];
                        temps[j] = temps[j - 1];
                        temps[j - 1] = temp;
                    }
                }
            }
            return temps;
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

        protected virtual void sortHandCard(int[] cards)
        {
            cards = sortCards(cards, true);
            handcards.clear();
            handcards.add(cards);
        }

        /// <summary>
        /// 将摸的牌 加入到手牌中
        /// </summary> 
        protected virtual void addMoCard()
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
        public virtual void moveCardIndexToMoCard(int index)
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

        public virtual void moveCardToMoCard(int card)
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
        /// 获取躺牌的索引
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public int[] getTangCardIndex(int[] cards)
        {
            int[] indexs=new int[cards.Length];

            cards = sortCards(cards, false);

            int temp = 0;
            bool b = false;

            for (int i = 0; i < cards.Length; i++)
            {
                b = false;
                for (int j = temp; j < handcards.count; j++)
                {
                    if(cards[i] == handcards.get(j))
                    if (cards[i] == handcards.get(j))
                    {
                        indexs[i]=j;
                        temp = j+1;
                        b = true;
                        break;
                    }
                }

                if (!b)
                    return null;
            }
            return indexs;
        }

        /// <summary>
        /// 获取躺的牌
        /// </summary>
        /// <returns></returns>
        public int[] getTangCards()
        {
            if (!hasStatus(STATUS_TANG)|| hutype!=-1) return null;

            ArrayList<int> list = new ArrayList<int>();
            int card = 0;
            for (int i=0;i<handcards.count;i++)
            {
                card = handcards.get(i);
                if (card!=MJCard.CIN&&MJCard.isSigned(card, MJCard.SIGN_TANG))
                {
                    list.add(MJCard.cancelSign(card, MJCard.SIGN_TANG));
                }
            }
            return list.toArray();
        }

        /// <summary>
        /// 获取躺牌的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] getTangCardValue(int[] index)
        {
            int[] tangcards = new int[index.Length];
            for (int j = 0; j < index.Length; j++)
            {
                for (int i = 0; i < handcards.count; i++)
                {
                    if (i == index[j])
                    {
                        tangcards[j] = handcards.get(i);
                        break;
                    }
                }
            }

            return tangcards;
        }

        public int[] getNOTangCardValue(int[] index)
        {
           ArrayList<int> notang=new ArrayList<int>();

            bool b = false;
            for (int i = 0; i < handcards.count; i++)
            {
                b = false;
                for (int j = 0; j < index.Length; j++)
                {
                    if (i == index[j])
                    {
                        b = true;
                        break;
                    }
                }

                if (!b)
                    notang.add(handcards.get(i));
            }
            return notang.toArray();
        }

        /// <summary>
        /// 标记躺牌的值
        /// </summary>
        /// <param name="cards"></param>
        public void signTang(int[] cards)
        {
            cards = sortCards(cards, false);
            int temp = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = temp; j < handcards.count; j++)
                {
                    if (cards[i] == handcards.get(j))
                    {
                        handcards.set(MJCard.sign(handcards.get(j),MJCard.SIGN_TANG),j);
                        break;
                    }
                }
            }

            addMoCard();
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
        public virtual int[] getMoCardMoveToDestIndex(int card, int index)
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

        public virtual bool isHu()
        {
            if (hasStatus(STATUS_HU)) return true;
            return false;
        }

        public int[] getDeflultHuansanzhang(int index, int cardtype,int replacecount)
        {
            int[] indexs = new int[replacecount];

            int card = 0;

            int count = 0;

            for (int i=0;i<handcards.count;i++)
            {
                card = handcards.get(i);

                if (MJCard.getType(card) == cardtype)
                {
                    indexs[count] = i;
                    count++;
                }

                if (count > indexs.Length-1) return indexs; 
            }

            if (mocard != MJCard.CNO&& MJCard.getType(mocard) == cardtype)
                indexs[2] =handcards.count;
            return indexs;
        }

        /// <summary>
        /// 是否还有定缺的牌
        /// </summary>
        /// <returns></returns>
        public virtual bool isHaveDingTypeCard()
        {
            for (int i = 0; i < handcards.count; i++)
            {
                if (distype == MJCard.getType(handcards.get(i)))
                    return true;
            }
            return false;
        }

        public bool isHaveDingTypeCardOne()
        {
            int count = getDingTypeCardNum();
            if (count > 1) return true;
            return false;
        }

        public int getDingTypeCardNum()
        {
            int count = 0;
            for (int i = 0; i < handcards.count; i++)
            {
                if (distype == MJCard.getType(handcards.get(i)))
                    count++;
            }
            return count;
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

            for (int i = 0; i < len; i++)
            {
                MJFixedCards fix = new MJFixedCards();
                fix.bytesRead(data);
                this.fixCards.add(fix);
            }

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
        }

        public override PlayerCards clone(PlayerCards pc)
        {
            MJPlayerCards mj = (MJPlayerCards)pc;
            mj.point = point;
            mj.status = status;
            mj.distype = distype;
            mj.hutype = hutype;

            mj.huIndex = huIndex;

            mj.huSource =huSource;
            mj.mocard = mocard;


            for (int i = 0; i < handcards.count; i++)
            {
                mj.handcards.add(handcards.get(i));
            }
            for (int i = 0; i < fixCards.count; i++)
            {
                mj.fixCards.add(fixCards.get(i).clone());
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

        /// <summary>
        /// 总分(单局结算的时候用)
        /// </summary>
        public long totalPoint;

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
                MJFixedCards fix = new MJFixedCards();
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

            accos = new MJSettle[clen];
            int sid=Room.room.getRule().sid;
            if (sid == 2003) //绵阳麻将
                accos = new MYMJSettle[clen];

            for (int i = 0; i < clen; i++)
            {
                
                if (sid == 2003) //绵阳麻将
                    accos[i] = new MYMJSettle();
                else
                    accos[i] = new MJSettle();
                accos[i].bytesRead(data);
            }

            mjcount = new MahjongCount();
            mjcount.bytesRead(data);
        }
    }
}
