using cambrian.common;
using platform.spotred.room;
using UnityEngine;

namespace platform.spotred
{
    /// <summary>
    /// 接收比赛消息
    /// </summary>
    public class RecvCPMatchMsg : RecvMsgHandle
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected static cambrian.common.Logger log = LogFactory.getLogger<RecvCPMatchMsg>(true);
        /// <summary>
        /// 消息类型：发牌
        /// </summary>
        public const int MSG_DEALCARDS = 101;

        /// <summary>
        /// 消息类型：翻牌
        /// </summary>
        public const int MSG_OPENCARD = 102;

        /// <summary>
        /// 消息类型：摸牌
        /// </summary>
        public const int MSG_DRAW = 103;

        /// <summary>
        /// 消息类型：出牌
        /// </summary>
        public const int MSG_DISCARD = 104;

        /// <summary>
        /// 消息类型：胡牌
        /// </summary>
        public const int MSG_HU = 105;

        /// <summary>
        /// 消息类型：碰牌
        /// </summary>
        public const int MSG_PONG = 106;

        /// <summary>
        /// 消息类型：吃牌
        /// </summary>
        public const int MSG_CHOW = 107;

        /// <summary>
        /// 消息类型：昭碰
        /// </summary>
        public const int MSG_ZHAOPONG = 108;

        /// <summary>
        /// 消息类型：昭吃
        /// </summary>
        public const int MSG_ZHAOCHOW = 109;

        /// <summary>
        /// 消息类型：吃吐
        /// </summary>
        public const int MSG_CHOWTU = 110;

        /// <summary>
        /// 消息类型：偷成4根
        /// </summary>
        public const int MSG_COUNT4 = 111;

        /// <summary>
        /// 消息类型：显示偷牌
        /// </summary>
        public const int MSG_SHOWFIXED = 112;

        /// <summary>
        /// 消息类型：滑牌
        /// </summary>
        public const int MSG_SLIP = 113;

        /// <summary>
        /// 消息类型：结束（被动消息）
        /// </summary>
        public const int MSG_OVER = 114;

        /// <summary>
        /// 消息类型：掉坎
        /// </summary>
        public const int MSG_COUNT3 = 115;

        /// <summary>
        /// 更新状态
        /// </summary>
        public const int MSG_UPDATE_STATES = 116;
        /// <summary>
        /// 撕对
        /// </summary>
        public const int MSG_CHAIDUI = 117;

        /// <summary>
        /// 巴杠牌
        /// </summary>
        public const int MSG_KONG = 118;
        /// <summary>
        /// 杠4张
        /// </summary>
        public const int MSG_KONG4 = 119;
        /// <summary>
        /// 吃成坎
        /// </summary>
        public const int MSG_CHOW3 = 120;
        /// <summary>
        /// 报牌
        /// </summary>
        public const int MSG_BAOPAI = 121;
        /// <summary>
        /// 推挡
        /// </summary>
        public const int MSG_DANG = 122;
        /// <summary>
        /// 单张偷牌
        /// </summary>
        public const int MSG_SINGLE = 123;
        /// <summary>
        /// 过消息
        /// </summary>
        public const int MSG_CANCEL = 124;
        /// <summary>
        /// 接收后端炸牌消息，目前巴中打大用
        /// </summary>
        public const int MSG_ZHAN_PAI = 125;
        /// <summary>
        /// 接收破对消息
        /// </summary>
        public const int MSG_PO_DUI = 126;
        /// <summary>
        /// 接收可以破对的牌
        /// </summary>
        public const int MSG_CAN_PO_DUI = 127;
        /// <summary>
        /// 接收牌局开始消息(用于初始化界面)
        /// </summary>
        public const int MSG_MATCH_START = 128;
        /// <summary>
        /// 接受飘消息
        /// </summary>
        public const int MSG_PIAO = 129;
        /// <summary>
        /// 消息类型：吃成4根
        /// </summary>
        public const int MSG_CHOW4 = 130;
        /// <summary>
        /// 后对7
        /// </summary>
        public const int MSG_AFTER7 = 131;


        /** 金币场自己被移除房间 自定义 */
        public const int MSG_REMOVE_ROOM = 200;
        /** 金币场房间移除玩家 自定义  */
        public const int MSG_REMOVE_PLAYER = 201;
        /// <summary>
        /// 金币房卡场，移除玩家
        /// </summary>
        public const int MSG_CLUB_GOLD_ROOM_REMOVE_PLAYER = 202;


