using System;
using UnityEngine;

namespace cambrian.common
{
    /**
     * 类说明：通信条目
     *
     * @version 1.0
     * @author maxw<woldits@qq.com>
     */

    class Entry
    {

        /* static fields */

        /* fields */
        /** 线程通讯号 */
        public int id;
        /** 发送时间 */
        public long time;
        /** 返回消息的处理函数 */
        private Action<ByteBuffer> func;

        /* constructors */
        /** 构造一个访问条目，参数用于访问调用，指定线程通讯号和线程通讯数据 */

        public Entry(int id, long time, Action<ByteBuffer> func)
        {
            this.id = id;
            this.time = time;
            this.func = func;
        }

        /* method */
        /** 处理返回消息 */

        public void parseResultData(ByteBuffer data)
        {
            //int type = data.readUnsignedShort();
            //if (type == Command.OK)
            //{
                if (this.func != null)
                    this.func(data);
            //}
            //else
            //{
            //    string str = data.readUTF();
            //    Alert.show(str);
            //}
        }
    }
}