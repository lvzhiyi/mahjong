using basef.rule;
using cambrian.common;
using platform.spotred;

namespace platform.poker
{

    public class PDKTenMatch : Match
    {
        public static PDKTenMatch match;

        /// <summary> 牌桌缓存的牌 </summary>
        public PDKTenDeskCacheCard deskCard { get; private set; }

        /// <summary> 玩家选择的牌 </summary>
        public ArrayList<int> selectCard { get; private set; }

        /// <summary> 获取记牌器 </summary>
        public CardRecorder recorder { get; private set; }

        /// <summary> 提示 </summary>
        public PDKTenTipsSeacher tips { get; private set; }

        /// <summary> 倍数 </summary>
        public PDKTenMultipleBean multiple;

        /// <summary> 比赛人数 </summary>
        public int playerCount { get { return rule.playerCount; } private set { } }

        public new Rule rule { get; private set; }

        /// <summary> 回放 </summary>
        public bool replay;

        /// <summary> 回合 </summary>
        public int rounds { get { return round; } private set { } }

        /// <summary> 操作值 </summary>
        public int[] operate { get; private set; }

        /// <summary> 操作类型 </summary>
        public int type { get; private set; }

        /// <summary> 首次出牌(十张跑得快要用) </summary>
        public int firstCard { get; set; }

        public PDKTenMatch()
        {
            replay = false;
            rule = Room.room.roomRule.rule;
            pcards = new PDKPlayerCards[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                pcards[i] = new PDKPlayerCards();
            }
            multiple = new PDKTenMultipleBean();
            selectCard = new ArrayList<int>();
            recorder = new CardRecorder();
            tips = new PDKTenTipsSeacher();
            deskCard = new PDKTenDeskCacheCard(playerCount);
        }

        public void setBase(int type, int stage, int paidui, int step, int round, int[] operate)
        {
            this.type = type;
            this.stage = stage;
            this.paidui = paidui;
            this.step = step;
            this.round = round;
            this.operate = operate;
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            var round = data.readInt();
            var roundTime = data.readLong();
            var operate = data.readInt();
            reconnectBytesRead(data);
            ReconnectPDKTenMatchManager.manager.setData(round, roundTime, operate);
            ReconnectPDKTenMatchManager.manager.bytesRead(data);
        }

        /// <summary> 重连读取 </summary>
        public void reconnectBytesRead(ByteBuffer data)
        {
            for (int i = 0, index = 0, temp = 0; i < players.Length; i++)
            {
                index = i;
                pcards[index].setStatus(data.readInt());
                int size = data.readInt();
                for (int j = 0; j < size; j++)
                {
                    temp = data.readInt();
                    pcards[index].AddHandCards(temp);
                }
            }
        }

        /// <summary> 玩家选择的牌 获取玩家选择的牌 </summary>
        public PDKTenCardInfo getSelectCard()
        {
            return new PDKTenCardInfo(mindex, selectCard.toArray());
        }

        /// <summary> 获取相对于自己位置的玩家信息 </summary>
        public MatchPlayer getPlayer(int pos)
        {
            return players[PKGAME.GetIndex(pos)];
        }

        /// <summary> 获取自己最小的牌 </summary>
        public int getSelfHandMaxCard()
        {
            if (pcards[mindex].getSortHandCards() == null) return -1;
            if (pcards[mindex].getSortHandCards().Length == 0) return -1;
            return pcards[mindex].getSortHandCards()[pcards[mindex].getSortHandCards().Length - 1];
        }

        /// <summary> 获取对应玩家的牌 </summary>
        public PDKPlayerCards getPCardIndex(int index)
        {
            return getPlayerCardIndex<PDKPlayerCards>(index);
        }

        /// <summary> 获取自己的手牌 </summary>
        public override int[] getSelfHandCard()
        {
            return pcards[mindex].getSortHandCards();
        }

        /// <summary> 获取对应玩家手牌 </summary>
        public override int[] GetMathPlayerHand(int index)
        {
            return pcards[index].handcards.toArray();
        }

        /// <summary> 发牌 </summary>
        public override void DealCards(int[][] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                pcards[i].AddHandAllCards(cards[i]);
            }
        }

        public override SpotRedCount[] getSpotRedCounts()
        {
            throw new System.NotImplementedException();
        }

        public override ArrayList<int> removeHandCard(int index, int card, int count)
        {
            throw new System.NotImplementedException();
        }

        /// <summary> 回放拷贝比赛数据 </summary>           
        public PDKTenMatch cloneCardMatch(PDKTenMatch mc)
        {
            if (mc == null) return new PDKTenMatch();
            int num = mc.playerCount;
            PDKTenMatch cs = new PDKTenMatch();

            cs.replay = mc.replay;
            cs.banker = mc.banker;
            cs.paidui = mc.paidui;
            cs.mindex = mc.mindex;
            cs.step = mc.step;
            cs.setStage(mc.getStage());
            cs.setRound(mc.rounds);
            cs.type = mc.type;

            cs.recorder = new CardRecorder();
            cs.recorder.clone(mc.recorder);
            cs.deskCard = new PDKTenDeskCacheCard(num);
            cs.deskCard.clone(mc.deskCard);

            cs.multiple.clone(mc.multiple.boomPoint);
            cs.operate = (int[])mc.operate.Clone();

            cs.pcards = new PDKPlayerCards[num];
            cs.players = new MatchPlayer[num];
            for (int i = 0; i < num; i++)
            {
                cs.pcards[i] = new PDKPlayerCards();
                cs.pcards[i].handcards = new ArrayList<int>();
                cs.pcards[i].handcards.add(mc.getPlayerCardIndex<PDKPlayerCards>(i).handcards.toArray());
                cs.players[i] = mc.players[i];
            }
            int[] remain = mc.remainingCards;
            if (remain == null)
                remain = new int[0];
            cs.remainingCards = new int[remain.Length];
            for (int i = 0; i < remain.Length; i++)
            {
                cs.remainingCards[i] = remain[i];
            }
            cs.rule = mc.rule;
            cs.setRoomRule(mc.getRoomRule());
            return cs;
        }
    }
}