        public RecvCPMatchMsg()
        {
            this.gamePlatform = GamePlatform.CP_GAME;
        }

        private ByteBuffer data;
        public override void handle(ByteBuffer data)
        {
            if (Room.room == null)
            {
                Debug.LogError("Room is null");
                return;
            }

            this.data = data;

            int type = data.readInt();
            int step = data.readInt(); //步数
            int stage = data.readInt(); ///当前比赛阶段
            int round = data.readInt(); //当前比赛回合
            int paidui = data.readInt(); //当前牌堆剩余牌数
            int lengths = data.readInt();
            int[] operates = new int[lengths];
            for (int i = 0; i < lengths; i++)
                operates[i] = data.readInt();

            if (log.isDebug)
                Debug.Log(log.getMessage(this, ",type=" + type));

            BaseOperate operate = recvOperate(type, step, operates, Room.room.indexOf(), stage, round, paidui);
            if (operate != null)
                addRecvOperate(operate, data);
        }

        /// <summary>
        /// 一般房间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="step"></param>
        /// <param name="operates"></param>
        /// <param name="selfIndex"></param>
        /// <param name="stage"></param>
        /// <param name="round"></param>
        /// <param name="paidui"></param>
        /// <param name="recvOperate"></param>
        /// <returns></returns>
        public BaseOperate recvOperate(int type, int step, int[] operates, int selfIndex, int stage, int round, int paidui)
        {
            BaseOperate recvOperate = null;
            switch (type)
            {
                case MSG_MATCH_START:
                    recvOperate = new MatchStartOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_PIAO:
                    recvOperate = new PiaoOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_DEALCARDS:
                    DealCardsOperate discard = new DealCardsOperate(-1, step, operates, selfIndex, stage, round,
                        paidui);//-1,表示没有对应得操作，只是为发牌动画延长时间
                    DealCardsOverOperate recvop = new DealCardsOverOperate(type, step, operates, selfIndex, stage, round,
                        paidui);
                    recvop.bytesRead(data);
                    SpotRoomPanel spotRoomPanel = UnrealRoot.root.getDisplayObject<SpotRoomPanel>();
                    spotRoomPanel.addRecvOperate(discard);
                    spotRoomPanel.addRecvOperate(recvop);
                    return null;
                case MSG_DANG:
                    recvOperate = new DangOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_SINGLE:
                    recvOperate = new SingleOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_OPENCARD: //翻牌
                    recvOperate = new OpenCardOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_DISCARD: //出牌
                    recvOperate = new DisCardOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_HU: //胡
                    recvOperate = new HuOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_PONG: //碰
                    recvOperate = new PongOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_DRAW:
                    recvOperate = new DrawOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_BAOPAI: //报牌
                    recvOperate = new BaopaiOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CHOW: //吃
                    recvOperate = new ChowOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_ZHAOPONG:
                    recvOperate = new ZhaoPongOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_ZHAOCHOW:
                    recvOperate = new ZhaoChowOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CHOWTU:
                    recvOperate = new ChowTuOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_COUNT4:
                    recvOperate = new Count4Operate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_COUNT3: //掉坎
                    recvOperate = new Count3Operate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CHOW3:
                    recvOperate = new Chow3Operate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CHAIDUI: //撕对
                    recvOperate = new ChaiDuiOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_SHOWFIXED:
                    recvOperate = new ShowFixedOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_SLIP:
                    recvOperate = new SlipOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_OVER:
                    recvOperate = new OverOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_UPDATE_STATES:
                    recvOperate = new StateOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_KONG:
                    recvOperate = new KongOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_KONG4:
                    recvOperate = new Kong4Opreate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CANCEL:
                    recvOperate = new CancelOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_ZHAN_PAI:
                    recvOperate = new ZhaPaiOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_PO_DUI:
                    recvOperate = new PoDuiOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CAN_PO_DUI:
                    recvOperate = new CanPoDuiOperate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_CHOW4:
                    recvOperate = new Chow4Operate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
                case MSG_AFTER7:
                    recvOperate = new After7Operate(type, step, operates, selfIndex, stage, round, paidui);
                    break;
            }
            return recvOperate;
        }


        public void addRecvOperate(BaseOperate recvOperate, ByteBuffer data)
        {
            recvOperate.bytesRead(data);
            UnrealRoot.root.getDisplayObject<SpotRoomPanel>().addRecvOperate(recvOperate);
        }
    }
}
