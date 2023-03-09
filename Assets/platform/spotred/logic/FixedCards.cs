using cambrian.common;

namespace platform.spotred
{
    /// <summary>
    /// 对,吃,划牌，固定牌
    /// </summary>
    public class FixedCards:BytesSerializable
    {
        //-----------麻将------------------
        /** 固定牌类型：碰牌 */
        public const int MJPONG = 1;
        /// <summary>
        /// 固定牌类型： 暗牌
        /// </summary>
        public const int MJKONG_PRI = 2;
        /// <summary>
        /// 固定牌类型： 明杠
        /// </summary>
        public const int MJKONG_PUB = 3;
        /// <summary>
        /// 固定牌类型： 补杠（巴杠）
        /// </summary>
        public const int MJKONG_SUP = 4;
        /** 固定牌类型： 吃牌 */
        public const int MJCHOW = 5;
        /** 固定牌类型： 补花牌 */
        public const int MJFLOWER = 6;

        /// <summary>
        /// 该类型用做前端，把补杠的牌显示成半透明状态
        /// </summary>
        public const int MJONG_SUP_WAIT = 7;
        /// <summary>
        /// 报杠的牌
        /// </summary>
        public const int MJ_BAO_KONG = 8;

        //------------长牌------------------

        /** 固定牌类型： 开局偷牌滑四张 */
        public static int KONG = 1;
        /** 固定牌类型： 开局偷牌滑三张或碰牌 */
        public static int PONG = 2;
        /** 固定牌类型： 吃牌 */
        public static int CHOW = 3;
        /** 固定牌类型： 财神听用单张滑牌 */
        public static int SINGLE = 4;

        /** 类型：滑牌，碰牌，DSSMsg中定义 */
        public int type;
        /** 包含的牌：数组是因为可能这里的牌并不相同 */
        public int[] cards;
        /// <summary>
        /// 一般为来源者索引
        /// </summary>
        public int source;

        public FixedCards()
        {
            // 序列化用此构造
        }

        public FixedCards(int type, int[] cards)
        {
            this.type = type;
            this.cards = cards;
        }

        /** 获取牌数组 */

        public int[] getCards()
        {
            return this.cards;
        }

        /// <summary>
        /// 增加一张相同的牌
        /// </summary>
        public virtual void addSameCards(int card)
        {
            int len = this.cards.Length+1;
            this.cards=new int[len];
            for (int i = 0; i < len; i++)
            {
                this.cards[i] = card;
            }
            this.type = KONG;
        }

        /// <summary>
        /// 是否是全部相同的牌(牌,几张相同)
        /// </summary>
        public virtual bool isSameCard(int card,int count)
        {
            if (cards.Length != count) return false;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != card) return false;
            }

            return true;
        }

        /// <summary>
        /// 是否有相同的牌
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public virtual bool hasSameCard(int card)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == card) return true;
            }

            return false;
        }

        /** 是否是全红 */

        public bool isAllRed()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (Card.isBlack(cards[i])) return false;
            }
            return true;
        }

        /** 是否是全黑 */

        public bool isAllBlack()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (Card.isRed(cards[i])) return false;
            }
            return true;
        }

        public virtual bool isKongPri()
        {
            return false;
        }

        public virtual FixedCards clone()
        {
            FixedCards fixedCards = new FixedCards();
            fixedCards.type = type;
            fixedCards.source = source;

            fixedCards.cards = new int[cards.Length];
            for (int i = 0; i < fixedCards.cards.Length; i++)
            {
                fixedCards.cards[i] = cards[i];
            }
            return fixedCards;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(cards.Length);
            for (int i = 0; i < cards.Length; i++)
            {
                data.writeInt(cards[i]);
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            type = data.readInt();
            int len = data.readInt();
            cards = new int[len];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = data.readInt();
            }
        }
    }
}