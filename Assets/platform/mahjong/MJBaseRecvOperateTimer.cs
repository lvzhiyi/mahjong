using cambrian.common;
using UnityEngine;
using XLua;

namespace platform.mahjong
{
    public class MJBaseRecvOperateTimer:MonoBehaviour
    {
        private long time;
        Queue<MJBaseOperate> recvOperates = new Queue<MJBaseOperate>(100);
        [CSharpCallLua]
        [LuaCallCSharp]
        public delegate void ActionBack(ByteBuffer d);
        [CSharpCallLua]
        [LuaCallCSharp]
        public ActionBack action;

        private ByteBuffer data;

        [CSharpCallLua]
        [LuaCallCSharp]
        public void setAction(ActionBack action, ByteBuffer data)
        {
            this.data = new ByteBuffer(data.toArray());
            this.action = action;
        }

        public MJBaseOperate removeFirstRecvOperate()
        {
            if (recvOperates.isEmpty()) return null;

            return recvOperates.remove();
        }

        public void addOperate(MJBaseOperate operate)
        {
            this.recvOperates.add(operate);
        }

        /// <summary>
        /// 清空接收操作队列
        /// </summary>
        public void clearBaseOperate()
        {
            this.recvOperates.clear();
        }


        private MJBaseOperate operate;

        void Update()
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
                if (!operate.isStart)
                {
                    operate.isStart = true;
                    exec(operate);
                }
                else if (operate.isOver && curtime >= time)
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

        public void exec(MJBaseOperate operate)
        {
            exeOperate(operate.type, operate);
        }

        public virtual void exeOperate(int type, MJBaseOperate operate)
        {
           
        }
    }
}
