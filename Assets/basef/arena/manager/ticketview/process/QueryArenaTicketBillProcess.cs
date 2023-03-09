using cambrian.common;

namespace basef.arena
{
    public class QueryArenaTicketBillProcess:MouseClickProcess
    {
        private ArenaTicketManagerView view;
        public override void execute()
        {
            view= transform.parent.GetComponent<ArenaTicketManagerView>();
            if (view._starttime == 0 || view._endtime == 0)
            {
                return;
            }
            CommandManager.addCommand(new GetArenaTicketBillListCommand(view._starttime,view._endtime,0), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            object[] objs = (object[]) obj;
            TicketBill[] bill = (TicketBill[])objs[0];
            view.setData(bill,view._starttime,view._endtime,(TicketTimeSoltTotal)objs[1]);
        }
    }
}
