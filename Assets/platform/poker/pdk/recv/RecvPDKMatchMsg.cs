using platform.bean;

namespace platform.poker
{
    /// <summary> 跑得快 比赛类接收 </summary>
    public class RecvPDKMatchMsg
    {
        public PKBaseOperate recvOperate(int type, int step, int[] operates, int stage, int round, int paidui)
        {
            PKBaseOperate operate = null;

            OperateData data = new OperateData(type, step, operates, stage, round, paidui);

            switch (type)
            {  
                case PDKMatchMsg.START:
                    operate = new PDKMStartGameOperate(data);
                    break;
                case PDKMatchMsg.DEALCARDS:
                    operate = new PDKMSystemDealCardOperate(data);
                    break;
                case PDKMatchMsg.START_MATCH:
                    operate = new PDKMStartMatchOperate(data);
                    break;
                case PDKMatchMsg.SHOWCARD:
                    operate = new PDKMShowCardOperate(data);
                    break;
                case PDKMatchMsg.CANCEL:
                    operate = new PDKMCancelOperate(data);
                    break;
                case PDKMatchMsg.OVER:
                    operate = new PDKMOverOperate(data);
                    break;
                default: break;
            }
            return operate;
        }
    }

    

    public class PDKMatchMsg
    {
        /// <summary> 消息类型：游戏开始 </summary>
        public const int START = 101;

        /// <summary> 消息类型：发牌 </summary>
        public const int DEALCARDS = 102;

        /// <summary> 消息类型：出牌 </summary>
        public const int SHOWCARD = 107;

        /// <summary> 消息类型：结束 </summary>
        public const int OVER = 108;

        /// <summary> 消息类型：取消操作 </summary>（一般托管发送）
        public const int CANCEL = 109;

        /// <summary> 消息类型：比赛开始前 </summary>（一般托管发送）
        public const int START_MATCH = 114;

        /// <summary>
        /// 更新炸弹分数(安岳跑得快)
        /// </summary>
        public const int UPDATE_MATCH_SCORE = 121;
    }
}
