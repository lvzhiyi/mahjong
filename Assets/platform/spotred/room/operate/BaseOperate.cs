using cambrian.common;

namespace platform.spotred.room
{
    public class BaseOperate:BytesSerializable
    {
        /// <summary>
        /// 新增加的操作对象
        /// </summary>
        public object luaOperate;

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
        public long waittime=100;
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

        public bool isStart;

        public bool isRelay;

        public BaseOperate(int type,int step,int[] operates,int stage,int round,int paidui,bool isRelay)
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

        /// <summary>
        /// 获取等待的时间(单位毫秒)
        /// </summary>
        /// <returns></returns>
        public virtual long getWaittime()
        {
            return waittime;
        }

        public void playOver()
        {
            this.isOver = true;
        }
    }
}
