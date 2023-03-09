using cambrian.common;
using platform.spotred;

namespace platform.mahjong
{
    public class MJMatch : Match
    {
        public static int NO_PLAYER_INDEX = -1, NO_CARD = -1;
        // ----------------------比赛阶段状态----------------------------
        /** 比赛阶段状态：初始阶段 */
        public const int STAGE_INIT = 0;
        /** 比赛阶段状态：准备阶段 */
        public const int STAGE_READY = 1;
        /** 比赛阶段状态：加漂阶段 */
        public const int STAGE_PIAO = 2;
        /** 比赛阶段状态：换牌阶段 */
        public const int STAGE_REPLACE = 3;
        /** 比赛阶段状态：定缺阶段 */
        public const int STAGE_DISTYPE = 4;
        /** 比赛阶段状态：报牌阶段 */
        public const int STAGE_BAOPAI = 5;
        /** 比赛阶段状态：行牌阶段 */
        public const int STAGE_MATCH = 6;
        /** 比赛阶段状态：结束阶段 */
        public const int STAGE_OVER = 7;

        public static MJMatch match { get; set; }
        /// <summary>
        /// 房主
        /// </summary>
        protected int master;
        /// <summary>
        /// 换三张牌的索引
        /// </summary>
        public int[] huangSanZhangIndex;

        /** 手牌区各牌数量 int[type][rank] */
        protected int[][] showCards;
        /// <summary>
        /// 打牌的人索引
        /// </summary>
        protected int last_player_index;
        /// <summary>
        /// 上一家打的牌或者巴杠正在等待中
        /// </summary>
        protected int last_player_card;
        /// <summary>
        /// 当前的牌是否是补杠产生(,或者是否是杠后出的牌)
        /// </summary>
        protected bool isKongSup=false;
        /// <summary>
        /// 杠牌类型
        /// </summary>
        protected int kongType;
        /// <summary>
        /// 杠牌者的索引
        /// </summary>
        public int kongIndex { get; set; }

        /// <summary>
        /// 是否是天胡
        /// </summary>
        public bool isTianhu { get; set; }
        /// <summary>
        /// 换牌数量
        /// </summary>
        public int replace { get; set; }
        /// <summary>
        /// 自己摸牌区的牌，(躺牌时需要还原)
        /// </summary>
        public int selfMoCard { get; set; }
        /// <summary>
        /// 躺牌的索引
        /// </summary>
        protected ArrayList<int> tangCardsIndex = null;

        public int operate { get; set; }

        public MJMatch()
        {

        }


        public MJMatch(int playercount)
        {
            isTianhu = true;
            pcards = new MJPlayerCards[playercount];
            for (int i = 0; i < playercount; i++)
            {
                pcards[i] = new MJPlayerCards();
            }
            showCards = new int[][] { new int[9], new int[9], new int[9], new int[4], new int[3], new int[8] };
            tangCardsIndex = null;
        }

        public MJMatch(int playercount, int step, int banker, int master, int paidui) : this(playercount)
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

        /// <summary>
        /// 移除躺的牌
        /// </summary>
        /// <param name="index"></param>
        public void removeTangCardIndex(int index)
        {
            if (tangCardsIndex == null) return;
            tangCardsIndex.remove(index);
        }

        /// <summary>
        /// 增加躺的牌
        /// </summary>
        /// <param name="index"></param>
        public void addTangCardIndex(int index)
        {
            if (tangCardsIndex == null) return;
            if (tangCardsIndex.contain(index)) return;
            tangCardsIndex.add(index);
        }

        public void addTangCardsIndexs(int[] indexs)
        {
            if(tangCardsIndex==null)
                tangCardsIndex=new ArrayList<int>();
            else
                tangCardsIndex.clear();
            tangCardsIndex.add(indexs);
        }

        public ArrayList<int> getTangCardsIndexs()
        {
            return tangCardsIndex;
        }

        /// <summary>
        /// 设置上一家人的索引和牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        public void setLastPlayerCard(int index, int card)
        {
            this.last_player_index = index;
            this.last_player_card = card;
        }

        /// <summary>
        /// 设置该牌是否是巴杠牌，,或者是否是杠后出的牌)
        /// </summary>
        /// <param name="isKongSup"></param>
        public void setKongSup(int index, int card, bool isKongSup)
        {
            this.last_player_index = index;
            this.last_player_card = card;
            this.isKongSup = isKongSup;
        }

