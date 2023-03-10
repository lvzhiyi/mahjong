using cambrian.game;
using System;
using UnityEngine;

namespace cambrian.common
{
    /// <summary>
    /// 发送数据有返回
    /// </summary>
    public abstract class AccessCommand : TcpCommand
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected static Logger log = LogFactory.getLogger<AccessCommand>(true);

        public AccessCommand()
        {
        }

        public AccessCommand(short id)
            : base(id)
        {
        }

        public override void excute()
        {
            ByteBuffer data = new ByteBuffer();
            this.bytesWrite(data);
            if (log.isDebug)
            {
                this.excutetime = DateTime.UtcNow.Ticks/10000;
                Debug.Log(log.getMessage(this,
                    "id=" + this.id + " ,send , len=" + data.length + " ,time=" + this.excutetime));
            }
            DataAccessHandler.getInstance().access(this.getConnect(), this.id, data, this.onReceived);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="data"></param>
        void onReceived(ByteBuffer data)
        {
            int type = data.readUnsignedShort();

            if (type == Port.OK)
            {
                this.bytesRead(data);
            }
            else
            {
                if (type > 100 && type < 200)
                {
                    User.logout(User.user);
                }
                string str = data.readUTF();
                Alert.show(str);
            }

            if (this.callback == null) return;
            this.callback(this.callbackobj);
        }
    }
}