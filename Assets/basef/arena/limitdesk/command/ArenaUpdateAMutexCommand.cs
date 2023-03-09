using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 更新限制组
    /// </summary>
    public class ArenaUpdateAMutexCommand:UserCommand
    {
        private long areanid;

        private long[] userids;

        private long limitid;
        public ArenaUpdateAMutexCommand(long limitid,long areanid, long[] userids)
        {
            id = CommandConst.PORT_ARENA_MUTEX_UPDATE;
            this.limitid = limitid;
            this.areanid = areanid;
            this.userids = userids;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(areanid);
            data.writeLong(limitid);
            data.writeInt(userids.Length);
            for (int i = 0; i < userids.Length; i++)
            {
                data.writeLong(userids[i]);
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();

            ArenaMutex[] list = new ArenaMutex[len];

            for (int i = 0; i < len; i++)
            {
                list[i] = new ArenaMutex();
                list[i].bytesRead(data);
            }

            callbackobj = list;
        }
    }
}
