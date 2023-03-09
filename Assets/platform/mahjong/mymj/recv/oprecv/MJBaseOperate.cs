using cambrian.common;

namespace platform.mahjong
{
    public class MJBaseOperate:BytesSerializable
    {
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
        /// 自己的索引
        /// </summary>
        public int selfIndex;
        /// <summary>
        /// 等待时间
        /// </summary>
        public long waittime = 5;
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


        public MJBaseOperate(int type,int selfIndex,bool isRelay=false)
        {
            this.type = type;
            this.selfIndex = selfIndex;
            this.isRelay = isRelay;
        }

        public int getSelfOperate()
        {
            return operates[selfIndex];
        }

        public override void bytesRead(ByteBuffer data)
        {
            step = data.readInt(); //步数
            stage = data.readInt(); //当前比赛阶段
            round = data.readInt(); //当前比赛回合
            paidui = data.readInt(); //当前牌堆剩余牌数
            int lengths = data.readInt();
            operates = new int[lengths];
            for (int i = 0; i < lengths; i++)
                operates[i] = data.readInt();
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
