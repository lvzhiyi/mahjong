using cambrian.common;
using platform.spotred;
using System;

/// <summary>
/// 牌局逻辑
/// </summary>
namespace platform
{
    /// <summary>
    /// 比赛类
    /// </summary>
    public abstract class Match: BytesSerializable
    {
        /// <summary>
        /// 比赛阶段
        /// </summary>
        protected int stage;
        /// <summary>
        /// 庄家索引
        /// </summary>
        public int banker { set; get; }
        /// <summary>
        /// 剩下牌总数
        /// </summary>
        public int paidui { get; set; }
        /// <summary>
        /// 自己的索引
        /// </summary>
        public int mindex { get; set; }
        /// <summary>
        /// 步数
        /// </summary>
        public int step { get; set; }
        /// <summary>
        /// 回合
        /// </summary>
        protected int round;
        /// <summary>
        /// 比赛玩家数组
        /// </summary>
        public MatchPlayer[] players;
        /// <summary>
        /// 玩家的牌（位置与matchpalyer[] 位置一样）
        /// </summary>
        protected PlayerCards[] pcards;

        /// <summary>
        /// 比赛规则
        /// </summary>
        protected RoomRule rule;
        /// <summary>
        /// 牌局结束牌堆剩余的牌
        /// </summary>
        public int[] remainingCards;
        /// <summary>
        /// 获取自己的手牌数组
        /// </summary>
        /// <returns></returns>
        public abstract int[] getSelfHandCard();
        /// <summary>
        /// 获取所有玩家的牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] getPlayerCardss<T>() where T : PlayerCards
        {
            return (T[])pcards;
        }

        public PlayerCards[] getPCards()
        {
            return pcards;
        }

        /// <summary>
        /// 获取自己的手牌对象
        /// </summary>
        /// <returns></returns>
        public T getSelfPlayerCards<T>() where T : PlayerCards
        {
            return (T)pcards[mindex];
        }
        /// <summary>
        /// 获取指定玩家的手牌对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T getPlayerCardIndex<T>(int index) where T:PlayerCards
        {
            return (T)pcards[index];
        }
        /// <summary>
        /// 获取每个人总的结算信息
        /// </summary>
        /// <returns></returns>
        public abstract SpotRedCount[] getSpotRedCounts();
        /// <summary>
        /// 系统发牌
        /// </summary>
        public abstract void DealCards(int[][] cards);
        /// <summary>
        /// 移除手牌
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        /// <param name="count"></param>
        /// <returns>剩余的手牌</returns>
        public abstract ArrayList<int> removeHandCard(int index, int card, int count);
        /// <summary>
        /// 获取指定玩家的手牌
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract int[] GetMathPlayerHand(int index);

        /// <summary>
        /// 设置比赛阶段
        /// </summary>
        /// <param name="stage"></param>
        public void setStage(int stage)
        {
            this.stage = stage;
        }

        public void setData(int stage,int step,int paidui,int round)
        {
            setStage(stage);
            this.step = step;
            this.paidui = paidui;
            this.round = round;
        }

        /// <summary>
        /// 设置回合
        /// </summary>
        public void setRound(int round)
        {
            this.round = round;
        }

        /// <summary>
        /// 是否是指定阶段
        /// </summary>
        /// <returns></returns>
        public bool isstage(int stage)
        {
            return this.stage == stage;
        }

        public int getStage()
        {
            return stage;
        }

        /// <summary>
        /// 比赛玩家,自己的位置
        /// </summary>
        /// <param name="players"></param>
        /// <param name="mine"></param>
        public void setPlayers(MatchPlayer[] players, int mine)
        {
            this.players = players;
            this.mindex = mine;
        }

        /// <summary>
        /// 是否是自己的回合
        /// </summary>
        /// <returns></returns>
        public bool isRound(int index)
        {
            return round == index;
        }

        public bool isMineRound()
        {
            return true;
        }

        /// <summary>
        /// 设置规则
        /// </summary>
        /// <param name="rule"></param>
        public virtual void setRoomRule(RoomRule rule)
        {
            this.rule = rule;
        }

        public RoomRule getRoomRule()
        {
            return rule;
        }

        /// <summary>
        /// 获取牌堆剩余牌组
        /// </summary>
        /// <returns></returns>
        public int[] getRemainingCards()
        {
            return this.remainingCards;
        }

        /// <summary>
        /// 单局结算的时候，在用
        /// </summary>
        /// <param name="data"></param>
        public virtual void bytesReadAll(ByteBuffer data)
        {
            for (int i = 0; i < pcards.Length; i++)
            {
                this.pcards[i].bytesRead(data);
            }

            int len = data.readInt();
            this.remainingCards = new int[len];
            for(int i=0;i<len;i++)
            {
                this.remainingCards[i] = data.readInt();
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.stage = data.readInt();
            this.step = data.readInt();
            this.banker = data.readInt();
        }
       
        /// <summary>
        /// 回放拷贝比赛数据
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public virtual Match cloneCardScene(Match match)
        {
            throw new Exception("need is override");
        }
    }
}