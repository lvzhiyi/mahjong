using cambrian.common;

namespace basef.player
{
    public class PlayerToken
    {
        /* static fields */

        /// <summary> 状态：信息完善的代理 /// </summary>
        public const int STATUS_COMPLETED = 1 << 0;// 1
        /// <summary> 状态：根代理：所有一代的上级 /// </summary>
        public const int STATUS_ROOT = 1 << 1;// 2
        /// <summary> 状态：超级代理，一代 /// </summary>
        public const int STATUS_SUPER = 1 << 2;// 4
        /// <summary> 状态：已被撤销代理身份 /// </summary>
        public const int STATUS_REVOKE = 1 << 3;// 8
        /// <summary> 状态：已经被冻结（只能查询，不能变更数据） /// </summary>
        public const int STATUS_FROZEN = 1 << 4;// 16
        /// <summary> 状态：片区代理，一代 /// </summary>
        public const int STATUS_ZONE = 1 << 5;// 32

        public static PlayerToken instance = new PlayerToken();

        /* static methods */

        /* fields */
        /// <summary>
        /// 用户id </summary>
        public long id;
        /// <summary>
        /// 通过充值渠道的总充值数 </summary>
        public long tokens;
        /// <summary>
        /// 代币 </summary>
        public long token;

        /// <summary>
        /// 直接游戏推荐人 </summary>
        public long master;

        /// <summary>
        /// 状态
        /// </summary>
        public int status;

        /* constructors */

        /* properties */

        /* init start */

        /* methods */

        /* common methods */


        /** 是否有指定状态（包含指定状态，但不限于指定状态） */
        public bool hasStatus(int status)
        {
            return (this.status & status) == status;
        }

        /** 是否是代理 */
        public bool isPromoter()
        {
            if (status == 0) return false;
            return true;
        }

        public void bytesRead(ByteBuffer data)
        {
            this.id = data.readLong();
            this.master = data.readLong();
            this.token = data.readLong();
            this.tokens = data.readLong();
            this.status = data.readInt();

        }
        public override string ToString()
        {
            return "Player [id=" + this.id + ", token=" + this.token + ", tokens=" + this.tokens+ "]";
        }
    }
}
