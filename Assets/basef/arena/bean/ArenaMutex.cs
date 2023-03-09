using cambrian.common;

namespace basef.arena
{
    public class ArenaMutex:BytesSerializable
    {
        public long id;

        private ArrayList<ArenaMutexMember> list;

        public ArenaMutex()
        {
            list=new ArrayList<ArenaMutexMember>();
        }

        public ArrayList<ArenaMutexMember> getList()
        {
            return list;
        }

        public void addArenaMember(ArenaMutexMember arenaMember)
        {
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).userid == arenaMember.userid)
                    return;
            }

            list.add(arenaMember);
        }

        public void deletArenaMember(long userid)
        {
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).userid == userid)
                {
                    list.removeAt(i);
                    break;
                }
            }
        }


        public override void bytesRead(ByteBuffer data)
        {
            id = data.readLong();
            int len = data.readInt();
            ArenaMutexMember member = null;
            for (int i = 0; i < len; i++)
            {
                member = new ArenaMutexMember();
                member.bytesRead(data);
                list.add(member);
            }
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(id);
            data.writeInt(list.count);
            for (int i = 0; i < list.count; i++)
            {
                data.writeLong(list.get(i).userid);
            }
        }
    }
}
