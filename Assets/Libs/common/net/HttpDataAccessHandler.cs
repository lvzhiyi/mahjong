using System.IO;
using System.Net;
using UnityEngine;

namespace cambrian.common
{
    public class HttpDataAccessHandler : MonoBehaviour
    {
        void Awake()
        {
            handler = this;
            this.entries = new ArrayList<HttpEntry>();
        }
        void Update()
        {
            this.excute();
        }

        /// <summary>
        /// 日志
        /// </summary>
        protected static Logger log = LogFactory.getLogger<HttpDataAccessHandler>(true);
        /// <summary>
        /// 默认的线程等待时间为10秒,（ms）
        /// </summary>
        public const long TIMEOUT = 10*1000;
        /// <summary>
        /// 当前的数据访问处理器
        /// </summary>
        private static HttpDataAccessHandler handler;
        /// <summary>
        /// 获得当前的数据访问处理器
        /// </summary>
        /// <returns></returns>
        public static HttpDataAccessHandler getInstance()
        {
            return handler;
        }

        /// <summary>
        /// 负数通讯号
        /// </summary>
        private int minusId;
        /// <summary>
        /// 等待返回的
        /// </summary>
        ArrayList<HttpEntry> entries;

        /// <summary>
        /// 获取新的负数通讯号
        /// </summary>
        /// <returns></returns>
        public int newMinusId()
        {
            this.minusId--;
            if (this.minusId >= 0)
                this.minusId = -1;
            return this.minusId;
        }
        /// <summary>
        /// 异步数据访问,发送
        /// </summary>
        /// <param name="command"></param>
        public void access(SimpleHttpCommand command)
        {
            long time = TimeKit.currentTimeMillis;
            HttpEntry entry = new HttpEntry(this.newMinusId(), time, command);
            this.entries.add(entry);
            if (log.isDebug)
                Debug.Log(log.getMessage(this, " access,url=" + command.url + ",entry.id=" + entry.id));

            if(time<0) time=(long)(Time.realtimeSinceStartup*1000);
            string url = command.url;
            if (url.IndexOf('?') == -1) url += "?" + time;
            else url += "&" + time;

            //http请求
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = command.method;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Proxy = null;
            //请求的内容    
            if (command.data != null)
            {
                Stream outStream = request.GetRequestStream();
                outStream.Write(command.data.toArray(), 0, command.data.length);
                outStream.Close();
            }
            //返回数据
            ByteBuffer bb = new ByteBuffer();
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            Stream inStream = response.GetResponseStream();
            for (int v = inStream.ReadByte(); v != -1; v = inStream.ReadByte())
            {
                bb.writeByte(v);
            }
            inStream.Close();
            response.Close();
            entry.value = bb;
        }
        /// <summary>
        /// 返回数据执行
        /// </summary>
        void excute()
        {
            HttpEntry entry = null;
            for (int i = 0; i < this.entries.count; i++)
            {
                if (this.entries.get(i).value == null) continue;
                entry = this.entries.removeAt(i);
                entry.command.onCommand(entry.value);
                break;
            }
            if (entry == null) return;
        }
    }
}