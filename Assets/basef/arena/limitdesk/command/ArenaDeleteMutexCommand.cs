using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 减少限制组
    /// </summary>
    public class ArenaDeleteMutexCommand:UserCommand
    {
        private long arenaid;

        private long mutexid;

        public ArenaDeleteMutexCommand(long arenaid,long mutexid)
        {
            id = CommandConst.PORT_ARENA_MUTEX_DEL;
            this.arenaid = arenaid;
            this.mutexid = mutexid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(mutexid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArenaMutex[] mutexs = new ArenaMutex[len];
            for (int i = 0; i < mutexs.Length; i++)
            {
                mutexs[i]=new ArenaMutex();
                mutexs[i].bytesRead(data);
            }

            callbackobj = mutexs;
        }
    }
}
