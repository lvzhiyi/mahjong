using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class AYMJMatch : MJMatch
    {
        public new static AYMJMatch match { get; set; }
       

        public AYMJMatch()
        {

        }


        public AYMJMatch(int playercount)
        {
            isTianhu = true;
            pcards = new AYMJPlayerCards[playercount];
            for (int i = 0; i < playercount; i++)
            {
                pcards[i] = new AYMJPlayerCards();
            }
            showCards = new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            tangCardsIndex = null;
        }

        public AYMJMatch(int playercount, int step, int banker, int master, int paidui) : this(playercount)
        {
            this.step = step;
            this.banker = banker;
            this.paidui = paidui;
            this.master = master;
        }

        public override void setRoomRule(RoomRule rule)
        {
            base.setRoomRule(rule);
            if (rule == null) return;
            replace = rule.rule.getIntAtribute("replacecount");
        }
       

        public override void DealCards(int[][] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                pcards[i].dealPlayerCards(cards[i]);
            }
        }


        public override int[] GetMathPlayerHand(int index)
        {
            return pcards[index].handcards.toArray();
        }

        public override int[] getSelfHandCard()
        {
            return pcards[mindex].handcards.toArray();
        }

        public override SpotRedCount[] getSpotRedCounts()
        {
            ArrayList<SpotRedCount> list = new ArrayList<SpotRedCount>(pcards.Length);
            for (int i = 0; i < pcards.Length; i++)
            {
                list.add(pcards[i].count);
            }
            return list.toArray();
        }

        public override ArrayList<int> removeHandCard(int index, int card, int count)
        {
            return pcards[index].delHandCard(card, count);
        }

        /// <summary>
        /// 给庄家增加一张牌
        /// </summary>
        /// <param name="card"></param>
        public override void addBankerCard(int card)
        {
            getPlayerCardIndex<AYMJPlayerCards>(banker).setMOCard(card);
        }

        /// <summary>
        /// 获取手牌中每种牌型的数量(筒，条，万)
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="type">牌的类型</param>
        /// <returns></returns>
        public override int getHandCardTypeCount(int index, int type)
        {
            AYMJPlayerCards playerCards= getSelfPlayerCards<AYMJPlayerCards>();
            ArrayList<int> cards = playerCards.handcards;
            int mocard = playerCards.mocard;
            int count = 0;
            for (int i = 0; i < cards.count; i++)
            {
                if (MJCard.getType(cards.get(i)) == type)
                {
                    count++;
                }
            }
            if (mocard != MJCard.CNO && MJCard.getType(mocard) == type)
                count++;

            return count;
        }


        /// <summary>
        /// 获取胡牌类型
        /// </summary>
        /// <returns></returns>
        protected override int getHuType(int index)
        {
            int hutype = MJMatchHuOperate.HU_NORMAL;
            AYMJPlayerCards playerCard = getPlayerCardIndex<AYMJPlayerCards>(index);
            if (isRound(index))
            {
                if (isTianhu)
                {
                    hutype = MJMatchHuOperate.HU_TIAN;
                }
                else if (isKongSup)
                {
                    hutype = MJMatchHuOperate.HU_KONG_SELF;
                }
                else if (paidui == 0) //海底
                {
                    hutype = MJMatchHuOperate.HU_HAIDI| MJMatchHuOperate.HU_SELF;
                }
                else
                {
                    hutype = MJMatchHuOperate.HU_SELF;
                }
            }
            else
            {

                if (isTianhu)
                {
                    hutype = MJMatchHuOperate.HU_DI;
                    if (isKongSup)
                        hutype |= MJMatchHuOperate.HU_KONG;
                }
                else if (isKongSup)
                {
                    if (kongType == MJMatchKongOperate.KONG_PRI || kongType == MJMatchKongOperate.KONG_PUB ||
                        kongType == MJMatchKongOperate.KONG_SUP_EXIT_ROB || kongType == MJMatchKongOperate.KONG_SUP)
                        hutype = MJMatchHuOperate.HU_KONG;
                    else if (kongType == MJMatchKongOperate.KONG_SUP_WAIT_ROB)
                    {
                        hutype = MJMatchHuOperate.HU_ROB;
                        if (paidui == 0)
                            hutype |= MJMatchHuOperate.HU_HAIDI;
                    }
                }
                else if (paidui == 0) //海底炮
                {
                    hutype = MJMatchHuOperate.HU_HAIDI;
                }
            }
            return hutype;
        }

        /// <summary>
        /// 获取具体的操作需要的数据
        /// </summary>
        /// <param name="operate"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public override MJOperateInfoBean[] getMJOperateInfos(int operate, int card, bool isGang, int index)
        {
            ArrayList<MJOperateInfoBean> list = new ArrayList<MJOperateInfoBean>();
            MJOperateInfoBean bean;
            if (StatusKit.hasStatus(operate, MJOperate.CAN_HU))
            {
                bean = new MJOperateInfoBean();
                int point = getHuFan(card, getHuType(index), index); //番数
                int num = this.getCardLastNum(card);
                if (num == 0 && rule.rule.getRuleAtribute("enableOnlyOne"))
                    point++;
                bool b = getSelfPlayerCards<AYMJPlayerCards>().hasStatus(MJPlayerCards.STATUS_BAOPAI);
                if (b)
                    point++;
                bean.setHu(card, point);
                list.add(bean);
            }

            if (StatusKit.hasStatus(operate, MJOperate.CAN_KONG))
            {
                bean = new MJOperateInfoBean();
                if (isRound(index))
                {
                    bean.setSelfKong(getSelfRoundKongs(index));
                }
                else
                {
                    bean.setKong(card);
                }

                list.add(bean);
            }

            if (StatusKit.hasStatus(operate, MJOperate.CAN_PONG))
            {
                bean = new MJOperateInfoBean();
                bean.setPong(card);
                list.add(bean);
            }

            if (StatusKit.hasStatus(operate, MJOperate.CAN_BAOPAI))
            {
                bean = new MJOperateInfoBean();
                bean.setBao();
                list.add(bean);
            }

            if (StatusKit.hasStatus(operate, MJOperate.CAN_BAO_KONG))
            {
                bean = new MJOperateInfoBean();
                bean.setBaoKong();
                list.add(bean);
            }


            if (StatusKit.hasStatus(operate, MJOperate.CAN_CANCEL))
            {
                bean = new MJOperateInfoBean();
                bean.setCancel();
                list.add(bean);
            }

            return list.toArray();
        }

       

        /// <summary>
        /// 获取胡牌的番数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="isGang"></param>
        /// <returns></returns>
        public override int getHuFan(int card,int huType,int index)
        {
            AYMJPlayerCards pc = getPlayerCardIndex<AYMJPlayerCards>(index);
            int[] baoKong=pc.baoKangCard;
            for (int i = 0; i < baoKong.Length; i++)
            {
                AYMJFixedCards f = new AYMJFixedCards(MJFixedCards.MJ_BAO_KONG, new int[3] { baoKong[i], baoKong[i], baoKong[i] });
                pc.fixCards.add(f);
            }
            AYMJCardsCounter info = new AYMJCardsCounter(rule.rule, pcards[index].handcards, pcards[index].fixCards,paidui);
            if (isOneFangshu())
                info = new AYMJCards2r1fCounter(rule.rule, pcards[index].handcards, pcards[index].fixCards,paidui);
            info.setHandCardSize(getSingleCount(index));
            info.addHandCard(card, 1);
            int fan=info.getHuPoint(huType, card, pcards[index].fixCards);
            pc.removeBaoKongFixed();
            return fan;
        }

        /// <summary>
        /// 是否是金勾勾，或者大单吊(摸牌区的牌不能算的，因为有自摸的情况)
        /// </summary>
        /// <returns></returns>
        protected override bool isSingle(int index)
        {
            int count= getPlayerCardIndex<AYMJPlayerCards>(index).handcards.count;

            if (isRound(index)&&count>1)//这里主要时判断该自己出牌时，是否满足金钩钩的条件
                count--;
            return count == 1;
        }

        /// <summary>
        /// 是否是金勾勾，或者大单吊(摸牌区的牌不能算的，因为有自摸的情况)
        /// </summary>
        /// <param name="index"></param>
        /// <returns>当前手牌数量</returns>
        private int getSingleCount(int index)
        {
            int count = getPlayerCardIndex<AYMJPlayerCards>(index).handcards.count;
            if (isRound(index) && count > 1)//这里主要时判断该自己出牌时，是否满足金钩钩的条件
                count--;
            return count;
        }


        #region 换三张

        /// <summary>
        /// 获取换三张最少牌的类型
        /// </summary>
        /// <returns></returns>
        public override int getHuanSZMinCardType()
        {
            int len = 3;
            //只有三张牌型
            int[] count = new int[len];

            count[0] = getHandCardTypeCount(mindex, MJCard.TYPE_DOT);
            count[1] = getHandCardTypeCount(mindex, MJCard.TYPE_BAM);
            count[2] = getHandCardTypeCount(mindex, MJCard.TYPE_CHA);

            for (int i = 0; i < len; i++)
            {
                if (count[i] < replace)
                    count[i] = 0;
            }

            int index = -1;
            while ((--len) >= 0)
            {
                if (count[len] == 0) continue;
                if (index == -1)
                {
                    index = len;
                }
                else if (count[len] < count[index])
                {
                    index = len;
                }
            }

            return getCardType(index);
        }

        /// <summary>
        /// 获取换三张的牌
        /// </summary>
        /// <returns></returns>
        public override int[] getHuanSanZhangCards()
        {
            if (huangSanZhangIndex == null)
            {
                huangSanZhangIndex = getSelfPlayerCards<AYMJPlayerCards>().getDeflultHuansanzhang(0, getHuanSZMinCardType(),replace);
            }

            int[] cards = new int[replace];
            for (int i = 0; i < replace; i++)
            {
                if (huangSanZhangIndex[i] == -1) return null;
                if (this.huangSanZhangIndex[i] == this.pcards[this.mindex].handcards.count)
                {
                    cards[i] = getSelfPlayerCards<AYMJPlayerCards>().mocard;
                }
                else
                {
                    cards[i] = pcards[mindex].handcards.get(this.huangSanZhangIndex[i]);
                }
            }
            return cards;
        }

        /// <summary>
        /// 获取换三张牌的索引
        /// </summary>
        /// <returns></returns>
        public override int[] getHuanSZIndex()
        {
            if (huangSanZhangIndex == null)
            {
                huangSanZhangIndex = getSelfPlayerCards<AYMJPlayerCards>().getDeflultHuansanzhang(0, getHuanSZMinCardType(),replace);
            }

            return huangSanZhangIndex;
        }

        /// <summary>
        /// 添加一张牌的索引到换三张数组中
        /// </summary>
        /// <param name="index"></param>
        public override bool addHuangSZIndex(int index)
        {
            if (huangSanZhangIndex == null)
            {
                huangSanZhangIndex = new int[replace];
                for (int i = 0; i < huangSanZhangIndex.Length; i++)
                {
                    huangSanZhangIndex[i] = -1;
                }
            }

            int tempIndex = -1;
            int num = 0;
            for (int i = 0; i < replace; i++)
            {
                if (huangSanZhangIndex[i] != -1)
                {
                    num++;
                    tempIndex = huangSanZhangIndex[i];
                }
            }

            AYMJPlayerCards cards = getSelfPlayerCards<AYMJPlayerCards>();
            if (num == replace)//换牌数量一致
            {
                if (rule.rule.getRuleAtribute("replaceSame"))
                {
                    int card=0;

                    if (index == cards.handcards.count)
                        card = cards.mocard;
                    else
                        card = cards.handcards.get(index);

                    int card2;
                    if (huangSanZhangIndex[0] == cards.handcards.count)
                        card2 = cards.mocard;
                    else
                        card2 = cards.handcards.get(huangSanZhangIndex[0]);

                    if (MJCard.getType(card) == MJCard.getType(card2))
                    {
                        return false;
                    }

                    huangSanZhangIndex = new int[replace];
                    for (int i = 0; i < huangSanZhangIndex.Length; i++)
                    {
                        huangSanZhangIndex[i] = -1;
                    }
                    num = 0;
                }
                else
                {
                    return false;
                }
               
            }
                

            //里面没牌
            if (num == 0)
            {
                huangSanZhangIndex[0] = index;
            }
            else
            {
                int card;
               
                if (tempIndex == cards.handcards.count)
                    card = cards.mocard;
                else
                    card = cards.handcards.get(tempIndex);

                int card2;
                if (index == cards.handcards.count)
                    card2 = cards.mocard;
                else
                    card2 = cards.handcards.get(index);

                if (MJCard.getType(card) != MJCard.getType(card2)&&rule.rule.getRuleAtribute("replaceSame"))//类型不相同
                    return false;
                for (int i = 0; i < replace; i++)
                {
                    if (huangSanZhangIndex[i] == -1)
                    {
                        huangSanZhangIndex[i] = index;
                        return true;
                    }
                }
            }
            return false;
        }

        //移除一张索引
        public override void deleteHuansanzhangIndex(int index)
        {
            for (int i = 0; i < replace; i++)
            {
                if (huangSanZhangIndex[i] == index)
                {
                    huangSanZhangIndex[i] = -1;
                    return;
                }
            }
        }

        #endregion

        #region 定缺
        /// <summary>
        /// 获取推荐的定缺类型
        /// </summary>
        /// <returns></returns>
        public override int[] getRecommendType()
        {
            int len = 3;
            int[] count = new int[len];

            count[0] = getHandCardTypeCount(mindex, MJCard.TYPE_DOT);
            count[1] = getHandCardTypeCount(mindex, MJCard.TYPE_BAM);
            count[2] = getHandCardTypeCount(mindex, MJCard.TYPE_CHA);

            ArrayList<int> types = new ArrayList<int>(2);

            int mincount = 0;

            for (int i = 0; i < count.Length; i++)
            {
                if (i == 0)
                {
                    mincount = count[i];
                }
                else if (mincount > count[i])
                {
                    mincount = count[i];
                }
            }

            for (int i = 0; i < count.Length; i++)
            {
                if (mincount == count[i])
                {
                    types.add(getCardType(i));
                }
            }
            
            return types.toArray();
        }

        #endregion

        #region 灯泡提示

        /// <summary>
        /// 点击灯泡提示听的牌
        /// </summary>
        /// <returns></returns>
        public override TingCardsInfo getDengTingCards(int index)
        {
            if (!rule.rule.getRuleAtribute("hucue")) return null;
            return getDengTingCards(0, index);
        }

        /// <summary>
        /// 获得可以听的牌
        /// </summary>
        /// <param name="card">选择出的牌</param>
        /// <returns></returns>
        public override TingCardsInfo getDengTingCards(int huType, int index)
        {
            TingCardsInfo tcardsr = getDengAccoBySendCard(huType, index);
            if (tcardsr == null)
                return null;
            ArrayList<TingCard> list = tcardsr.getTingList();
            int num = 0;
            TingCard tingCard = null;
            for (int i = 0; i < list.count; i++)
            {
                tingCard = list.get(i);
                num = this.getCardLastNum(tingCard.card);
                tingCard.num = num;
                if (rule.rule.getRuleAtribute("enableOnlyOne")&&num==1)
                    tingCard.setFan(tingCard.getMultiple()+1);
                tcardsr.addCount(tingCard.num);
            }

            return tcardsr;
        }


        /// <summary>
        /// 获取自己报杠的牌
        /// </summary>
        /// <returns></returns>
        public int[] getSelfBaoKongCard()
        {
            AYMJPlayerCards playerCards= getSelfPlayerCards<AYMJPlayerCards>();

            int[] canBaoKong= playerCards.getCanBaoKongCard();

            int[] curhandCard = playerCards.handcards.toArray();//当前的手牌

            ArrayList<int> list = new ArrayList<int>();//移除了需要报杠的牌

            ArrayList<int> baoKong = new ArrayList<int>();
            for (int i=0;i<canBaoKong.Length;i++)
            {
                list.clear();
                for (int j=0;j<curhandCard.Length;j++)
                {
                    if (curhandCard[j] != canBaoKong[i])
                        list.add(curhandCard[j]);
                }

                AYMJCardsCounter info = new AYMJCardsCounter(rule.rule, list, playerCards.fixCards, paidui);
                if (isOneFangshu())
                    info = new AYMJCards2r1fCounter(rule.rule, list, playerCards.fixCards, paidui);
                info.setHandCardSize(list.count);
                ArrayList<FixedCards> fixCards = playerCards.fixCards;
                int[] array = info.checkOutListens(fixCards);
                if (array != Single.EMPTY_INT_ARRAY)
                {
                    baoKong.add(canBaoKong[i]);
                }
            }
            return baoKong.toArray();
        }

        public bool isBaoKong(int[] canBaoKong)
        {
            AYMJPlayerCards playerCards = getSelfPlayerCards<AYMJPlayerCards>();

            int[] curhandCard = playerCards.handcards.toArray();//当前的手牌

            ArrayList<int> list = new ArrayList<int>();//移除了需要报杠的牌

            bool b = false;
           for (int j = 0; j < curhandCard.Length; j++)
           {
                b = false;
                for (int i = 0; i < canBaoKong.Length; i++)
                {
                    if (curhandCard[j] == canBaoKong[i])
                    {
                        b = true;
                        break;
                    }
                       
                }
                if(!b)
                list.add(curhandCard[j]);
            }

            AYMJCardsCounter info = new AYMJCardsCounter(rule.rule, list, playerCards.fixCards, paidui);
            if (isOneFangshu())
                info = new AYMJCards2r1fCounter(rule.rule, list, playerCards.fixCards, paidui);
            info.setHandCardSize(list.count);
            ArrayList<FixedCards> fixCards = playerCards.fixCards;
            int[] array = info.checkOutListens(fixCards);
            if (array != Single.EMPTY_INT_ARRAY)
            {
                return true;
            }
            return false;
        }
      

        /// <summary>
        /// 获得听牌 一张一张遍历  临时写死  如果其他类型麻将 需要重写此方法
        /// </summary>
        /// <param name="card">传入的牌</param>
        /// <returns></returns>
        protected override TingCardsInfo getDengAccoBySendCard(int huType, int index)
        {
            AYMJPlayerCards selfCards = getSelfPlayerCards<AYMJPlayerCards>();
            if (selfCards.isHu())
                return null;
            if (selfCards.isHaveDingTypeCard())
                return null;

            ArrayList<FixedCards> fixCards = pcards[index].fixCards;
            AYMJCardsCounter info = new AYMJCardsCounter(rule.rule, pcards[index].handcards, fixCards, paidui);
            if (isOneFangshu())
                info = new AYMJCards2r1fCounter(rule.rule, pcards[index].handcards, fixCards, paidui);

            info.setHandCardSize(getSingleCount(index));

            int[] array = info.checkOutListens(fixCards);
            if (array == Single.EMPTY_INT_ARRAY) return null;

            TingCardsInfo tinglist = new TingCardsInfo();
            int point = 0;
            TingCard tcard = null;

            bool b = selfCards.hasStatus(MJPlayerCards.STATUS_BAOPAI);

            int[] baoKong=selfCards.baoKangCard;
            for (int i=0;i<baoKong.Length;i++)
            {
                AYMJFixedCards f = new AYMJFixedCards(MJFixedCards.MJ_BAO_KONG,new int[3] { baoKong [i], baoKong[i], baoKong[i]});
                fixCards.add(f);
            }


            for (int i = 0; i < array.Length; i++)
            {
                info.init(pcards[index].handcards, fixCards);
                info.addHandCard(array[i], 1);
                point = info.getHuPoint(huType, array[i], fixCards);
                tcard = new TingCard();
                tcard.card = array[i];
                if (b)
                    point++;
                tcard.setFan(point);
                tinglist.card = array[i];
                tinglist.setTingCards(tcard);
                info.delHandCard(array[i], 1);
            }

            selfCards.removeBaoKongFixed();

            return tinglist;
        }


        #endregion

        #region 听牌
        /// <summary>
        /// 获取自己听的牌
        /// </summary>
        /// <returns></returns>
        public override TingCardsInfo[] getSelfTingCards(int index)
        {
            if (!rule.rule.getRuleAtribute("hucue")) return null;
            return getPlayIndexTingCards(index);
        }

        public override TingCardsInfo[] getPlayIndexTingCards(int index)
        {
            ArrayList<TingCardsInfo> list = new ArrayList<TingCardsInfo>();

            AYMJPlayerCards playerCards = getPlayerCardIndex<AYMJPlayerCards>(index);

            TingCardsInfo ting;
            for (int i = 0; i < playerCards.handcards.count; i++)
            {
                ting = getTingCards(playerCards.handcards.get(i), 0, index);
                if (ting != null)
                {
                    list.add(ting);
                }
            }

            if (playerCards.mocard != MJCard.CNO)
            {
                ting = getTingCards(playerCards.mocard, 0, index);
                if (ting != null)
                    list.add(ting);
            }


            if (list.count == 0) return null;

            if (list.count > 1)
            {
                int maxpoint = 0;//最大番数
                int maxnum = 0;//最多数量

                ArrayList<int> maxpointlist = new ArrayList<int>();
                ArrayList<int> maxnumlist = new ArrayList<int>();
                for (int i = 0; i < list.count; i++)
                {
                    ting = list.get(i);
                    if (i == 0)
                    {
                        maxpoint = ting.maxpoint;
                        maxnum = ting.count;
                        maxpointlist.add(i);
                        maxnumlist.add(i);
                    }
                    else
                    {
                        if (ting.maxpoint > maxpoint)
                        {
                            maxpointlist.clear();
                            maxpoint = ting.maxpoint;
                            maxpointlist.add(i);
                        }
                        else if (ting.maxpoint == maxpoint)
                        {
                            maxpointlist.add(i);
                        }

                        if (ting.count > maxnum)
                        {
                            maxnumlist.clear();
                            maxnum = ting.count;
                            maxnumlist.add(i);
                        }
                        else if (ting.count == maxnum)
                        {
                            maxnumlist.add(i);
                        }
                    }
                }

                if (maxnumlist.count != list.count)
                {
                    for (int i = 0; i < maxnumlist.count; i++)
                    {
                        list.get(maxnumlist.get(i)).setMaxCountType();
                    }
                }

                if (maxpointlist.count != list.count)
                {
                    for (int i = 0; i < maxpointlist.count; i++)
                    {
                        list.get(maxpointlist.get(i)).setMaxPointType();
                    }
                }
            }

            return list.toArray();
        }


        /// <summary>
        /// 获得可以听的牌
        /// </summary>
        /// <param name="card">选择出的牌</param>
        /// <returns></returns>
        public override TingCardsInfo getTingCards(int card,int huType,int index)
        {
            TingCardsInfo tcardsr = getAccoBySendCard(card, huType,index);
            if (tcardsr == null)
                return null;
            ArrayList<TingCard> list = tcardsr.getTingList();

            TingCard ting = null;

            bool b = getPlayerCardIndex<AYMJPlayerCards>(index).hasStatus(MJPlayerCards.STATUS_BAOPAI);

            for (int i = 0; i < list.count; i++)
            {
                ting = list.get(i);
                if (b)
                    ting.setFan(ting.getMultiple()+1);
                ting.num = getCardLastNum(list.get(i).card);
                if (rule.rule.getRuleAtribute("enableOnlyOne")&&ting.num==1)
                    ting.setFan(ting.getMultiple() + 1);
                tcardsr.addCount(ting.num);
            }
            return tcardsr;
        }

        /// <summary>
        /// 获得听牌 一张一张遍历  临时写死  如果其他类型麻将 需要重写此方法
        /// </summary>
        /// <param name="card">传入的牌</param>
        /// <returns></returns>
        protected override TingCardsInfo getAccoBySendCard(int card,int huType,int index)
        {
            AYMJPlayerCards selfCards = getSelfPlayerCards<AYMJPlayerCards>();
            if (selfCards.isHu())
                return null;
            if (selfCards.isHaveDingTypeCardOne())
                return null;
            if (selfCards.getDingTypeCardNum() == 1)
            {
                if (selfCards.mocard != MJCard.CNO && selfCards.distype == MJCard.getType(selfCards.mocard))
                    return null;
            }

            //缓存玩家手上的牌
            ArrayList<int> handcd = new ArrayList<int>(selfCards.handcards.toArray());

            //将摸牌 加入手牌集合
            if (selfCards.mocard != MJCard.CNO)
                handcd.add(selfCards.mocard);

            ArrayList<FixedCards> fixCards = pcards[index].fixCards;
            AYMJCardsCounter info = new AYMJCardsCounter(rule.rule, handcd, fixCards, paidui);
            if (isOneFangshu())
                info = new AYMJCards2r1fCounter(rule.rule, handcd, fixCards, paidui);
            info.setHandCardSize(getSingleCount(index));

            info.delHandCard(card, 1);
            int[] array = info.checkOutListens(fixCards);
            if (array == Single.EMPTY_INT_ARRAY) return null;


            TingCardsInfo tinglist = new TingCardsInfo();
            int point = 0;
            TingCard tcard = null;
            for (int i = 0; i < array.Length; i++)
            {
                info.init(handcd, fixCards);
                info.delHandCard(card, 1);
                info.addHandCard(array[i], 1);
                point = info.getHuPoint(huType, array[i], fixCards);
                tcard = new TingCard();
                tcard.card = array[i];
                tcard.setFan(point);
                tinglist.card = card;
                tinglist.setTingCards(tcard);
            }

            return tinglist;
        }


        //获得指定牌剩余数量
        //遍历 全部出的牌和 自己的手牌  所有的组合牌
        public override int getCardLastNum(int card)
        {
            int num = showCards[MJCard.getType(card)][MJCard.getIndex(card)];


            AYMJPlayerCards playerCards = null;
            for (int i = 0; i < pcards.Length; i++)
            {
                playerCards = (AYMJPlayerCards)pcards[i];
                for (int j = 0; j < playerCards.disableCards.count; j++)
                {
                    if (card == MJCard.cancelSign(playerCards.disableCards.get(j),MJCard.SIGN_HU))
                    {
                        num++;
                    }
                }

                //if(i==mindex) continue;

                for (int j = 0; j < playerCards.handcards.count; j++)
                {
                    if (card == MJCard.cancelSign(playerCards.handcards.get(j),MJCard.SIGN_TANG))
                    {
                        num++;
                    }
                }
                if (card == playerCards.mocard&& MJMatchHuOperate.isZiMO(playerCards.hutype))
                    num++;
            }
            
           
              for (int i = 0; i < pcards.Length; i++)
              {
                  for (int j = 0; j < pcards[i].fixCards.count; j++)
                  {
                      for (int k = 0; k < pcards[i].fixCards.get(j).cards.Length; k++)
                      {
                          if (card == pcards[i].fixCards.get(j).cards[k])
                          {
                              num++;
                          }
                      }
                  }
              }
              
            if (card == getSelfPlayerCards<AYMJPlayerCards>().mocard)
            {
                num++;
            }
            return 4 - num;
        }
        #endregion

        #region 获取杠牌

        /// <summary>
        /// 能否暗杠 必须是自己回合
        /// 返回全部能暗杠的牌 [31,32] 表示有2组牌可以暗杠
        /// </summary>
        public override ArrayList<int> canMinePriGang(int index)
        {
            ArrayList<int> gangs = new ArrayList<int>();
            if (paidui == 0&&!rule.rule.getRuleAtribute("lastKong"))
                return gangs;

            AYMJPlayerCards playerCards = getPlayerCardIndex<AYMJPlayerCards>(index);
            playerCards.handcards.add(playerCards.mocard); //将摸牌 加入手牌
            int[] cards = playerCards.handcards.toArray(); //获得完整手牌集合 
            playerCards.handcards.removeLast(); //去掉摸牌

            cards = playerCards.sortArray(cards);

            int count = 0;
            int temp = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (temp == 0)
                {
                    temp = cards[i];
                    count++;
                }
                else
                {
                    if (temp == cards[i])
                    {
                        count++;
                        if (count == 4)
                        {
                            if (!playerCards.isDingType(temp))
                                gangs.add(temp);
                        }
                    }
                    else
                    {
                        temp = cards[i];
                        count = 1;
                    }
                }
            }
            return gangs;
        }

        /// <summary>
        /// 获取巴杠
        /// </summary>
        /// <returns></returns>
        public override ArrayList<int> canMineSupGang(int index)
        {
            ArrayList<int> gangs = new ArrayList<int>();
            if (paidui == 0&&!rule.rule.getRuleAtribute("lastKong"))
                return gangs;
            ArrayList<int> cardlist = new ArrayList<int>();

            AYMJPlayerCards playerCards = getPlayerCardIndex<AYMJPlayerCards>(index);

            for (int i = 0; i < playerCards.handcards.count; i++)
            {
                cardlist.add(playerCards.handcards.get(i));
            }

            cardlist.add(playerCards.mocard);

            int num = cardlist.count;

            //找出出所有的明杠
            for (int i = 0; i < playerCards.fixCards.count; i++)
            {
                if (playerCards.fixCards.get(i).type == MJFixedCards.MJPONG)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (cardlist.get(j) == playerCards.fixCards.get(i).cards[0])
                        {
                            gangs.add(cardlist.get(j));
                        }
                    }
                }
            }
            return gangs;
        }
        #endregion

        public override void bytesRead(ByteBuffer data)
        {
            stage = data.readInt();
            step = data.readInt();
            banker = data.readInt();

            paidui = data.readInt();
            isTianhu = data.readBoolean();
            ReconnectAYMJManager.manager.bytesReadInfos(data);

            reconnectBytesRead(data);

            ReconnectAYMJManager.manager.bytesRead(data);

        }

        public override void reconnectBytesRead(ByteBuffer data)
        {
            for (int i = 0; i < players.Length; i++)
            {
                ((AYMJPlayerCards)pcards[i]).setDistype(data.readInt());
                pcards[i].setStatus(data.readInt());
                pcards[i].bytesRead(data);
                extraData(i, data);
            }
        }

        /// <summary>
        /// 重连读取每个规则对应的数据
        /// </summary>
        /// <param name="data"></param>
        public override void extraData(int i, ByteBuffer data)
        {
            
        }

        /// <summary>
        /// 单局结算序列化
        /// </summary>
        /// <param name="data"></param>
        public override void bytesReadAll(ByteBuffer data)
        {
            //这里不能读父类的，因为父类的东西，其实是长牌的
            for (int i = 0; i < pcards.Length; i++)
            {
                this.pcards[i].bytesReadSingleOver(data);
            }
        }

        /// <summary>
        /// 回放拷贝比赛数据
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public override Match cloneCardScene(Match match)
        {
            int num = match.players.Length;
            AYMJMatch cs = new AYMJMatch(num);
            cs.banker = match.banker;
            cs.paidui = match.paidui;
            cs.mindex = match.mindex;
            cs.setStage(match.getStage());

            cs.pcards = new AYMJPlayerCards[num];
            for (int i = 0; i < num; i++)
            {
                PlayerCards pc = new AYMJPlayerCards();
                pc = match.getPCards()[i].clone(pc);
                cs.pcards[i] = pc;
            }

            cs.players = new MatchPlayer[num];
            for (int i = 0; i < num; i++)
            {
                cs.players[i] = match.players[i];
            }
            cs.setRoomRule(match.getRoomRule());
            return cs;
        }
    }
}
