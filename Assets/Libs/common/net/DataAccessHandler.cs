using System;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 有返回类型的消息处理器
    /// </summary>
    public class DataAccessHandler : RecvPort
    {
        /// <summary>
        /// 通信超时时间为5秒
        /// </summary>
        public const int TIMEOUT = 5*1000;
        /// <summary>
        /// 负数通讯号
        /// </summary>
        private static int minusId;
        /// <summary>
        /// 当前的数据访问处理器
        /// </summary>
        private static DataAccessHandler handler;

        /// <summary>
        /// 获得当前的数据访问处理器
        /// </summary>
        /// <returns></returns>
        public static DataAccessHandler getInstance()
        {
            if (handler == null)
                handler = new DataAccessHandler();
            return handler;
        }
        /// <summary>
        /// 获取新的负数通讯号
        /// </summary>
        /// <returns></returns>
        public static int newMinusId()
        {
            minusId--;
            if (minusId >= 0)
                minusId = -1;
            return minusId;
        }

        /// <summary>
        /// 通信对象等待返回条目列表
        /// </summary>
        private Entry[] entryList = new Entry[1024];

        public DataAccessHandler()
        {
            this.id = ProxyDataHandler.PORT_ACCESS_RETURN;
        }

        /// <summary>
        /// 异步数据访问,发送
        /// </summary>
        /// <param name="c"></param>
        /// <param name="port"></param>
        /// <param name="data"></param>
        /// <param name="func"></param>

        public void access(TcpConnect c, short port, ByteBuffer data, Action<ByteBuffer> func)
        {
            Entry entry = new Entry(newMinusId(), TimeKit.currentTimeMillis, func);
            if (log.isDebug)
                Debug.Log(log.getMessage(this, " access,port=" + port + ",len=" + data.length + ",entry.id=" + entry.id));
            int id = (-entry.id)%1024;
            this.entryList[id] = entry;

            if (log.isDebug)
                Debug.Log("=======================c============================="+c);
            c.send(port, ProxyDataHandler.PORT_ACCESS_RETURN, entry.id, data);
        }
        /// <summary>
        /// 移除一个指定线程通讯号的线程访问条目,并清理超时的条目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Entry removeEntryById(int id)
        {
            Entry entry = this.entryList[(-id)%1024];
            this.entryList[(-id)%1024] = null;
            return entry;
        }

        /// <summary>
        /// 返回数据执行
        /// </summary>
        public override void excute()
        {
            int id = this.data.readInt();// 访问序列号seq
            Entry entry = this.removeEntryById(id);

            if (entry != null)
                entry.parseResultData(this.data);
        }

        public void collate(long time)
        {
            //if (log.isDebug)
            //    Debug.Log(log.getMessage(this, "collate, " + "time=" + time));
            for (int i=0;i<entryList.Length;i++)
            {
                if (entryList[i] == null|| entryList[i].time<0) continue;
                if(entryList[i].time+TIMEOUT<time)
                {
                    this.entryList = new Entry[1024];
                    throw new DataAccessException("网络超时");
                }
            }
        }
    }
}