using System.Text;
using basef.rule;
using cambrian.common;
using platform.spotred;
using UnityEngine;

namespace platform.poker
{
    public class DDZSTAGE
    {
        /// <summary> 比赛阶段：初始阶段 </summary>
        public const int INIT = 0;

        /// <summary> 比赛阶段：初始阶段 </summary>
        public const int READY = 1;

        /// <summary> 比赛阶段：叫地主阶段 </summary>
        public const int JIAODIZHU = 2;

        /// <summary> 比赛阶段：抢地主阶段 </summary>
        public const int QIANGDIZHU = 3;

        /// <summary> 比赛阶段：加倍阶段 </summary>
        public const int JIABEI = 4;

        /// <summary> 比赛阶段：行牌阶段 </summary>
        public const int MATCH = 5;

        /// <summary> 比赛阶段：结束阶段 </summary>
        public const int OVER = 6;

        /// <summary> 比赛阶段：发牌阶段 </summary>
        public const int DEALCARDS = 7;

        /// <summary> 比赛阶段：明牌阶段 </summary>
        public const int MINGPAI = 9;

        /// <summary> 比赛阶段：叫分阶段 </summary>
        public const int JIAOFEN = 10;
    }

    public class DDZMatch : Match
    {
        public static DDZMatch match;

        /// <summary> 是否回放 </summary>
        public bool replay;

        /// <summary> 牌桌缓存的牌 </summary>
        public DDZDeskCacheCard deskCard { get; private set; }

        /// <summary> 玩家选择的牌 </summary>
        public ArrayList<int> selectCard { get; private set; }

        /// <summary> 当前游戏倍数 </summary>
        public DDZMultipleBean multipleBean { get; private set; }

        /// <summary> 获取记牌器 </summary>
        public CardRecorder recorder { get; private set; }

        /// <summary> 提示 </summary>
        public DDZTipsSeacher tips { get; private set; }

        /// <summary> 地主索引 </summary>
        public int diZhuIndex { get; private set; }

        /// <summary> 自己是否是地主 </summary>
        public bool mineDiZhu { get { return diZhuIndex == mindex; } private set { } }

        /// <summary> 获取地主的相对位置 </summary>
        public int dizhuIndexLocal { get { return PKGAME.GetIndex(diZhuIndex); } private set { } }

        /// <summary> 地主牌 </summary>
        public int[] lordCards { get; private set; }

        /// <summary> 比赛人数 </summary>
        public int playerCount { get { return rule.playerCount; } private set { } }

        /// <summary> 当前玩家倍数 </summary>
        public int grossMuitiplier { get { return multipleBean.getSinglePoint(mindex); } private set { } }

        /// <summary> 叫分模式分数 </summary>
        public int callScore { get; private set; }

        /// <summary> 回合 </summary>
        public int rounds { get { return round; } private set { } }

        /// <summary> 操作值 </summary>
        public int[] operate { get; private set; }

        /// <summary> 操作类型 </summary>
        public int type { get; private set; }

        public new Rule rule { get; private set; }

