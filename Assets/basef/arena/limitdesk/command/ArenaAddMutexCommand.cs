using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 增加限制组
    /// </summary>
    public class ArenaAddMutexCommand : UserCommand
    {
        private long areanid;

        private long[] userids;

        public ArenaAddMutexCommand(long areanid, long[] userids)
        {
            id = CommandConst.PORT_ARENA_MUTEX_ADD;
            this.areanid = areanid;
            this.userids = userids;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(areanid);
            data.writeInt(userids.Length);
            for (int i = 0; i < userids.Length; i++)
            {
                data.writeLong(userids[i]);
            }
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();

            ArenaMutex[] mutexs = new ArenaMutex[len];

            for (int i = 0; i < len; i++)
            {
                mutexs[i] = new ArenaMutex();
                mutexs[i].bytesRead(data);
            }

            callbackobj = mutexs;
        }
    }
}
