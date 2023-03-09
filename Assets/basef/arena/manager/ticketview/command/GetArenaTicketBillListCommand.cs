using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 获取门票管理列表
    /// </summary>
    public class GetArenaTicketBillListCommand : UserCommand
    {
        private long starttime;

        private long endtime;

        private long uuid;

        public GetArenaTicketBillListCommand(long starttime, long endtime, long uuid)
        {
            id = CommandConst.PORT_ARENA_TICKET_BILL_LIST;
            this.starttime = starttime;
            this.endtime = endtime;
            this.uuid = uuid;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(Arena.arena.getId());
            data.writeLong(starttime);
            data.writeLong(endtime);
            data.writeLong(uuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            TicketBill[] bill = new TicketBill[len];
            for (int i = 0; i < len; i++)
            {
                bill[i] = new TicketBill();
                bill[i].bytesRead(data);
            }

            TicketTimeSoltTotal total = null;
            if (uuid == 0)
            {
                total=new TicketTimeSoltTotal();
                total.bytesRead(data);
            }
            object[] ticketdata=new object[2]{ bill ,total};

            callbackobj = ticketdata;
        }
    }
}
