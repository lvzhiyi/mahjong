using cambrian.common;

namespace platform.spotred
{
    public class StatusAble: BytesSerializable
    {
        /** 状态值 */
        protected int status;

        /** 强制设置为仅有指定状态 */
        public void setStatus(int status)
        {
            this.status = status;
        }

        public int getStatus()
        {
            return this.status;
        }

        /** 是否仅仅是指定状态：状态值等于此值 */
        public bool isStatus(int status)
        {
            return this.status == status;
        }

        /** 设置指定状态有效性 */
        public void setStatus(int status, bool b)
        {
            if (b)
                this.status |= status;
            else
                this.status &= ~status;
        }

        /** 是否包含指定状态 */
        public bool hasStatus(int status)
        {
            return (this.status & status) == status;
        }
    }
}
