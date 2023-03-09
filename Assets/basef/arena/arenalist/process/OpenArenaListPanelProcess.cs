using basef.player;
using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 打开茶馆界面
    /// </summary>
    public class OpenArenaListPanelProcess : MouseClickProcess
    {
        public override void execute()
        {
            CommandManager.addCommand(new GetArenaListCommand(), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            //TODO 后续修改
            ArrayList<Arena> list = (ArrayList<Arena>)obj;

            ArrayList<Arena> createList = new ArrayList<Arena>();
            ArrayList<Arena> joinList = new ArrayList<Arena>();

            Arena temp = null;
            for (int i = 0; i < list.count; i++)
            {
                temp = list.get(i);
                if (temp.getMaster() == Player.player.userid)
                    createList.add(temp);
                else
                    joinList.add(temp);
            }

            ArenaListPanel panel = UnrealRoot.root.getDisplayObject<ArenaListPanel>();
            panel.setData(createList, joinList);
            panel.refresh();
            panel.setVisible(true);
            this.root.setVisible(false);
            panel.setLastPanel(this.root);
        }
    }
}
