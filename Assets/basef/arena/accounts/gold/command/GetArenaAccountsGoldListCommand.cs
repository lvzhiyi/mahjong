using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary> 获取玩家金豆明细列表 </summary>
    public class GetArenaAccountsGoldListCommand : UserCommand
    {
        /// <summary> 获取玩家金豆明细最后一条ID </summary>
        long uuid = 0;

        /// <summary> 查询类型 </summary>
        int type;

        /// <summary> 是否金豆明细不足 </summary>
        bool isNegativeNumber;

        private long userid;

        public GetArenaAccountsGoldListCommand(long userid,int type,bool isNegativeNumber = false,long uuid = 0)
        {
            this.id = CommandConst.PORT_GET_MEMBER_ARENAGOLD_RECORD;
            this.uuid = uuid;
            this.type = type;
            this.userid = userid;
            this.isNegativeNumber = isNegativeNumber;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            //休闲场ID   
            data.writeLong(Arena.arena.getId());
            //玩家ID
            data.writeLong(userid);
            //是否查询金豆流水不足
            data.writeBoolean(isNegativeNumber);
            //查询类型   
            data.writeInt(type);
            //uuid 默认 0
            data.writeLong(uuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaAccountsGoldContentData> list = new ArrayList<ArenaAccountsGoldContentData>(len);
            ArenaAccountsGoldContentData value;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaAccountsGoldContentData();
                value.bytesRead(data);
                list.add(value);
            }
            value = null;
            callbackobj = list;
        }
    }
}
