using cambrian.common;
using cambrian.game;
using scene.game;

namespace basef.arena
{
    /// <summary>
    /// 创建规则，删除规则
    /// </summary>
    public class ArenaCreateAndDeleteCommand:UserCommand
    {
        long arenaid;
        private bool b;//true：增加，false:删除
        private ArenaLockRule rule;
        public ArenaCreateAndDeleteCommand(long arenaid,bool b, ArenaLockRule rule)
        {
            id = CommandConst.PORT_ARENA_SETTING_RULE_LOCK;
            this.arenaid = arenaid;
            this.b = b;
            this.rule = rule;
        }

        public override void bytesWrite(ByteBuffer data)
        {
            data.writeLong(arenaid);
            data.writeBoolean(b);
            if(b)
                rule.bytesWrite(data);
            else
                data.writeInt(rule.uuid);
        }

        public override void bytesRead(ByteBuffer data)
        {
            int len = data.readInt();
            ArrayList<ArenaLockRule> list=new ArrayList<ArenaLockRule>(len);
            for (int i = 0; i < len; i++)
            {
                ArenaLockRule rule=new ArenaLockRule();
                rule.bytesRead(data);
                list.add(rule);
            }
            bool freedom = data.readBoolean();
            len = data.readInt();
            int[] showRules = new int[len];
            for(int i=0;i<len;i++)
            {
                showRules[i] = data.readInt();
            }
            Arena.arena.fkcSettings.setRues(list);
            Arena.arena.fkcSettings.freedomEnable = freedom;
            Arena.arena.fkcSettings.showRules = showRules;
            callbackobj = list;
        }
        
    }
}
