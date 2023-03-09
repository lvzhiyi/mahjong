using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取限制组
    /// </summary>
    public class GetArenaMutexCommand:UserCommand
    {
        private long arenaid;

        public GetArenaMutexCommand(long arenaid)
        {
            id = CommandConst.PORT_ARENA_MUTEX_GET;
            this.arenaid = arenaid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();

            ArenaMutex[] list=new ArenaMutex[len];

            for (int i = 0; i < len; i++)
            {
                list[i]= new ArenaMutex();
                list[i].bytesRead(data);
            }

            callbackobj = list;
        }
    }
}
