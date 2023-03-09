using cambrian.common;

namespace basef.arena
{
    public class RebateList:BytesSerializable
    {
        /** 特殊设置(是否是独立设置) */
        public bool special;
        /** 返利列表 */
        public ArrayList<RuleRebate> list;

        public RebateList()
        {
            list=new ArrayList<RuleRebate>();
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.special = data.readBoolean();
            this.list.clear();
            int len = data.readInt();
            RuleRebate rebate;
            for (int i = 0; i < len; i++)
            {
                rebate = new RuleRebate();
                rebate.bytesRead(data);
                list.add(rebate);
            }
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeBoolean(special);
            data.writeInt(list.count);
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).bytesWrite(data);
            }
        }

    }
}
