using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 摸牌对象(用于显示的数据)
    /// </summary>
    public class MJMOCard:BytesSerializable
    {
        /// <summary>
        /// 牌
        /// </summary>
       // public int card = MJCard.CNO;
        public int card {  get;private set; }
        /// <summary>
        /// 0 表示没胡牌 1 表示第一个胡牌
        /// </summary>
        public int huIndex = 0;
        /// <summary>
        /// 胡牌类型(自摸,还是点炮，抢杠)
        /// </summary>
        public int hutype = -1;
        /// <summary>
        /// 方向
        /// </summary>
        public int index;
        /// <summary>
        /// 自摸亮牌
        /// </summary>
        public bool zimoShowCard;

        public MJMOCard()
        {
        }

        public MJMOCard(int card)
        {
            this.card = card;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="huIndex"></param>
        /// <param name="hutype"></param>
        /// <param name="index">谁点炮</param>
        public MJMOCard(int card,int huIndex,int hutype,int index,bool zimoShowCard) :this(card)
        {
            this.huIndex = huIndex;
            this.hutype = hutype;
            this.index = index;
            this.zimoShowCard = zimoShowCard;
        }

        /// <summary>
        /// 是否需要亮牌
        /// </summary>
        /// <returns></returns>
        public bool isBrightCard()
        {
            if (hutype == -1) return false;//没有胡牌
            if (MJMatchHuOperate.isZiMO(hutype) && !zimoShowCard)
                return false;
            return true;
        }

        public bool isHu()
        {
            return hutype >= 0;
        }
    }
}
