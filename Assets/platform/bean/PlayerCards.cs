using cambrian.common;
using platform.spotred;
using System;

namespace platform
{
    /// <summary>
    /// 单个玩家的牌
    /// </summary>
    public abstract class PlayerCards:BytesSerializable
    {
        /// <summary>
        /// 手牌
        /// </summary>
        public ArrayList<int> handcards;
        /// <summary>
        /// 碰 吃(固定牌)
        /// </summary>
        public ArrayList<FixedCards> fixCards;
        /// <summary>
        /// (出,翻)的牌
        /// </summary>
        public ArrayList<int> disableCards;
        /// <summary>
        /// 本局分数
        /// </summary>
        public long point = 0;
        /// <summary>
        /// 番数
        /// </summary>
        public int fanshu = -1;
        /// <summary>
        /// 结算分数
        /// </summary>
        public SpotRedCount count;
        /// <summary>
        /// 胡牌加番类型
        /// </summary>
        public long hu_type = 0;
        /// <summary>
        /// 加根数量
        /// </summary>
        public int root_count = 0;
        /// <summary>
        /// 福数
        /// </summary>
        public int fu_count = 0;
        /// <summary>
        /// 比赛中的状态
        /// </summary>
        public int status;

        public PlayerCards()
        {
            handcards = new ArrayList<int>();
            fixCards = new ArrayList<FixedCards>();
            disableCards = new ArrayList<int>();
            count=new SpotRedCount();
        }

        /// <summary>
        /// 添加所有的手牌
        /// </summary>
        public virtual void AddHandAllCards(int[] cards)
        {
            handcards.clear();
            handcards.add(cards);
        }

        /// <summary>
        /// 添加手牌（发牌）
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="isBanker">是否是庄家</param>
        public virtual void dealPlayerCards(int[] cards)
        {
            handcards.clear();
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        public void setStatus(int status)
        {
            this.status |= status;
        }

        /// <summary>
        /// 是否是有指定状态
        /// </summary>
        public bool hasStatus(int status)
        {
            return status ==(this.status & status);
        }

        public void delStatus(int status)
        {
            this.status &= (~status);
        }

        /// <summary>
        /// 增加牌到手上
        /// </summary>
        /// <param name="card"></param>
        public void AddHandCards(int card)
        {
            handcards.add(card);
        }

        /// <summary>
        /// 增加牌到手上
        /// </summary>
        public virtual ArrayList<int> AddHandCards(int[] card)
        {
            handcards.add(card);
            return handcards;
        }

        public virtual ArrayList<int> addHszCards(int[] cards)
        {
            return null;
        }

        /// <summary>
        /// 获取指定手牌的数量
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int getCardCount(int card)
        {
            int count = 0;
            for (int i = 0; i < handcards.count; i++)
            {
                if (handcards.get(i)==card)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///减少手牌  （返回剩余的手牌）
        /// </summary>
        /// <param name="cards"></param>
        public virtual ArrayList<int> delHandCards(int[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = handcards.count-1; j >=0; j--)
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

        /// <summary>
        /// 删除相同的牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="count"></param>
        /// <returns>返回剩余的牌</returns>
        public virtual ArrayList<int> delHandCard(int card,int count)
        {
            int[] c=new int[count];
            for (int i = 0; i < count; i++)
            {
                c[i] = card;
            }
            return delHandCards(c);
        }
        /// <summary>
        /// 删除相同的牌
        /// </summary>
        /// <param name="card"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public ArrayList<int> delAllSameCard(int card,int num)
        {
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < handcards.count; j++)
                {
                    if (handcards.get(j) == card)
                    {
                        handcards.removeAt(j);
                        break;
                    }
                }
            }  
            return handcards;
        }


        public abstract void addFixCard(int type,int[] card);
        /// <summary>
        /// 获取全部相同的牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract FixedCards getSameFixedCards(int card, int count);

        public void addFixCard(FixedCards[] cards)
        {
            fixCards.add(cards);
        }
        public void addFixedCard(FixedCards cards)
        {
            fixCards.add(cards);
        }

        /// <summary>
        /// 增加一张出牌区的牌  正常出牌
        /// </summary>
        /// <param name="card"></param>
        public void addDisableCard(int card)
        { 
            disableCards.add(card);
        }

        /// <summary>
        /// 增加出牌区的牌
        /// </summary>
        /// <param name="card"></param>
        public void addDisableCards(int[] cards)
        {
            disableCards.add(cards);
        }

        public void removeLastDisbaleCard()
        {
            disableCards.removeLast();
        }

        public void updateDisableLastSign(int card)
        {
            disableCards.set(card,disableCards.count-1);
        }

        /// <summary>
        /// 获取出牌区
        /// </summary>
        /// <returns></returns>
        public ArrayList<int> getDisableCard()
        {
            return disableCards;
        }

        public override void bytesRead(ByteBuffer data)
        {
            throw new Exception("is need override");
        }

        public virtual PlayerCards clone(PlayerCards pc)
        {
            throw new Exception("is need override");
        }

        /// <summary>
        /// 单局结算的时候用
        /// </summary>
        /// <param name="data"></param>
        public virtual void bytesReadSingleOver(ByteBuffer data)
        {
            throw new Exception("is need override");
        }

        /// <summary>
        /// 获取排序好的手牌
        /// </summary>
        public virtual int[] getSortHandCards()
        {
            throw new Exception("is need override");
        }
    }
}