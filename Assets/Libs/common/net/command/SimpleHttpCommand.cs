
namespace cambrian.common
{
    /// <summary>
    /// 基本的HTTP通信命令基类
    /// </summary>
    public class SimpleHttpCommand : Port
    {
        /// <summary> 日志 </summary>
        protected static Logger log = LogFactory.getLogger<SimpleHttpCommand>(true);

        /// <summary> 访问的URL </summary>
        public string url;
        /// <summary> 提交方式 </summary>
        public string method;
        /// <summary> 数据 </summary>
        public ByteBuffer data;

        public SimpleHttpCommand(string url) : this(url, "GET")
        {
        }

        public SimpleHttpCommand(string url, string method)
        {
            this.url = url;
            this.method = method;
        }

        public override void excute()
        {
            HttpDataAccessHandler.getInstance().access(this);
        }

        public virtual void onCommand(ByteBuffer data)
        {
            this.callback(data);
        }
    }
}