        public DDZMatch()
        {
            replay = false;
            rule = Room.room.roomRule.rule;
            pcards = new DDZPlayerCards[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                pcards[i] = new DDZPlayerCards();
            }
            
            diZhuIndex = -1;
            callScore = 0;
            tips = new DDZTipsSeacher();
            recorder = new CardRecorder();
            selectCard = new ArrayList<int>();
            deskCard = new DDZDeskCacheCard(playerCount);
            multipleBean = new DDZMultipleBean(playerCount, rule.getBet(), rule.getIntAtribute("maxmultiple"));
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

        /// <summary> 地主牌初始化 </summary>
        public void lordCardsInit(int len)
        {
            lordCards = new int[len];
            for (int i = 0; i < len; i++)
            {
                lordCards[i] = Poker.INVISIBLE;
            }
        }

        /// <summary> 玩家选择的牌 获取玩家选择的牌 </summary>
        public DDZCardInfo getSelectCard()
        {
            return new DDZCardInfo(mindex, selectCard.toArray());
        }

        /// <summary> 获取相对于自己位置的玩家信息 </summary>
        public MatchPlayer getPlayer(int pos)
        {
            return players[PKGAME.GetIndex(pos)];
        }

        /// <summary> 获取下标玩家是否明牌 </summary>
        public bool getIndexPlayerMingPai(int pos)
        {
            return match.getPlayerCardIndex<DDZPlayerCards>(pos).getMingPaiStatus();
        }

        /// <summary> 设置地主索引 </summary>
        public void setDiZhuIndex(int dizhu)
        {
            diZhuIndex = dizhu;
        }

        /// <summary> 设置叫分 分数 </summary>
        public void setCallScore(int score)
        {
            if (score < callScore) return;
            callScore = score;
        }

        /// <summary> 设置叫分 分数 </summary>
        public void SetCallScore(int score)
        {
            callScore = score;
        }

        /// <summary> 设置地主牌 </summary>
        public void setDiZhuCards(int[] cards)
        {
            lordCards = cards;
        }

        /// <summary> 获取自己最小的牌 </summary>
        public int getSelfHandMaxCard()
        {
            if (pcards[mindex].getSortHandCards().Length == 0) return -1;
            return pcards[mindex].getSortHandCards()[pcards[mindex].getSortHandCards().Length - 1];
        }

        /// <summary> 获取对应玩家的牌 </summary>
        public DDZPlayerCards getPCardIndex(int index)
        {
            return getPlayerCardIndex<DDZPlayerCards>(index);
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

        public override ArrayList<int> removeHandCard(int index, int card, int count)
        {
            throw new System.NotImplementedException();
        }

        public override SpotRedCount[] getSpotRedCounts()
        {
            throw new System.NotImplementedException();
        }

        public override void bytesRead(ByteBuffer data)
        {
            base.bytesRead(data);
            diZhuIndex = data.readInt();           
            paidui = data.readInt();             
            lordCards = new int[paidui];
            for (int i = 0; i < paidui; i++)
            {
                lordCards[i] = data.readInt();                 
            }
            int round = data.readInt();           
            long roundTime = data.readLong();   
            int operate = data.readInt();        
            reconnectBytesRead(data);           
            ReconnectDDZMatchManager.manager.setData(round, roundTime, operate);
            ReconnectDDZMatchManager.manager.bytesRead(data);
        }

        /// <summary> 重连读取 </summary>
        public void reconnectBytesRead(ByteBuffer data)
        {
            for (int i = 0; i < players.Length; i++)
            {
                int index = i;
                pcards[index].setStatus(data.readInt());
                int size = data.readInt();
                for (int j = 0; j < size; j++)
                {
                    int temp = data.readInt();
                    pcards[index].AddHandCards(temp);
                }
            }
        }

        /// <summary> 回放拷贝比赛数据 </summary>           
        public DDZMatch cloneCardMatch(DDZMatch mc)
        {
            if (mc == null) return new DDZMatch();
            int num = mc.playerCount;
            DDZMatch cs = new DDZMatch();

            cs.replay = mc.replay;
            cs.banker = mc.banker;
            cs.paidui = mc.paidui;
            cs.mindex = mc.mindex;
            cs.diZhuIndex = mc.diZhuIndex;
            cs.step = mc.step;
            cs.setStage(mc.getStage());
            cs.setRound(mc.rounds);
            cs.type = mc.type;

            cs.multipleBean = new DDZMultipleBean(num, mc.rule.getBet(), mc.rule.getIntAtribute("maxmultiple"));
            cs.multipleBean.clone(mc.multipleBean);   
            cs.recorder = new CardRecorder();
            cs.recorder.clone(mc.recorder);          
            cs.deskCard = new DDZDeskCacheCard(num);
            cs.deskCard.clone(mc.deskCard);           

            cs.operate = (int[])mc.operate.Clone();
            cs.lordCards = (int[])mc.lordCards.Clone();
            cs.callScore = mc.callScore;

            cs.pcards = new DDZPlayerCards[num];
            cs.players = new MatchPlayer[num];
            for (int i = 0; i < num; i++)
            {
                cs.pcards[i] = new DDZPlayerCards();
                cs.pcards[i].handcards = new ArrayList<int>();
                cs.pcards[i].handcards.add(mc.getPlayerCardIndex<DDZPlayerCards>(i).handcards.toArray());
                cs.players[i] = mc.players[i];
            }

            cs.rule = mc.rule;
            cs.setRoomRule(mc.getRoomRule());
            return cs;
        }
    }
}
