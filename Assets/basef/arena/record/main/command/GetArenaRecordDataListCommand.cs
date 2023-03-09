using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取赛场战绩 数据
    /// </summary>
    public class GetArenaRecordDataListCommand : UserCommand
    {
        /// <summary> 最小ID</summary>
        long minUid;
        /// <summary> 时间 </summary>
        long time;
        /// <summary> 竞赛场ID </summary>
        long arenaid;
        /// <summary> 锁定规则id  0表示查看全部</summary>
        int rule;

        public GetArenaRecordDataListCommand(long arenaid,long time,int rule,long minUid)
        {
            this.id = CommandConst.PORT_ARENA_GET_COMPETITION_RECORD;
            this.minUid = minUid;
            this.time = time;
            this.arenaid = arenaid;
            this.rule = rule;
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt(); //长度
            ArrayList<ArenaRecordContentData> list = new ArrayList<ArenaRecordContentData>(len);
            ArenaRecordContentData value;
            for (int i = 0; i < len; i++)
            {
                value = new ArenaRecordContentData();
                value.bytesRead(data);
                list.add(value);
            }
            callbackobj = list;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(time);
            data.writeInt(rule);
            data.writeLong(minUid);
        }
    }
}
