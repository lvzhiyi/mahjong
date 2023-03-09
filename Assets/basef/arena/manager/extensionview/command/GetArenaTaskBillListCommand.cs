using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 获取任务明细
    /// </summary>
    public class GetArenaTaskBillListCommand : UserCommand
    {
        private long arenaid;

        private int type ;

        private long lastuuid;

        private long dest;
        
        public GetArenaTaskBillListCommand(long arenaid,int type,long lastuuid,long dest)
        {
            id = CommandConst.PORT_ARENA_TASKE_BILL_LIST_GET;
            this.arenaid = arenaid;
            this.type = type;
            this.lastuuid = lastuuid;
            this.dest = dest;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeInt(type);
            data.writeLong(dest);
            data.writeLong(lastuuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();

            TaskBill[] bill=new TaskBill[len];
            for (int i = 0; i < len; i++)
            {
                bill[i]=new TaskBill();
                bill[i].bytesRead(data);
            }
            callbackobj = bill;
        }
    }
}
