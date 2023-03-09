using cambrian.common;
using UnityEngine;

namespace platform.poker
{
    /// <summary> 接收服务器指令 操作队列 </summary>
    public class PKRecvOperateTimer : MonoBehaviour
    {
        public delegate void ActionBack(ByteBuffer data);

        public ActionBack action;

        private long time;

        private Queue<PKBaseOperate> recvOperates = new Queue<PKBaseOperate>(100);

        private ByteBuffer data;

        private PKBaseOperate operate;

        public virtual void setAction(ActionBack action, ByteBuffer data)
        {
            this.data = new ByteBuffer(data.toArray());
            this.action = action;
        }

        /// <summary> 移除队列第一个操作 </summary>
        public virtual PKBaseOperate removeFirstRecvOperate()
        {
            if (recvOperates.isEmpty()) return null;
            return recvOperates.remove();
        }

        /// <summary> 添加一个操作 </summary>
        public virtual void addOperate(PKBaseOperate operate)
        {
            recvOperates.add(operate);
        }

        /// <summary> 清空接收操作队列 </summary>
        public virtual void clearBaseOperate()
        {
            recvOperates.clear();
        }

        private void Update()
        {
            update();
        }

        public virtual void update()
        {
            long curtime = TimeKit.currentTimeMillis;

            if (time == 0)
            {
                time = curtime;
                return;
            }

            if (operate == null)
            {
                operate = removeFirstRecvOperate();
                if (operate != null)
                {
                    time = curtime + operate.getWaittime();
                }
            }

            if (operate != null)
            {
                if (!operate.operateData.isStart)
                {
                    operate.operateData.isStart = true;
                    exec(operate);
                }
                else if (operate.operateData.isOver && curtime >= time)
                {
                    time = curtime;
                    operate = null;
                }
            }

            if (operate == null && recvOperates.isEmpty())
            {
                if (action != null)
                {
                    action(data);
                    action = null;
                }
            }
        }

        public virtual void exec(PKBaseOperate operate) { }

    }
}