        /// <summary>
        /// 设置是否是杠牌
        /// </summary>
        /// <param name="b"></param>
        public void setKong(bool b)
        {
            this.isKongSup = b;
        }

        public void setKongType(int kongType)
        {
            this.kongType = kongType;
        }

        /// <summary>
        /// 重置上一家人索引和牌
        /// </summary>
        public void ResetPlayerCard()
        {
            this.last_player_card = NO_CARD;
            this.last_player_index = NO_PLAYER_INDEX;
        }

        public void resetKongStatus()
        {
            isKongSup = false;
            kongIndex = -1;
        }

        /// <summary>
        /// 获取上一次做操作的人的索引
        /// </summary>
        /// <returns></returns>
        public int getLastPlayerIndex()
        {
            return last_player_index;
        }

        public int getLastPlayerCard()
        {
            return last_player_card;
        }

        /// <summary>
        /// 是否是补杠产生，,或者是否是杠后出的牌)
        /// </summary>
        /// <returns></returns>
        public bool isKongSups()
        {
            return isKongSup;
        }

        public override void DealCards(int[][] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                pcards[i].dealPlayerCards(cards[i]);
            }
        }

        public void addHuanSZCards(int[][] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                pcards[i].addHszCards(cards[i]);
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
        public virtual void addBankerCard(int card)
        {
            getPlayerCardIndex<MJPlayerCards>(banker).setMOCard(card);
        }

        /// <summary>
        /// 是否是，需要躺牌
        /// </summary>
        /// <returns></returns>
        public bool isTang()
        {
            return rule.rule.getRuleAtribute("lie");
        }

        /// <summary>
        /// 躺牌玩家的人数
        /// </summary>
        /// <returns></returns>
        public int huPlayerCount()
        {
            int len=pcards.Length;
            MJPlayerCards player=null;
            int count = 0;
            for (int i = 0; i < len; i++)
            {
                player = (MJPlayerCards) pcards[i];
                if (player.hutype!=-1)
                    count++;
            }
            return count;
        }


        /// <summary>
        /// 获取玩家的状态
        /// </summary>
        /// <returns></returns>
        public int[] getPlayerCardsStatus()
        {
            int[] status=new int[pcards.Length];
            for (int i = 0; i < pcards.Length; i++)
            {
                status[i] = pcards[i].status;
            }

            return status;
        }

        /// <summary>
        /// 获取手牌中每种牌型的数量(筒，条，万)
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="type">牌的类型</param>
        /// <returns></returns>
        public virtual int getHandCardTypeCount(int index, int type)
        {
            MJPlayerCards playerCards= getSelfPlayerCards<MJPlayerCards>();
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
        /// 筒，条，万
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual int getCardType(int index)
        {
            switch (index)
            {
                case 0:
                    return MJCard.TYPE_DOT;
                case 1:
                    return MJCard.TYPE_BAM;
                case 2:
                    return MJCard.TYPE_CHA;
            }

            return -1;
        }

        /// <summary>
        ///  获取玩家定缺的牌
        /// </summary>
        /// <returns></returns>
        public virtual int[] getPlayerDistypes()
        {
            MJPlayerCards[] players=getPlayerCardss<MJPlayerCards>();
            int[] distypes = new int[players.Length];
            for (int i=0;i<distypes.Length;i++)
            {
                distypes[i] = players[i].distype;
            }
            return distypes;
        }

        /// <summary>
        /// 获取胡牌类型
        /// </summary>
        /// <returns></returns>
        protected virtual int getHuType(int index)
        {
            int hutype = MJMatchHuOperate.HU_NORMAL;
            MJPlayerCards playerCard = getPlayerCardIndex<MJPlayerCards>(index);
            if (isRound(index))
            {
                if (rule.rule.sid == 2010&&playerCard.hasStatus(MJPlayerCards.STATUS_TIAN_DI_HU))
                {
                    hutype = MJMatchHuOperate.HU_TIAN;
                }
                else if (rule.rule.sid != 2010&&isTianhu)
                    hutype = MJMatchHuOperate.HU_TIAN;
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

                if (rule.rule.sid == 2010 && playerCard.hasStatus(MJPlayerCards.STATUS_TIAN_DI_HU))
                {
                    hutype = MJMatchHuOperate.HU_DI;
                }
                else if (rule.rule.sid != 2010 && isTianhu)
                    hutype = MJMatchHuOperate.HU_DI;
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
        public virtual MJOperateInfoBean[] getMJOperateInfos(int operate, int card, bool isGang, int index)
        {
            ArrayList<MJOperateInfoBean> list = new ArrayList<MJOperateInfoBean>();
            MJOperateInfoBean bean;
            if (StatusKit.hasStatus(operate, MJOperate.CAN_HU))
            {
                bean = new MJOperateInfoBean();
                int point = getHuFan(card, getHuType(index), index); //番数
                if (isTang() && getPlayerCardIndex<MJPlayerCards>(index).hasStatus(MJPlayerCards.STATUS_TANG))
                    point++;
                if (last_player_index != mindex &&getPlayerCardIndex<MJPlayerCards>(last_player_index).hasStatus(MJPlayerCards.STATUS_TANG))
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

            if (isTangCard(index)) //该规则有躺牌,并且有叫的情况
            {
                bean = new MJOperateInfoBean();
                bean.setTang();
                list.add(bean);

                bean = new MJOperateInfoBean();
                bean.setCancel();
                list.add(bean);
            }
            else if (StatusKit.hasStatus(operate, MJOperate.CAN_CANCEL))
            {
                bean = new MJOperateInfoBean();
                bean.setCancel();
                list.add(bean);
            }

            return list.toArray();
        }


        /// <summary>
        /// 能否躺牌
        /// </summary>
        /// <returns></returns>
        private bool isTangCard(int index)
        {
            if (!isTang()||paidui < 8) return false;//最后8张不能躺牌
            int playernum = rule.rule.playerCount;
            if ( playernum==4&&huPlayerCount() >= 2) return false;
            else if (playernum == 3 && huPlayerCount() >= 1) return false;

            if (round == mindex &&
                !getPlayerCardIndex<MJPlayerCards>(index).hasStatus(MJPlayerCards.STATUS_TANG) &&
                getPlayIndexTingCards(index) != null)
                return true;

            return false;
        }

        protected virtual int[][] getSelfRoundKongs(int playerindex)
        {
            int[][] gangs; //杠牌类型
            int[] priGangs = canMinePriGang(playerindex).toArray();
            int[] supGangs = canMineSupGang(playerindex).toArray();

            int num = priGangs.Length + supGangs.Length;
            gangs = new int[num][];

            int index = 0;

            for (int i = 0; i < priGangs.Length; i++)
            {
                gangs[index] = new int[2];
                gangs[index][0] = SendMJMatchCommand.KONG_PRI;
                gangs[index][1] = priGangs[i];
                index++;
            }
            for (int i = 0; i < supGangs.Length; i++)
            {
                gangs[index] = new int[2];
                gangs[index][0] = SendMJMatchCommand.KONG_SUP;
                gangs[index][1] = supGangs[i];
                index++;
            }

            return gangs;
        }

        /// <summary>
        /// 获取胡牌的番数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="isGang"></param>
        /// <returns></returns>
        public virtual int getHuFan(int card,int huType,int index)
        {
            MJCardsCounter info = new MJCardsCounter(rule.rule, pcards[index].handcards, pcards[index].fixCards,paidui);
            if (isOneFangshu())
                info = new MJCards2r1fCounter(rule.rule, pcards[index].handcards, pcards[index].fixCards,paidui);
            info.setHandCardSize(getSingleCount(index));
            info.addHandCard(card, 1);
            int fan=info.getHuPoint(huType, card, pcards[index].fixCards);
            return fan;
        }

        public bool isOneFangshu()
        {
            return rule.rule.getIntAtribute("fangshu")==1;
        }

        /// <summary>
        /// 是否是金勾勾，或者大单吊(摸牌区的牌不能算的，因为有自摸的情况)
        /// </summary>
        /// <returns></returns>
        protected virtual bool isSingle(int index)
        {
            int count= getPlayerCardIndex<MJPlayerCards>(index).handcards.count;

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
            int count = getPlayerCardIndex<MJPlayerCards>(index).handcards.count;
            if (isRound(index) && count > 1)//这里主要时判断该自己出牌时，是否满足金钩钩的条件
                count--;
            return count;
        }

        /// <summary>
        /// 获取点炮的牌
        /// </summary>
        /// <returns></returns>
        public virtual int[] getDianPaoCard()
        {
            if (!isTang()||!isRound(mindex)|| operate==0) return null;
            ArrayList<int[]> list = new ArrayList<int[]>();
            MJPlayerCards playercards = null;
            int[] tangcard = null;
            TingCardsInfo tings = null;
            ArrayList<int> dp = new ArrayList<int>();
            for (int i=0;i<players.Length;i++)
            {
                if (i == mindex) continue;
                playercards =getPlayerCardIndex<MJPlayerCards>(i);
                tangcard = playercards.getTangCards();
                
                if (tangcard!=null&&tangcard.Length>0)
                {
                    list.add(tangcard);
                    tings = getTangTing(tangcard,i);
                    if (tings != null)
                    {
                        for (int j = 0; j < tings.getTingList().count; j++)
                        {
                            dp.add(tings.getTingList().get(j).card);
                        }

                    }
                }
            }
            return dp.count == 0?null:dp.toArray();
        }


        #region 换三张

        /// <summary>
        /// 获取换三张最少牌的类型
        /// </summary>
        /// <returns></returns>
        public virtual int getHuanSZMinCardType()
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
        public virtual int[] getHuanSanZhangCards()
        {
            if (huangSanZhangIndex == null)
            {
                huangSanZhangIndex = getSelfPlayerCards<MJPlayerCards>().getDeflultHuansanzhang(0, getHuanSZMinCardType(),replace);
            }

            int[] cards = new int[replace];
            for (int i = 0; i < replace; i++)
            {
                if (huangSanZhangIndex[i] == -1) return null;
                if (this.huangSanZhangIndex[i] == this.pcards[this.mindex].handcards.count)
                {
                    cards[i] = getSelfPlayerCards<MJPlayerCards>().mocard;
                }
                else
                {
                    cards[i] = pcards[mindex].handcards.get(this.huangSanZhangIndex[i]);
                }
            }
            return cards;
        }

        public  bool replaceCardValid(int[] cards)
        {
            if (cards == null) return false;
            for (int i=0;i<cards.Length;i++)
            {
                if (cards[i] == -1) return false;
            }
            return true;
        }

        /// <summary>
        /// 获取换三张牌的索引
        /// </summary>
        /// <returns></returns>
        public virtual int[] getHuanSZIndex()
        {
            if (huangSanZhangIndex == null)
            {
                huangSanZhangIndex = getSelfPlayerCards<MJPlayerCards>().getDeflultHuansanzhang(0, getHuanSZMinCardType(),replace);
            }

            return huangSanZhangIndex;
        }

        /// <summary>
        /// 添加一张牌的索引到换三张数组中
        /// </summary>
        /// <param name="index"></param>
        public virtual bool addHuangSZIndex(int index)
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
            if (num == replace)//有三张相同的牌
                return false;

            //里面没牌
            if (num == 0)
            {
                huangSanZhangIndex[0] = index;
            }
            else
            {
                int card;
                MJPlayerCards cards = getSelfPlayerCards<MJPlayerCards>();
                if (tempIndex == cards.handcards.count)
                    card = cards.mocard;
                else
                    card = cards.handcards.toArray()[tempIndex];

                int card2;
                if (index == cards.handcards.count)
                    card2 = cards.mocard;
                else
                    card2 = cards.handcards.toArray()[index];

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
        public virtual void deleteHuansanzhangIndex(int index)
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
        public virtual int[] getRecommendType()
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
        public virtual TingCardsInfo getDengTingCards(int index)
        {
            if (!rule.rule.getRuleAtribute("hucue")) return null;
            return getDengTingCards(0, index);
        }

        /// <summary>
        /// 获得可以听的牌
        /// </summary>
        /// <param name="card">选择出的牌</param>
        /// <returns></returns>
        public virtual TingCardsInfo getDengTingCards(int huType, int index)
        {
            TingCardsInfo tcardsr = getDengAccoBySendCard(huType, index);
            if (tcardsr == null)
                return null;
            ArrayList<TingCard> list = tcardsr.getTingList();
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).num = this.getCardLastNum(list.get(i).card);
                tcardsr.addCount(list.get(i).num);
            }

            return tcardsr;
        }

        /// <summary>
        /// 获取躺牌，听的牌
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public virtual TingCardsInfo getTangTing(int[] cards,int index)
        {
            TingCardsInfo tcardsr = getTangAccoBySendCard(0, index,cards);
            if (tcardsr == null)
                return null;

            ArrayList<TingCard> list = tcardsr.getTingList();
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).num = this.getCardLastNum(list.get(i).card);
                tcardsr.addCount(list.get(i).num);
            }
            return tcardsr;
        }

        public bool isContainType(int[] cards,int type)
        {
            if (cards == null || cards.Length == 0)
                return false;
            for (int i = 0; i < cards.Length; i++)
            {
                if (MJCard.getType(cards[i]) == type)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 获得听牌 一张一张遍历  临时写死  如果其他类型麻将 需要重写此方法
        /// </summary>
        /// <param name="card">传入的牌</param>
        /// <returns></returns>
        protected virtual TingCardsInfo getTangAccoBySendCard(int huType, int index,int[] cards)
        {
            MJPlayerCards selfCards = getPlayerCardIndex<MJPlayerCards>(index);


            ArrayList<int> handcard = new ArrayList<int>(cards);

            MJCardsCounter info = new MJCardsCounter(rule.rule, handcard, selfCards.fixCards,paidui);
            if (isOneFangshu())
                info = new MJCards2r1fCounter(rule.rule, new ArrayList<int>(cards),selfCards.fixCards,paidui);
            info.setHandCardSize(getSingleCount(index));
            ArrayList<FixedCards> fixCards = pcards[index].fixCards;
            int[] array = info.checkOutListens(fixCards);
            if (array == Single.EMPTY_INT_ARRAY) return null;

            TingCardsInfo tinglist = new TingCardsInfo();
            int point = 0;
            TingCard tcard = null;
            for (int i = 0; i < array.Length; i++)
            {
                info.init(handcard, fixCards);
                info.addHandCard(array[i], 1);
                point = info.getHuPoint(huType, array[i], fixCards);
                tcard = new TingCard();
                tcard.card = array[i];
                tcard.setFan(point);
                tinglist.card = array[i];
                tinglist.setTingCards(tcard);
                info.delHandCard(array[i], 1);
            }
            return tinglist;
        }

        /// <summary>
        /// 获得听牌 一张一张遍历  临时写死  如果其他类型麻将 需要重写此方法
        /// </summary>
        /// <param name="card">传入的牌</param>
        /// <returns></returns>
        protected virtual TingCardsInfo getDengAccoBySendCard(int huType, int index)
        {
            MJPlayerCards selfCards = getSelfPlayerCards<MJPlayerCards>();
            if (selfCards.isHu())
                return null;
            if (selfCards.isHaveDingTypeCard())
                return null;


            ArrayList<FixedCards> fixCards = pcards[index].fixCards;
            MJCardsCounter info = new MJCardsCounter(rule.rule, pcards[index].handcards, fixCards, paidui);
            if (isOneFangshu())
                info = new MJCards2r1fCounter(rule.rule, pcards[index].handcards, fixCards, paidui);

            info.setHandCardSize(getSingleCount(index));

            int[] array = info.checkOutListens(fixCards);
            if (array == Single.EMPTY_INT_ARRAY) return null;

            TingCardsInfo tinglist = new TingCardsInfo();
            int point = 0;
            TingCard tcard = null;
            for (int i = 0; i < array.Length; i++)
            {
                info.init(pcards[index].handcards, fixCards);

                info.addHandCard(array[i], 1);
                point = info.getHuPoint(huType, array[i], fixCards);
                tcard = new TingCard();
                tcard.card = array[i];
                tcard.setFan(point);
                tinglist.card = array[i];
                tinglist.setTingCards(tcard);
                info.delHandCard(array[i], 1);
            }
            return tinglist;
        }


        #endregion

        #region 听牌
        /// <summary>
        /// 获取自己听的牌
        /// </summary>
        /// <returns></returns>
        public virtual TingCardsInfo[] getSelfTingCards(int index)
        {
            if (!rule.rule.getRuleAtribute("hucue")) return null;
            if (isTang() && getPlayerCardIndex<MJPlayerCards>(index).hasStatus(MJPlayerCards.STATUS_TANG))
                return null;
            return getPlayIndexTingCards(index);
        }

        public virtual TingCardsInfo[] getPlayIndexTingCards(int index)
        {
            ArrayList<TingCardsInfo> list = new ArrayList<TingCardsInfo>();

            MJPlayerCards playerCards = getPlayerCardIndex<MJPlayerCards>(index);

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
        public virtual TingCardsInfo getTingCards(int card,int huType,int index)
        {
            TingCardsInfo tcardsr = getAccoBySendCard(card, huType,index);
            if (tcardsr == null)
                return null;
            ArrayList<TingCard> list = tcardsr.getTingList();
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).num = getCardLastNum(list.get(i).card);
                tcardsr.addCount(list.get(i).num);
            }
            return tcardsr;
        }

        /// <summary>
        /// 获得听牌 一张一张遍历  临时写死  如果其他类型麻将 需要重写此方法
        /// </summary>
        /// <param name="card">传入的牌</param>
        /// <returns></returns>
        protected virtual TingCardsInfo getAccoBySendCard(int card,int huType,int index)
        {
            MJPlayerCards selfCards = getSelfPlayerCards<MJPlayerCards>();
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
            MJCardsCounter info = new MJCardsCounter(rule.rule, handcd, fixCards, paidui);
            if (isOneFangshu())
                info = new MJCards2r1fCounter(rule.rule, handcd, fixCards, paidui);
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
        public virtual int getCardLastNum(int card)
        {
            int num = showCards[MJCard.getType(card)][MJCard.getIndex(card)];


            MJPlayerCards playerCards = null;
            for (int i = 0; i < pcards.Length; i++)
            {
                playerCards = (MJPlayerCards)pcards[i];
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
              
            if (card == getSelfPlayerCards<MJPlayerCards>().mocard)
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
        public virtual ArrayList<int> canMinePriGang(int index)
        {
            ArrayList<int> gangs = new ArrayList<int>();
            if (paidui == 0&&!rule.rule.getRuleAtribute("lastKong"))
                return gangs;

            MJPlayerCards playerCards = getPlayerCardIndex<MJPlayerCards>(index);
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
        public virtual ArrayList<int> canMineSupGang(int index)
        {
            ArrayList<int> gangs = new ArrayList<int>();
            if (paidui == 0&&!rule.rule.getRuleAtribute("lastKong"))
                return gangs;
            ArrayList<int> cardlist = new ArrayList<int>();

            MJPlayerCards playerCards = getPlayerCardIndex<MJPlayerCards>(index);

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
            base.bytesRead(data);
            
            paidui = data.readInt();
            isTianhu = data.readBoolean();
            ReconnectMJSecenManager.manager.bytesReadInfos(data);

            reconnectBytesRead(data);

            ReconnectMJSecenManager.manager.bytesRead(data);

        }

        public virtual void reconnectBytesRead(ByteBuffer data)
        {
            for (int i = 0; i < players.Length; i++)
            {
                ((MJPlayerCards)pcards[i]).setDistype(data.readInt());
                pcards[i].setStatus(data.readInt());
                pcards[i].bytesRead(data);
                extraData(i, data);
            }
        }

        /// <summary>
        /// 重连读取每个规则对应的数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void extraData(int i, ByteBuffer data)
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
            MJMatch cs = new MJMatch(num);
            cs.banker = match.banker;
            cs.paidui = match.paidui;
            cs.mindex = match.mindex;
            cs.setStage(match.getStage());

            cs.pcards = new MJPlayerCards[num];
            for (int i = 0; i < num; i++)
            {
                PlayerCards pc = new MJPlayerCards();
                pc = match.getPCards()[i].clone(pc);
                cs.pcards[i] = pc;
            }

            cs.players = new MatchPlayer[num];
            for (int i = 0; i < num; i++)
            {
                cs.players[i] = match.players[i];
            }

            cs.setRoomRule(match.getRoomRule());
            //cs.getRoomRule().copy(match.getRoomRule());
            return cs;
        }
    }
}
