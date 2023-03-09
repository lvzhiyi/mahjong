using cambrian.common;

namespace platform.bean
{
    public class OperateData: BytesSerializable
    {
        public int index;
        /// <summary>
        /// 类型
        /// </summary>
        public int type;
        /// <summary>
        /// 步数
        /// </summary>
        public int step;
        /// <summary>
        /// 操作类型
        /// </summary>
        public int[] operates;
        /// <summary>
        /// 等待时间
        /// </summary>
        public long waittime = 100;
        /// <summary>
        /// 当前比赛阶段
        /// </summary>
        public int stage;
        /// <summary>
        /// 当前比赛回合
        /// </summary>
        public int round;
        /// <summary>
        /// 当前牌堆剩余牌数
        /// </summary>
        public int paidui;
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool isOver;
        /// <summary>
        /// 是否开始
        /// </summary>
        public bool isStart;
        /// <summary>
        /// 是否是回放
        /// </summary>
        public bool isRelay;

        public OperateData(int type,int step,int[] operates,int stage,int round,int paidui,bool isRelay=false)
        {
            this.type = type;
            this.step = step;
            this.operates = operates;
            this.stage = stage;
            this.round = round;
            this.paidui = paidui;
            this.isRelay = isRelay;
        }

        public override void bytesRead(ByteBuffer data)
        {
            
        }
    }
}
