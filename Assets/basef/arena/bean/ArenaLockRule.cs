using basef.rule;
using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 锁定规则
    /// </summary>
    public class ArenaLockRule : BytesSerializable
    {
        /** 门票收入类型：0按分数阶梯收取，1按比例收取，2AA支付门票 */
        public const int TYPE_STEP = 0, TYPE_RATIO = 1, TYPE_AA = 2;


        ///<summary> 玩法名称 </summary>
        public string name;
        ///<summary> 规则 </summary>
        public Rule rule;
        ///<summary> 入场最低金豆数 </summary>
        long limitgold;

        ///<summary> 解散房间阈值:低于此值解散房间 </summary>
        int disbandThreshold;

        ///<summary> 可透支金豆：-1有多少扣多少，0不可透支，正数可透支的数 </summary>
        int overdraft;

        ///<summary> 门票收取方式类型 </summary>
        public int mpType;
        ///<summary> 门票返利阈值：低于此值不返给下级推广员 </summary>
        int mpThreshold;

        ///<summary> 门票收入等级 </summary>
        public ArrayList<TicketLevel> list;

        public int uuid;


        public ArenaLockRule()
        {
            limitgold = 12;
            disbandThreshold = 12;
            mpThreshold = 0;
            overdraft = -1;
            list = new ArrayList<TicketLevel>();
        }

        /// <summary>
        /// 克隆大赢家支付
        /// </summary>
        /// <returns></returns>
        public ArrayList<TicketLevel> cloneCurList()
        {
            ArrayList<TicketLevel> tickets = new ArrayList<TicketLevel>();
            TicketLevel level = null;
            ByteBuffer data = new ByteBuffer();
            if (mpType == TYPE_STEP)
                for (int i = 0; i < list.count; i++)
                {
                    level = new TicketLevel();
                    list.get(i).bytesWrite(data);
                    level.bytesRead(data);
                    tickets.add(level);
                    data.clear();
                }

            if (tickets.count == 0)
            {
                tickets.add(new TicketLevel());
            }

            return tickets;
        }


        public long getLimitGold()
        {
            return limitgold / Arena.PROPORTION;
        }

        public void setLimitGold(long limitgold)
        {
            this.limitgold = limitgold * Arena.PROPORTION;
        }

        public int getDisbandThreshold()
        {
            return disbandThreshold / Arena.PROPORTION;
        }

        public void setDisbandThreshod(int disbandThreshold)
        {
            this.disbandThreshold = disbandThreshold * Arena.PROPORTION;
        }

        public int getThReshold()
        {
            return mpThreshold / Arena.PROPORTION;
        }

        public void setThreshold(int mpThreshold)
        {
            this.mpThreshold = mpThreshold * Arena.PROPORTION;
        }

        public int getOverDraft()
        {
            if (overdraft == -1) return overdraft;

            return overdraft / Arena.PROPORTION;
        }

        public void setOverDraft(int overdraft)
        {
            if (overdraft == -1) this.overdraft = -1;
            else
                this.overdraft = overdraft * Arena.PROPORTION;
        }

        /// <summary>
        /// 增加门票等级
        /// </summary>
        public void addTicketLevel()
        {
            TicketLevel ticketLevel = new TicketLevel();

            long minScore = 0;

            long maxScore = TicketLevel.NO_UPPER_LIMIT;

            long ticket = TicketLevel.COLLECT_TICKET_STEP;
            if (list.count > 0)
            {
                TicketLevel lastTicket = list.getLast();
                if (lastTicket.maxScore == TicketLevel.NO_UPPER_LIMIT)
                {
                    minScore = lastTicket.minScore + TicketLevel.GOLD_STEP;
                    ticket = lastTicket.value + TicketLevel.COLLECT_TICKET_STEP;
                    lastTicket.maxScore = minScore;
                }
            }
            ticketLevel.setData(minScore, maxScore, ticket, 0);
            list.add(ticketLevel);
        }

        public bool updateTicketLevel(int index, long minScroe, long maxScroe, long ticket, long guding)
        {
            if (list.count - 1 < index) return false;
            TicketLevel ticketLevel = list.get(index);
            ticketLevel.setData(minScroe, maxScroe, ticket, guding);
            TicketLevel curticketLevel = null;
            for (int i = index + 1; i < list.count; i++)
            {
                curticketLevel = list.get(i);
                if (i != list.count - 1)
                {
                    if (curticketLevel.maxScore <= ticketLevel.maxScore)
                        curticketLevel.setDatas(ticketLevel.maxScore, ticketLevel.maxScore + TicketLevel.GOLD_STEP);
                    else
                        curticketLevel.setDatas(ticketLevel.maxScore, curticketLevel.maxScore);
                }
                else
                    curticketLevel.setDatas(ticketLevel.maxScore, TicketLevel.NO_UPPER_LIMIT);

                ticketLevel = curticketLevel;
            }

            return true;
        }

        public bool delteTicketLevel(int index)
        {
            if (list.count - 1 < index || list.count == 1) return false;
            list.removeAt(index);

            TicketLevel curticketLevel = null;
            TicketLevel lastTicketLevel = null;
            for (int i = 0; i < list.count; i++)
            {
                curticketLevel = list.get(i);
                if (i == 0 && list.count == 1)
                {
                    curticketLevel.minScore = 0;
                    curticketLevel.maxScore = -1;
                }
                else if (i == 0)
                {
                    curticketLevel.minScore = 0;

                }
                else if (i == list.count - 1)
                {
                    curticketLevel.setDatas(lastTicketLevel.maxScore, TicketLevel.NO_UPPER_LIMIT);
                }
                else
                {
                    curticketLevel.setDatas(lastTicketLevel.maxScore, lastTicketLevel.maxScore + TicketLevel.GOLD_STEP);
                }

                lastTicketLevel = curticketLevel;
            }

            return true;
        }

        public override void bytesRead(ByteBuffer data)
        {
            this.uuid = data.readInt();
            this.name = data.readUTF();
            this.rule = RuleManager.manager.newSample(data.readInt());
            rule.bytesRead(data);
            this.rule.setBet(this.rule.getBet());

            this.limitgold = data.readLong();
            this.disbandThreshold = data.readInt();
            this.overdraft = data.readInt();
            this.mpType = data.readInt();
            this.mpThreshold = data.readInt();
            int len = data.readInt();
            TicketLevel level;
            for (int i = 0; i < len; i++)
            {
                level = new TicketLevel();
                level.bytesRead(data);
                list.add(level);
            }
        }

        public void bytesReads(ByteBuffer data)
        {
            uuid = data.readInt();
            this.name = data.readUTF();
            this.rule = RuleManager.manager.newSample(data.readInt());
            rule.bytesReadLocal(data);
            int bet = data.readInt();
            this.rule.setBet(bet);

            this.limitgold = data.readLong();
            this.disbandThreshold = data.readInt();
            this.overdraft = data.readInt();

            this.mpType = data.readInt();
            this.mpThreshold = data.readInt();
            int len = data.readInt();
            TicketLevel level;
            for (int i = 0; i < len; i++)
            {
                level = new TicketLevel();
                level.bytesRead(data);
                list.add(level);
            }

            if (len == 0)
            {
                list.add(new TicketLevel(0, 0, 0, 0));
                list.add(new TicketLevel());
            }
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeInt(uuid);
            data.writeUTF(name);
            rule.bytesWrite(data);
            data.writeInt(rule.getBet());
            data.writeLong(limitgold);
            data.writeInt(disbandThreshold);
            data.writeInt(overdraft);
            data.writeInt(mpType);
            data.writeInt(mpThreshold);
            data.writeInt(list.count);
            for (int i = 0; i < list.count; i++)
            {
                list.get(i).bytesWrite(data);
            }
        }

        public ArenaLockRule clone()
        {
            ArenaLockRule rule = new ArenaLockRule();
            ByteBuffer data = new ByteBuffer();
            bytesWrite(data);
            rule.bytesReads(data);
            return rule;
        }
    }
}
