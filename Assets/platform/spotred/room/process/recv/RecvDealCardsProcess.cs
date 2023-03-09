using platform.spotred;

namespace platform.spotred.room
{
    /// <summary>
    /// 接收发牌消息
    /// </summary>
    public class RecvDealCardsProcess:Process
    {
        /// <summary>
        /// 步数
        /// </summary>
        private int step;
        /// <summary>
        /// 第几局
        /// </summary>
        private int tray;
        /// <summary>
        /// 庄家
        /// </summary>
        private int banker;
        /// <summary>
        /// 挡家
        /// </summary>
        private int dangjia;
        /// <summary>
        /// 小家
        /// </summary>
        private int xiajia;
        /// <summary>
        /// 每个玩家的牌
        /// </summary>
        private int[][] cards;
        /// <summary>
        /// 操作
        /// </summary>
        int operate;
       
        private int[] disablecard;

        private int stage;

        private int round;

        private int paidui;


        private BaseOperate dealCardsOperate;


        public RecvDealCardsProcess(int step,int tray,int banker,int dangjia,int xiaojia,int[][] cards,int operate,int[] disablecard,int stage,int paiduan,BaseOperate dealCardsOperate)
        {
            this.step=step;
            this.tray = tray;
            this.banker = banker;
            this.dangjia = dangjia;
            this.xiajia = xiaojia;
            this.cards = cards;
            this.operate = operate;
            this.disablecard = disablecard;
            this.stage = stage;
            this.paidui = paiduan;
            this.dealCardsOperate = dealCardsOperate;
        }


        public override void execute()
        {
            ///开始比赛
            SpotRoomPanel panel=UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
            panel.setOperate(operate);
            CPMatch.match.setStage(stage);
            CPMatch.match.DealCards(cards);
            CPMatch.match.paidui=paidui;//发完牌后剩余的牌
            CPMatch.match.step=step;
          
            panel.refreshCardNum();
            panel.ShowMatchPlayerInfo();

            //int index= Room.room.indexOf();
            //panel.allHandView.selfView.getHandView().DealCard(CPMatch.match.GetMathPlayerHand(index));

            this.dealCardsOperate.playOver();
        }
    }
}
