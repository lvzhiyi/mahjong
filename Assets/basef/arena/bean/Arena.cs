using cambrian.common;

namespace basef.arena
{
    public class Arena : BytesSerializable
    {
        /// <summary>
        /// 金豆，推广任务，显示比例(后台放大100倍，前端相应要放大100，后者缩小100)
        /// </summary>
        public static int PROPORTION = 100;

        public static Arena arena { get; set; }

        /// <summary> 竞赛场状态：打烊 </summary>
        public const int STATE_SUSPEND = 1;

        /// <summary> 竞赛场状态：已解散 </summary>
        public const int STATE_DISBAND = 1 << 1;

        /// <summary> 显示排行榜 </summary>
        public const int STATE_NO_SHOW_RANK = 1 << 2;

        /// <summary> 唯一ID </summary>
        long id;

        /// <summary> 创建者 </summary>
        long master;

        /// <summary> 竞赛场名称 </summary>
        public string name;

        /// <summary> 竞技场图标 </summary>
        string icon;

        /// <summary> 累计房卡消耗 </summary>
        long total_spend;

        /// <summary> 累计房卡建房 </summary>
        long total_rooms;

        /// <summary> 竞赛场公告 </summary>
        string notice;

        /// <summary> 擂主最大可增加推广任务 </summary>
        long maxIncrTask;
        /// <summary> 擂主已增加推广任务 </summary>
        long incrCount;

        /// <summary> 活动时间 </summary>
        long activeTime;

        /// <summary> 创建时间 </summary>
        long createTime;

        /// <summary> 创建者昵称 </summary>
        public string masterName;

        /// <summary> 成员人数 </summary>
        public int members;

        /// <summary> 成员人数上限 </summary>
        public int maxMembers;

        /// <summary>
        /// 状态（打烊，解散，）
        /// </summary>
        public int status { get; set; }

        /// <summary> 兑换设置 </summary>
        public ArenaExchangeSettings exchangeSettings;

        /// <summary> 房卡场设置 </summary>
        public ArenaFKCSettings fkcSettings;

        /// <summary> 竞赛场成员 </summary>
        ArenaMember member;

        public int maxLeve;

        /// <summary>
        /// 是否有新消息
        /// </summary>
        public bool isNewMsg;



        public Arena()
        {
            exchangeSettings = new ArenaExchangeSettings();
            fkcSettings = new ArenaFKCSettings();
            member = new ArenaMember();
        }

        public long getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getIcon()
        {
            return this.icon;
        }

        public long getMaster()
        {
            return this.master;
        }

        public bool isMaster(long userid)
        {
            return master==userid;
        }

        public long getCreateTime()
        {
            return this.createTime;
        }

        public long getActiveTime()
        {
            return this.activeTime;
        }

        public void setActiveTime(long time)
        {
            this.activeTime = time;
        }

        public string getNotice()
        {
            return this.notice;
        }

        public bool setNotice(string notice)
        {
            this.notice = notice;
            return true;
        }

        /// <summary>
        /// 是否有多级
        /// </summary>
        /// <returns></returns>
        public bool isMultilevel()
        {
            return maxLeve != 0;
        }

        public bool isLastAgent()
        {
            return member.isAgent() && member.level == maxLeve;
        }

        /// <summary> 修改竞赛场名称 </summary>
        public void updateName(string name)
        {
            this.name = name;
        }

        public ArenaMember getMember()
        {
            return member;
        }

        public bool hasStatus(int value)
        {
            return StatusKit.hasStatus(status, value);
        }



        public override void bytesRead(ByteBuffer data)
        {
            id = data.readLong();
            name = data.readUTF();
            icon = data.readUTF();
            master = data.readLong();
            masterName = data.readUTF();
            maxLeve = data.readInt();
            status = data.readInt();
            total_spend = data.readLong();
            total_rooms = data.readLong();
            members = data.readInt(); // memberSize
            maxMembers = data.readInt(); // maxMemberSize
            notice = data.readUTF();
            maxIncrTask = data.readLong();
            incrCount = data.readLong();

            fkcSettings.bytesRead(data);
            exchangeSettings.bytesRead(data);
           
        }
    }
}
