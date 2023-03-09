namespace cambrian.common
{
    /// <summary>
    /// 样本
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        protected static Logger log = LogFactory.getLogger<Sample>(false);

        /** 样本工厂 */
        public static SampleFactory factory = new SampleFactory();

        /* static methods */

        /* fields */
        /** 类型标示符 */
        public int sid;

        /* constructors */

        public Sample()
        {
        }

        /* properties */

        /* init start */
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void init()
        {
            //throw new SystemException("override sub class init,"+this);
        }

        /// <summary>
        /// 检查格式，不对则抛异常
        /// </summary>
        public virtual void check()
        {
            //throw new SystemException("override sub class check," + this);
        }
        /// <summary>
        /// 相关联
        /// </summary>
        public virtual void relate()
        {

        }

        /* methods */
        /** 获得sid */

        public int getSid()
        {
            return this.sid;
        }

        /* common methods */
        /** 序列化 */

        public virtual void bytesWrite(ByteBuffer data)
        {
            data.writeInt(this.sid);
        }

        /** 反序列化 */

        public virtual void bytesRead(ByteBuffer data)
        {
            //这里不读SID

        }

        /** 克隆 */

        public virtual Sample clone()
        {
            //("no override function:clone");
            return (Sample) this.MemberwiseClone();
        }

        public override string ToString()
        {
            return base.ToString() + ",sid=" + this.sid;
        }
    }
}