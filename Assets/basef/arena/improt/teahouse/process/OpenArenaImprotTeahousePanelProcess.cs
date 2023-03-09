using cambrian.common;

namespace basef.arena
{
    /// <summary> 打开导入茶馆面板 </summary>
    public class OpenArenaImprotTeahousePanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaImprotListCommand(),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            ArenaImprotTeahousePanel panel = UnrealRoot.root.getDisplayObject<ArenaImprotTeahousePanel>();
            panel.setData((ArrayList<ArenaImprotTeahouseData>)obj);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}