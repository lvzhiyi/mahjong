using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    public class GetArenaAccountsMedalListCommand : UserCommand
    {
        /// <summary> 类型：兑换奖章 </summary>
        public const int TYPE_INCR_MEDAL = 101;
        /// <summary> 类型：兑换物品 </summary>
        public const int TYPE_DECR_MEDAL = 201;
        /// <summary> 类型：全部（前台使用）</summary>
        public const int TYPE_ALL = 0;


        /// <summary> 获取奖章明细最后一条ID </summary>
        long uuid = 0;

        /// <summary> 查询类型 </summary>
        int type;

        /// <summary> 玩家ID </summary>
        long userid;

        public GetArenaAccountsMedalListCommand(long userid,int type,long uuid = 0)
        {
            this.id = CommandConst.PORT_GET_MEMBER_MEDAL_RECORD;
            this.uuid = uuid;
            this.type = type;
            this.userid = userid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            //休闲场
            data.writeLong(Arena.arena.getId());
            //玩家ID
            data.writeLong(userid);
            //查询类型   
            data.writeInt(type);
            //uuid 默认 0
            data.writeLong(uuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaMedalBill> list = new ArrayList<ArenaMedalBill>(len);
            ArenaMedalBill value;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaMedalBill();
                value.bytesRead(data);
                list.add(value);
            }
            callbackobj = list;
        }
    }
}
