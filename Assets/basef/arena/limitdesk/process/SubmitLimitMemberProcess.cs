using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 提交限制组，到服务器
    /// </summary>
    public class SubmitLimitMemberProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaAddLimitDeskPanel panel = getRoot<ArenaAddLimitDeskPanel>();
            ArrayList<ArenaMutexMember> list = panel.rightmembers;
            long[] userid = new long[list.count];
            for (int i = 0; i < list.count; i++)
            {
                userid[i] = list.get(i).userid;
            }

            if (!panel.isUpdate)
                CommandManager.addCommand(new ArenaAddMutexCommand(Arena.arena.getId(), userid), back);
            else
            {
                CommandManager.addCommand(new ArenaUpdateAMutexCommand(panel.limitid, Arena.arena.getId(), userid),
                    back);
            }
        }

        public void back(object obj)
        {
            if (obj == null) return;

            ArenaMutex[] mutexs = (ArenaMutex[]) obj;

            ArenaLimitDeskPanel panel = UnrealRoot.root.getDisplayObject<ArenaLimitDeskPanel>();
            panel.setData(mutexs);
            panel.refresh();
            panel.setVisible(true);

            Alert.autoShow("设置成功");
            this.root.setVisible(false);
        }
    }
}
