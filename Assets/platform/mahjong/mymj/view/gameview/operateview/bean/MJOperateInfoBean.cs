using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 操作类型需要的牌的具体幸喜
    /// </summary>
    public class MJOperateInfoBean: BytesSerializable
    {
        /// <summary>
        /// 没有杠牌
        /// </summary>
        public const int NO_KONG_TYPE=-1;
        /// <summary>
        /// 操作类型,胡，碰，杠，过
        /// </summary>
        public int operateType;

        public MJOperationCardBean[] cards;

        /// <summary>
        /// 这个是通用的
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="cards"></param>
        /// <param name="fanshu"></param>
        public void setCommand(int operateType,int[] cards,int fanshu)
        {

        }


        public void setHu(int card,int fanshu)
        {
            operateType = MJOperate.CAN_HU;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE,card,fanshu);
            cards = new MJOperationCardBean[] { bean };
        }

        public void setCancel()
        {
            operateType = MJOperate.CAN_CANCEL;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        /// <summary>
        /// 报牌
        /// </summary>
        public void setBao()
        {
            operateType = MJOperate.CAN_BAOPAI;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        /// <summary>
        /// 报杠
        /// </summary>
        public void setBaoKong()
        {
            operateType = MJOperate.CAN_BAO_KONG;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        public void setTing()
        {
            operateType = MJOperate.CAN_LISTEN;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        /// <summary>
        /// 设置躺牌
        /// </summary>
        public void setTang()
        {
            operateType = MJOperate.CAN_TANG;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        /// <summary>
        /// 设置必须躺牌
        /// </summary>
        public void setMustTang()
        {
            operateType = MJOperate.MUST_TANG;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, 0, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        public void setPong(int card)
        {
            operateType = MJOperate.CAN_PONG;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, card, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        public void setChi(int card)
        {
            operateType = MJOperate.CAN_CHOW;
            MJOperationCardBean bean = new MJOperationCardBean(NO_KONG_TYPE, card, 0);
            cards = new MJOperationCardBean[] { bean };
        }

        /// <summary>
        /// 设置自己回合的杠
        /// </summary>
        /// <param name="cards">[0][1]:card,[0][0]:杠的类型，暗杠，巴杠</param>
        public void setSelfKong(int[][] cards)
        {
            operateType = MJOperate.CAN_KONG;
            this.cards = new MJOperationCardBean[cards.Length];
            for (int i=0;i<cards.Length;i++)
            {
                this.cards[i] = new MJOperationCardBean(cards[i][0],cards[i][1],0);
            }
        }

        public void setKong(int card)
        {
            operateType = MJOperate.CAN_KONG;
            MJOperationCardBean bean = new MJOperationCardBean(SendMJMatchCommand.KONG_PUB, card, 0);
            cards = new MJOperationCardBean[] { bean };
        }
    }
}
