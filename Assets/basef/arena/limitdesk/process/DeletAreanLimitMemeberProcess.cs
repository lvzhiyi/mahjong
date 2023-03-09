using cambrian.common;

namespace basef.arena
{
    public class DeletAreanLimitMemeberProcess:MouseClickProcess
    {
        private ArenaMutex mutex;
        public override void execute()
        {
            ArenaLimitBar bar= transform.parent.GetComponent<ArenaLimitBar>();
            mutex = bar.mutex;
            CommandManager.addCommand(new ArenaDeleteMutexCommand(Arena.arena.getId(),mutex.id),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ArenaMutex[] mutexs = (ArenaMutex[]) obj;
            ArenaLimitDeskPanel panel = getRoot<ArenaLimitDeskPanel>();
            panel.setData(mutexs);
            panel.refresh();
        }
    }
}
