using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 提交增加合伙人积分
    /// </summary>
    public class SubmitEditAgentScoreProcess : MouseClickProcess
    {
        public bool isIncr;

        private ArenaExtensionChangePanel panel;
        public override void execute()
        {
            panel = getRoot<ArenaExtensionChangePanel>();
            string text = panel.inputText.text;
            if (text == null || text.Length == 0) return;
            long task = StringKit.parseLong(text);

            if (!isIncr) task = -task;
            CommandManager.addCommand(new ArenaIncrTaskCommand(Arena.arena.getId(), task, panel.dest), back);
            this.root.setVisible(false);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            ByteBuffer data = (ByteBuffer)obj;
            long money = data.readLong();
            long total = data.readLong();
            if (panel.callback != null) panel.callback(money, total);
        }
    }
}
