using cambrian.common;
using cambrian.game;

namespace basef.player
{
    public class Player
    {

        /** 玩家状态：新玩家（未登陆过） */
        public const int STATUS_NEW = 1;
        /** 玩家状态：活跃玩家 */
        public const int STATUS_ACTIVE = 2;
        /** 玩家状态：回归玩家 */
        public const int STATUS_BACK = 4;

        /** 玩家状态：已完成首次充值 */
        public const int STATUS_FIRST_RECHARGE = 8;
        /// <summary>
        /// 实名认证奖励
        /// </summary>
        public const int STATUS_AUTH_AWARD = 16;
        /// <summary>
        /// 绑定代理
        /// </summary>
        public const int STATUS_BIND_PROXY = 32;

        /// <summary> 日志 </summary>
        public static cambrian.common.Logger log = LogFactory.getLogger<Player>();

        /// <summary>
        /// 每日兑换的次数
        /// </summary>
        public static int DAY_EXCHANGE_COUNT = 1000;


        /// <summary> 本地玩家 </summary>
        public static Player player = new Player();

        /* static fields */

        /* static methods */

        /* fields */
        /// <summary>
        /// id </summary>
        public long userid;
        /// <summary>
        /// 名字 </summary>
        public string name;
        /// <summary>
        /// 用户头像 </summary>
        public string head;

        /// <summary>
        /// 经验 </summary>
        public long expr;
        /// <summary>
        /// 等级 </summary>
        public int medal;

        /// <summary>
        /// 游戏币 </summary>
        public long money;

        /// <summary>
        /// 钻石
        /// </summary>
        public long diamond;

        /// <summary>
        /// 奖卷
        /// </summary>
        public int gift;
        /// <summary>
        /// 创建时间 </summary>
        public long createtime;
        /// <summary>
        /// 创建日期 </summary>
        public long createday;
        /// <summary>
        /// 上次活动时间 </summary>
        public long activetime;
        /// <summary>
        /// 性别
        /// </summary>
        public int sex;
        /** 状态备用 */
        public int status;
        /** 是否领取初始游戏币:0未领取，1已领取 */
        public int isTakeInitMoney;
        /// <summary>
        /// 是否有绑定消息
        /// </summary>
        public bool isAddBind;


        /* constructors */
        public Player()
        {
        }

        public bool isSelf(long userid)
        {
            return this.userid == userid;
        }

        public bool hasStatus(int status)
        {
            return (this.status & status)== status;
        }

        public virtual void bytesRead(ByteBuffer data)
        {
            this.userid = data.readLong();
            this.name = data.readUTF();
            this.head = data.readUTF();
            this.sex = data.readInt();
            this.status = data.readInt();
            this.gift = data.readInt();
            this.money = data.readLong();
            this.diamond = data.readLong();
            this.medal = data.readInt();
            this.expr = data.readLong();
            this.createtime = data.readLong();
            this.activetime = data.readLong();
            this.isTakeInitMoney = data.readInt();
            this.isAddBind = data.readBoolean();
            this.head = User.user.head;

            if (this.head == null || this.head.Length < 2) return;
            if (this.head[this.head.Length - 1] == '0')
            {
                if (this.head[this.head.Length - 2] == '/')
                {
                    CharBuffer buf = new CharBuffer();
                    buf.append(this.head, 0, this.head.Length - 2);
                    buf.append("/64");
                    this.head = buf.getString();
                }
            }

            this.head += "?" + MathKit.random(1, 10000);
        }

        /// <summary>
        /// 获取简要信息对象
        /// </summary>
        public SimplePlayer getSimplePlayer()
        {
            SimplePlayer player=new SimplePlayer();

            player.userid = userid;
            player.name = this.name;
            player.head = this.head;
            player.sex = this.sex;
            player.setStatus(SimplePlayer.STATUS_ONLINE);
            player.host = "";
            return player;
        }

        public override string ToString()
        {
            return base.ToString() + "Player [userid=" + userid + ", name=" + name + ", head=" + head + ", expr=" + expr + ", medal=" + medal + ", money=" + money  + ", createtime=" + createtime + ", createday=" + createday + ", activetime=" + activetime + "]";
        }
    }
}
