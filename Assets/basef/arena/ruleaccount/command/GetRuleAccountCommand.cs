using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 获取规则结算
    /// </summary>
    public class GetRuleAccountCommand : UserCommand
    {
        private long arenaid;

        /// <summary>
        /// 目标
        /// </summary>
        private long dest;

        public GetRuleAccountCommand(long arenaid, long dest)
        {
            id = CommandConst.PORT_ARENA_AGENT_ACCOUNT_LIST;
            this.arenaid = arenaid;
            this.dest = dest;
        }


        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeLong(dest);
        }

        public override void bytesRead(ByteBuffer data)
        {
            if (data.length == 0) return;
            int len = data.readInt();
            RuleAccount[] accounts;
            ArrayList<ArenaRuleAccountcs> list = new ArrayList<ArenaRuleAccountcs>();
            for (int i=0;i<len;i++)
            {
                int size = data.readInt();
                accounts=new RuleAccount[size];
                for (int j = 0; j < size; j++)
                {
                    accounts[j] = new RuleAccount();
                    accounts[j].bytesRead(data);
                    setArenaRuleAccount(list,accounts[j],i);
                }
            }

            callbackobj = list;
        }

        public void setArenaRuleAccount(ArrayList<ArenaRuleAccountcs> list, RuleAccount rule,int type)
        {
            ArenaRuleAccountcs account = null;
            for (int i = 0; i < list.count; i++)
            {
                if (list.get(i).rulelockid == rule.rule)
                    account = list.get(i);
            }

            if (account == null)
            {
                account = new ArenaRuleAccountcs(rule.rule, rule.rulename);
                list.add(account);
            }

            setData(account,rule,type);
        }

        public void setData(ArenaRuleAccountcs account, RuleAccount rule,int type)
        {
            switch (type)
            {
                case 0://今日
                    account.addTodayTicket(rule.ticket);
                    account.addTodayMatch(rule.match);
                    break;
                case 1://昨日
                    account.addYesterDayTicket(rule.ticket);
                    account.addYesterDayMatch(rule.match);
                    break;
                case 2://本周
                    account.addWeekTicket(rule.ticket);
                    account.addWeekMatch(rule.match);
                    break;
                case 3://上周
                    account.addLastWeekTicket(rule.ticket);
                    account.addLastWeekMatch(rule.match);
                    break;
            }
        }

    }
}
