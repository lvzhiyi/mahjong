using cambrian.common;
using scene.game;

namespace platform.mahjong
{
    public class SendMJMatchCommand:SendCommand
    {
        // ===============阶段操作===============
        /// <summary> 操作类型：换牌（换三张） </summary>
        public const int REPLACE = 101;
        /// <summary> 操作类型：定缺 </summary>
        public const int DISTYPE = 102;

        // ===============行牌操作===============
        /// <summary> 操作类型：报胡（起手下叫可行操作） </summary>
        public const int BAO = 201;
        /// <summary>
        /// 操作类型：出牌
        /// </summary>
        public const int DISCARD = 202;
        /// <summary> 操作类型：碰牌 </summary>
        public const int PONG = 203;
        /// <summary> 操作类型：吃牌 </summary>
        public const int CHOW = 204;
        /// <summary> 操作类型：明杠 </summary>
        public const int KONG_PUB = 205;
        /// <summary> 操作类型：暗杠 </summary>
        public const int KONG_PRI = 206;
        /// <summary> 操作类型：补杠 </summary>
        public const int KONG_SUP = 207;
        /// <summary> 操作类型：补花 （可能是被动操作，先定义在这里） </summary>
        public const int SUP_FLOWER = 208;
        /// <summary> 操作类型：听牌（下好叫后操作，一般操作后不能更换手牌） </summary>
        public const int LISTEN = 209;
        /// <summary> 操作类型：飘（加番可选项） </summary>
        public const int PIAO = 210;
        /// <summary> 操作类型：点炮胡牌 </summary>
        public const int HU = 211;
        /// <summary> 操作类型：抢杠胡牌 </summary>
        public const int HU_ROB = 212;
        /// <summary> 操作类型：自摸胡牌 </summary>
        public const int HU_SELF = 213;
        /// <summary> 操作类型：取消 </summary>
        public const int CANCEL = 214;
        /// <summary>
        /// 操作类型-躺牌
        /// </summary>
        public const int LIE_CARD = 215;
        /// <summary>
        /// 操作类型-卖分
        /// </summary>
        public const int MAIFEN = 216;
        /// <summary>
        /// 操作类型-报杠
        /// </summary>
        public const int BAO_KONG = 217;
        /// <summary>
        /// 操作类型-唱歌
        /// </summary>
        public const int SING = 218;


        int type;

        int step;
        /// <summary>
        /// 定缺类型
        /// </summary>
        int card;
        /// <summary>
        /// 多个牌
        /// </summary>
        int[] cards;
        /// <summary>
        /// 飘不飘
        /// </summary>
        private bool isPiao;

        public SendMJMatchCommand(int type, int step)
        {
            id = CommandConst.COMMAND_ROOM_MATCH_OPTION;
            this.type = type;
            this.step = step;
        }

        public SendMJMatchCommand(int type, int step,int card) : this(type, step)
        {
            this.card = card;
        }


        public SendMJMatchCommand(int type,int step,int[] cards):this(type,step)
        {
            this.cards = cards;
        }

        public SendMJMatchCommand(int type,int step,bool isPiao):this(type,step)
        {
            this.isPiao = isPiao;
        }

        public SendMJMatchCommand(int type,int step,int card,int[] cards):this(type,step)
        {
            this.cards = cards;
            this.card = card;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(step);

            switch (type)
            {
                case REPLACE:
                    data.writeInt(cards.Length);
                    for (int i = 0; i < cards.Length; i++)
                    {
                        data.writeInt(cards[i]);
                    }
                    break;
                case DISTYPE:
                    data.writeInt(card);//牌类型
                    break;
                case DISCARD:
                    data.writeInt(card);//牌类型
                    break;
                case PONG:
                case KONG_PUB:
                case KONG_PRI:
                case KONG_SUP:
                case HU:
                case HU_ROB:
                case HU_SELF:
                    data.writeInt(card);
                    break;
                case CANCEL:
                    break;
                case BAO:
                    data.writeInt(card);
                    break;
                case PIAO:
                    data.writeBoolean(isPiao);
                    break;
                case LISTEN:
                    data.writeBoolean(isPiao);
                    break;
                case SUP_FLOWER:
                    data.writeInt(card);
                    break;
                case LIE_CARD:
                    data.writeInt(card);
                    data.writeInt(cards.Length);
                    for (int i = 0; i < cards.Length; i++)
                    {
                        data.writeInt(cards[i]);
                    }
                    break;
                case MAIFEN:
                    data.writeInt(card);//这里的card其实是分数
                    break;
                case CHOW:
                    data.writeInts(cards);
                    break;
                case BAO_KONG:
                    data.writeInts(cards);
                    break;
                case SING:
                    data.writeInt(card);
                    break;

            }
        }

    }
}
