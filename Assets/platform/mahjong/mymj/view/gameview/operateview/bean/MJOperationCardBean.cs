using cambrian.common;

namespace platform.mahjong
{
    public class MJOperationCardBean: BytesSerializable
    {
        /// <summary>
        /// 巴杠，暗杠, 点杠
        /// </summary>
        public int type;
        /// <summary>
        /// 牌
        /// </summary>
        public int card;
        /// <summary>
        /// 番数
        /// </summary>
        public int fanshu;

        public MJOperationCardBean()
        {

        }

        public MJOperationCardBean(int type,int card,int fanshu)
        {
            this.type = type;
            this.card = card;
            this.fanshu = fanshu;
        }
    }
}
