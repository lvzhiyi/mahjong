using platform.bean;
using UnityEngine;

namespace platform.poker
{
    /// <summary> 斗地主 比赛类接收 </summary>
    public class RecvDDZMatchMsg
    {
        public PKBaseOperate recvOperate(int type, int step, int[] operates, int stage, int round, int paidui)
        {
            PKBaseOperate operate = null;

            OperateData data = new OperateData(type, step, operates, stage, round, paidui);
            switch (type)
            {    /*------------------基础阶段------------------*/
                case DDZMatchMsg.START:
                    operate = new DDZMStartGameOperate(data);
                    break;
                case DDZMatchMsg.DEALCARDS:
                    operate = new DDZMSystemDealCardOperate(data);
                    break;
                case DDZMatchMsg.FLOW:
                    operate = new DDZMFlowGameOperate(data);
                    break;
                case DDZMatchMsg.DEALLADNLORDCARD:
                    operate = new DDZMDealLandlordCardOperate(data);
                    break;
                case DDZMatchMsg.SHOWCARD:
                    operate = new DDZMShowCardOperate(data);
                    break;
                case DDZMatchMsg.CANCEL:
                    operate = new DDZMCancelOperate(data);
                    break;
                case DDZMatchMsg.OVER:
                    operate = new DDZMOverOperate(data);
                    break;
                /*------------------特殊阶段------------------*/
                case DDZMatchMsg.LANDLORDCALL:
                    operate = new DDZMLandlordCallOperate(data);
                    break;
                case DDZMatchMsg.LANDLORDGRAB:
                    operate = new DDZMLandlordGrabOperate(data);
                    break;
                case DDZMatchMsg.JIABEI:
                    operate = new DDZMJiaBeiOperate(data);
                    break;
                case DDZMatchMsg.MINGPAI:
                    operate = new DDZMMingPaiOperate(data);
                    break;
                case DDZMatchMsg.MINGPAI_CANCEL:
                    operate = new DDZMMingPaiCancelOperate(data);
                    break;
                case DDZMatchMsg.LANDLORD_CALLSCORE:
                    operate = new DDZMCallScoreOperate(data);
                    break;
            }
            return operate;
        }
    }
    public class DDZMatchMsg
    {
        /// <summary> 消息类型：游戏开始 </summary>
        public const int START = 101;

        /// <summary> 消息类型：发牌 </summary>
        public const int DEALCARDS = 102;

        /// <summary> 消息类型：叫地主 </summary>
        public const int LANDLORDCALL = 103;

        /// <summary> 消息类型：抢地主 </summary>
        public const int LANDLORDGRAB = 104;

        /// <summary> 消息类型：加倍 </summary>
        public const int JIABEI = 105;

        /// <summary> 消息类型：明牌</summary>
        public const int MINGPAI = 106;

        /// <summary> 消息类型：出牌 </summary>
        public const int SHOWCARD = 107;

        /// <summary> 消息类型：结束 </summary>
        public const int OVER = 108;

        /// <summary> 消息类型：取消操作 </summary>
        public const int CANCEL = 109;

        /// <summary> 消息类型：发地主牌 </summary>
        public const int DEALLADNLORDCARD = 110;

        /// <summary> 消息类型：流局 </summary>
        public const int FLOW = 111;

        /// <summary> 消息类型：明牌取消 </summary>
        public const int MINGPAI_CANCEL = 112;

        /// <summary> 消息类型：叫分 </summary>
        public const int LANDLORD_CALLSCORE = 113;
    }
}
