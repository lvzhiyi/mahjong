using cambrian.common;
using scene.game;

namespace platform.spotred.room
{
    /// <summary>
    /// 长牌-前端发送-比赛操作
    /// </summary>
    public class SendMatchCommand:SendCommand
    {
        /// <summary>
        /// 操作类型：出牌
        /// </summary>
        public static  int DISCARD = 201;
        /// <summary>
        /// 操作类型：胡牌
        /// </summary>
        public static  int HU = 202;
        /// <summary>
        /// 操作类型：碰牌
        /// </summary>
        public static  int PONG = 203;
        /// <summary>
        /// 操作类型：吃牌
        /// </summary>
        public static  int CHOW = 204;
        /// <summary>
        /// 操作类型：昭碰
        /// </summary>
        public static  int ZHAOPONG = 205;
        /// <summary>
        /// 操作类型：昭吃
        /// </summary>
        public static  int ZHAOCHOW = 206;
        /// <summary>
        /// 操作类型：吃吐
        /// </summary>
        public static  int CHOWTU = 207;
        /// <summary>
        /// 操作类型：取消
        /// </summary>
        public static  int CANCEL = 208;
        /// <summary>
        /// 操作类型：滑牌
        /// </summary>
        public static int SLIP = 209;
        /// <summary>
        /// 巴杠牌
        /// </summary>
        public static  int KONG = 210;
        /// <summary>
        /// 暗杠4张
        /// </summary>
        public static  int KONG4 = 211;
        /// <summary>
        /// 报牌
        /// </summary>
        public static int BAOPAI = 212;
        /// <summary>
        /// 挡
        /// </summary>
        public static int DANG = 213;
        /// <summary>
        /// 单张偷牌
        /// </summary>
        public static int SINGLE = 214;
        /// <summary>
        /// 飘
        /// </summary>
        public static int PIAO = 215;

        int type;
        /// <summary>
        /// 牌
        /// </summary>
        int card;
        /// <summary>
        /// 数量
        /// </summary>
        int count;
        /// <summary>
        /// 步数
        /// </summary>
        int step;

        private int[][] slipCards;


         
        public SendMatchCommand(int type,int step,int card,int count,int[][] slipCards)
        {
            this.id = CommandConst.COMMAND_ROOM_MATCH_OPTION;
            this.type = type;
            this.card = card;
            this.count = count;
            this.step = step;
            this.slipCards = slipCards;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(type);
            data.writeInt(step);
            if (type== DISCARD)
            {
                data.writeInt(card);
            }
            else if(type== HU)
            {
               
            }
            else if (type==PONG)
            {
                data.writeInt(card);
                data.writeInt(count);
            }
            else if (type == CHOW)
            {
                data.writeInt(card);//用手里的哪张牌吃
            }
            else if (type == ZHAOPONG)
            {
               
            }

            else if (type == ZHAOCHOW)
            {
               
            }
            else if (type == CHOWTU)
            {
               
            }
            else if (type == CANCEL)
            {
               
            }
            else if (type == SLIP)
            {
                int len = slipCards.Length;
                data.writeInt(len);
                for (int i = 0; i < len; i++)
                {
                    int[] cs = slipCards[i];
                    for (int j = 0; j < cs.Length; j++)
                        data.writeInt(cs[j]);
                }
            }
            else if (type == KONG)
            {
                data.writeInt(card);
            }
            else if (type == KONG4)
            {
                data.writeInt(card);
            }
            else if (type == BAOPAI)
            {

            }
            else if (type == DANG)
            {

            }
            else if (type == SINGLE)
            {

            }
            else if (type== PIAO)
            {

            }
        }
    }
}
