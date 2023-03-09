using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 长牌比赛类
    /// </summary>
    public class CPMatch:Match
    {
        public static CPMatch match;
        /// <summary>
        /// 无小家
        /// </summary>
        public const int NO_XIAOJIA = -1;

        public static int NO_PLAYER_INDEX = -1, NO_CARD = -1;

        /** 比赛阶段状态：准备阶段 */
        public const int STAGE_READY = 1;
        /** 比赛阶段状态：推挡阶段 */
        public const int STAGE_TUIDANG = 2;
        /** 比赛阶段状态：偷牌阶段 */
        public const int STAGE_SLIP = 3;
        /** 比赛阶段状态：报牌阶段 */
        public const int STAGE_BAOPAI = 4;
        /** 比赛阶段状态：行牌阶段 */
        public const int STAGE_MATCH = 5;
        /** 比赛阶段状态：结束阶段 */
        public const int STAGE_OVER = 6;

        /// <summary>
        /// 挡家
        /// </summary>
        public int dangjia { set; get; }
        /// <summary>
        /// 小家
        /// </summary>
        public int xiaojia { set; get; }
        /// <summary>
        /// 上一家玩家的索引
        /// </summary>
        private int last_player_index = NO_PLAYER_INDEX;
        /// <summary>
        /// 上一家人的牌(包括出牌和翻牌)
        /// </summary>
        private int last_player_card = NO_CARD;
        /// <summary>
        /// 胡的牌
        /// </summary>
        public int hu_card = NO_CARD;

        public CPMatch(int playercount)
        {
            pcards = new CPPlayerCards[playercount];
            for (int i = 0; i < playercount; i++)
            {
                pcards[i] = new CPPlayerCards();
            }
        }

        public CPMatch(int playercount,int step,int banker,int dangjia,int xiaojia,int paidui) : this(playercount)
        {
            this.step = step;
            this.banker = banker;
            this.dangjia = dangjia;
            this.xiaojia = xiaojia;
            this.paidui = paidui;
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

        public int getLastPlayerIndex()
        {
            return this.last_player_index;
        }

        /// <summary>
        /// 获取自己可以破对的牌
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getCanPdCards()
        {
            return ((CPPlayerCards)pcards[mindex]).getCanpdPlayCard();
        }

        /// <summary>
        /// 获取不能打的牌
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getNoCanPlayCards()
        {
            return ((CPPlayerCards)pcards[mindex]).getNoCanPlayCards();
        }

        /// <summary>
        /// 获取报牌后，不能出的牌
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] getBaoDisableCards()
        {
            int[] canPlayCards = getSelfPlayerCards<CPPlayerCards>().getCanPlayCards();
            if (canPlayCards == null || canPlayCards.Length == 0) return new int[0];
            ArrayList<int> list = new ArrayList<int>();
            int[] handCards = getSelfHandCard();

            for (int i = 0; i < handCards.Length; i++)
            {
                int card = handCards[i];
                bool b = false;
                for (int j = 0; j < canPlayCards.Length; j++)
                {
                    if (card == canPlayCards[j])
                    {
                        b = true;
                        break;
                    }
                }

                if (!b)
                    list.add(card);
            }

            return list.toArray();
        }

        /// <summary>
        /// 重置上一家人索引和牌
        /// </summary>
        public void ResetPlayerCard()
        {
            this.last_player_card = NO_CARD;
            this.last_player_index = NO_PLAYER_INDEX;
        }

        /// <summary>
        /// 将上一家人打的牌加入到出牌区
        /// </summary>
        public bool addLastPlayerCardToDisable()
        {
            if (last_player_index != NO_PLAYER_INDEX)
            {
                this.pcards[last_player_index].addDisableCard(last_player_card);
                return true;
            }
            return false;
        }


        public override int[] getSelfHandCard()
        {
            return pcards[mindex].handcards.toArray();
        }

        public bool isXiaoJia()
        {
            if (xiaojia == mindex)
                return true;
            return false;
        }

        public bool isXiaoJia(int index)
        {
            if (xiaojia == index) return true;
            return false;
        }

        public override SpotRedCount[] getSpotRedCounts()
        {
            ArrayList<SpotRedCount> list = new ArrayList<SpotRedCount>();
            for (int i = 0; i < pcards.Length; i++)
            {
                list.add(pcards[i].count);
            }

            return list.toArray();
        }

        /// <summary>
        /// 系统发牌
        /// </summary>
        public override void DealCards(int[][] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                pcards[i].AddHandAllCards(cards[i]);
            }
        }

        /// <summary>
        /// 移除手牌
        /// </summary>
        /// <param name="index">玩家索引</param>
        /// <param name="card">牌</param>
        /// <param name="count">数量</param>
        public override ArrayList<int> removeHandCard(int index, int card, int count)
        {
            return pcards[index].delHandCard(card, count);
        }

        public override int[] GetMathPlayerHand(int index)
        {
            return pcards[index].handcards.toArray();
        }

        public void reconnectBytesRead(ByteBuffer data)
        {
            for (int i = 0; i < players.Length; i++)
            {
                pcards[i].setStatus(data.readInt());
                int size = data.readInt();
                for (int j = 0; j < size; j++)
                {
                    FixedCards c = new FixedCards();
                    c.bytesRead(data);

                    pcards[i].addFixedCard(c);
                }

                size = data.readInt();
                for (int j = 0; j < size; j++)
                {
                    pcards[i].AddHandCards(data.readInt());
                }

                size = data.readInt();
                for (int j = 0; j < size; j++)
                {
                    pcards[i].addDisableCard(data.readInt());
                }

                extraData(i, data);
            }

            int sid = Room.room.roomRule.rule.sid;
            if (sid == RuleSid.GUANG_YUAN_CP)
            {
                int card = data.readInt();//不能打的牌
                int[] canpd = new int[data.readInt()];
                for (int i = 0; i < canpd.Length; i++)
                {
                    canpd[i] = data.readInt();
                }

                ((CPPlayerCards)pcards[mindex]).addNoCanPlayCard(card);
                ((CPPlayerCards)pcards[mindex]).addCanpdPlayCards(canpd);
            }
        }

        /// <summary>
        /// 重连读取每个规则对应的数据
        /// </summary>
        /// <param name="data"></param>
        public void extraData(int i, ByteBuffer data)
        {
            //if(this.rule.rule.sid==) 巴中打大
            int size = data.readInt();

            int[] canPlayCards = new int[size];
            for (int j = 0; j < size; j++)
            {
                canPlayCards[j] = data.readInt();
            }

            if (size > 0) ((CPPlayerCards)pcards[i]).setCanplayCards(canPlayCards);
        }


        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            this.dangjia = data.readInt();
            this.xiaojia = data.readInt();
            this.paidui = data.readInt(); //还剩余多少张牌
            ReconnectSecenManager.manager.bytesReadInfos(data);
            this.reconnectBytesRead(data);
            ReconnectSecenManager.manager.bytesRead(data);
        }

        /// <summary>
        /// 回放拷贝比赛数据
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public override Match cloneCardScene(Match match)
        {
            int num = match.players.Length;
            CPMatch cs = new CPMatch(num);
            cs.banker = match.banker;
            cs.paidui = match.paidui;
            cs.dangjia = ((CPMatch)match).dangjia;
            cs.xiaojia = ((CPMatch)match).xiaojia;
            cs.mindex = match.mindex;

            cs.pcards = new CPPlayerCards[num];
            for (int i = 0; i < num; i++)
            {
                PlayerCards pc = new CPPlayerCards();
                pc = match.getPCards()[i].clone(pc);
                cs.pcards[i] = pc;
            }

            cs.players = new MatchPlayer[num];
            for (int i = 0; i < num; i++)
            {
                cs.players[i] = match.players[i];
            }

            cs.setRoomRule(new RoomRule());
            cs.getRoomRule().copy(match.getRoomRule());
            return cs;
        }
    }
}
