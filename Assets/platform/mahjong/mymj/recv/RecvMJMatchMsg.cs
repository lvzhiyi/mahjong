using cambrian.common;

namespace platform.mahjong
{
    /// <summary>
    /// 接收麻将比赛
    /// </summary>
    public class RecvMJMatchMsg : RecvMsgHandle
    {
        /// <summary>
        /// 消息类型：开始消息
        /// </summary>
        public const int MSG_START = 101;
        /// <summary> 消息类型：更新玩家可操作状态（用于状态变化，但是无其他可通知信息得，单独通知操作状态） </summary>
        public const int MSG_UPDATE_STATES = 102;
        /// <summary> 消息类型：发牌 </summary>
        public const int MSG_DEALCARDS = 103;
        /// <summary> 消息类型：单个玩家定缺状态 </summary>
        public const int MSG_DISTYPE = 104;
        /// <summary> 消息类型：所有玩家定缺信息通知 </summary>
        public const int MSG_DISTYPE_ALL = 105;
        /// <summary> 消息类型：单个玩家换牌状态 </summary>
        public const int MSG_REPLACE = 106;
        /// <summary> 消息类型：所有玩家换牌信息通知（换牌方式，每个玩家要换的牌） </summary>
        public const int MSG_REPLACE_ALL = 107;
        /// <summary> 消息类型：加飘 </summary>
        public const int MSG_PIAO = 108;
        /// <summary> 消息类型：听牌 </summary>
        public const int MSG_LISTEN = 109;
        /// <summary> 消息类型：报牌 </summary>
        public const int MSG_BAOPAI = 110;
        /// <summary> 消息类型：摸牌 </summary>
        public const int MSG_DRAW = 111;
        /// <summary> 消息类型：出牌 </summary>
        public const int MSG_DISCARD = 112;
        /// <summary> 消息类型：胡牌 </summary>
        public const int MSG_HU = 113;
        /// <summary> 消息类性：杠</summary>
        public const int MSG_KONG = 114;
        /// <summary> 消息类型：碰牌 </summary>
        public const int MSG_PONG = 115;
        /// <summary> 消息类型：吃牌 </summary>
        public const int MSG_CHOW = 116;
        /// <summary> 消息类型：结束（被动消息） </summary>
        public const int MSG_OVER = 118;
        /// <summary> 消息类型：取消操作（一般托管发送） </summary>
        public const int MSG_CANCEL = 119;
        /// <summary>消息类型：飘结束（包含甩飘，和选飘）</summary>
        public const int MSG_PIAO_OVER = 120;
        /// <summary>消息类型：躺牌</summary>
        public const int MSG_LIE_CARD = 121;
        /// <summary>消息类型：单个人估卖</summary></summary>
        public const int MSG_SINGLE_SALE = 122;
        /// <summary>消息类型：所有人卖完估卖</summary></summary>
        public const int MSG_SALE = 123;
        /// <summary>消息类型：结算前翻牌定鸡</summary></summary>
        public const int MSG_OPEN_CARD = 124;
        /** 消息类型：消息类型：结算前翻牌定鸡 */
        public const int MSG_SUP_FLOWER = 125;
        /// <summary>消息类型：报杠</summary>
        public const int MSG_BAO_KONG = 126;
        /// <summary>
        /// 延迟操作
        /// </summary>
        public const int MSG_DELAY = 3000;

        /// <summary>
        /// 服务器端移除玩家
        /// </summary>
        public const int MSG_ROOM_REMOVE_PLAYER = 202;

        public RecvMJMatchMsg()
        {
            gamePlatform = GamePlatform.MJ_GAME;
        }


        private ByteBuffer data;

        public override void handle(ByteBuffer data)
        {
            if (Room.room == null)
            {
                Alert.show("房间为空，接收麻将比赛消息");
                return;
            }

            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);
            switch (gametype)
            {
               
                case GamePanelData.MY_GAME:
                    mYMsg(data);
                    break;
                case GamePanelData.AY_GAME:
                    new RecvAYMJMatchMsg(data).execute();
                    break;
            }
        }

        public void mYMsg(ByteBuffer data)
        {
            this.data = data;
            int type = data.readInt();
            int index = Room.room.indexOf();

            MJBaseOperate operate = recvOperate(type, index);

            if (operate != null)
                addRecvOperate(operate, data);
        }

        public MJBaseOperate recvOperate(int type, int index)
        {
            MJBaseOperate recvOperate = null;
            switch (type)
            {
                case MSG_START:
                    recvOperate = new MJMatchStartOperate(type, index);
                    break;
                case MSG_DEALCARDS:
                    recvOperate = new MJMatchDealCardsOperate(type,index);
                    break;
                case MSG_REPLACE:
                    recvOperate = new MJMatchSingleReplaceOperate(type,index);
                    break;
                case MSG_REPLACE_ALL:
                    recvOperate = new MJMatchAllReplaceOperate(type, index);
                    break;
                case MSG_DISTYPE:
                    recvOperate = new MJMatchSingleDistypeOperate(type,index);
                    break;
                case MSG_DISTYPE_ALL:
                    recvOperate=new MJMatchAllDisTypeOperate(type,index);
                    break;
                case MSG_DRAW:
                    recvOperate = new MJMatchDrawOperate(type,index);
                    break;
                case MSG_DISCARD:
                    recvOperate = new MJMatchDiscardOperate(type,index);
                    break;
                case MSG_PONG:
                    recvOperate = new MJMatchPongOperate(type, index);
                    break;
                case MSG_KONG:
                    recvOperate = new MJMatchKongOperate(type, index);
                    break;
                case MSG_HU:
                    recvOperate = new MJMatchHuOperate(type, index);
                    break;
                case MSG_OVER:
                    addDelayOperate();
                    recvOperate = new MJMatchOverOperate(type, index);
                    break;
                case MSG_CANCEL:
                    recvOperate = new MJMatchCancelOperate(type, index);
                    break;
                case MSG_UPDATE_STATES:
                    recvOperate = new MJMatchUpdateStateslOperate(type, index);
                    break;
                case MSG_PIAO:
                    recvOperate=new MJMatchSinglePiaoOperate(type,index);
                    break;
                case MSG_PIAO_OVER:
                    recvOperate=new MJMatchAllPiaoOverOperate(type,index);
                    break;
                case MSG_LIE_CARD:
                    recvOperate=new MJMatchTangOperate(type,index);
                    break;
            }

            return recvOperate;
        }


        public void addRecvOperate(MJBaseOperate recvOperate, ByteBuffer data)
        {
            recvOperate.bytesRead(data);
            MahJongPanel.getPanel().addRecvOperate(recvOperate);
        }

        public void addDelayOperate()
        {
            MahJongPanel.getPanel().addRecvOperate(new MJMatchDelayOperate(MSG_DELAY,0));
        }
    }
}
