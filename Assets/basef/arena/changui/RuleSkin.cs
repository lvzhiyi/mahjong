using cambrian.common;

namespace basef.arena
{
    public class RuleSkin
    {

        ArrayList<int> rules;

        ArrayList<int> deskbgs;

        public RuleSkin()
        {
            rules = new ArrayList<int>();
            deskbgs = new ArrayList<int>();
        }

        public int getDeskBg(int ruleid,int platform)
        {
            for (int i = 0; i < rules.count; i++)
            {
                if (ruleid == rules.get(i))
                {
                    return deskbgs.get(i);
                }
            }
            return platform;
        }

        public void setDeskBg(int ruleid,int deskbg)
        {
            int index = -1;
            for (int i = 0; i < rules.count; i++)
            {
                if (ruleid == rules.get(i))
                {
                    index = i;
                    break;
                }
            }
            if(index==-1)
            {
                rules.add(ruleid);
                deskbgs.add(deskbg);
            }
            else
            {
                deskbgs.set(deskbg, index);
            }
        }

        public void update(ArrayList<ArenaLockRule> lockRules)
        {
            ArrayList<int> temp1 = new ArrayList<int>();
            ArrayList<int> temp2 = new ArrayList<int>();

            ArenaLockRule rule;
            int deskbg = 0;
            for (int i = 0; i < lockRules.count; i++)
            {
                rule = lockRules.get(i);
                deskbg = rule.rule.getPlatFormValue();
                temp1.add(rule.uuid);
                for (int j = 0; j < rules.count; j++)
                {
                    if (rule.uuid == rules.get(j))
                    {
                        deskbg = deskbgs.get(j);
                        break;
                    }
                }
                temp2.add(deskbg);
            }
            rules = temp1;
            deskbgs = temp2;
        }


        public void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            int ruleid, deskbg;
            for (int i = 0; i < len; i++)
            {
                ruleid = data.readInt();
                deskbg = data.readInt();
                rules.add(ruleid);
                deskbgs.add(deskbg);
            }
        }

        public void bytesWrite(ByteBuffer data)
        {
            data.writeInt(rules.count);
            for (int i = 0; i < rules.count; i++)
            {
                data.writeInt(rules.get(i));
                data.writeInt(deskbgs.get(i));
            }
        }
    }
}
