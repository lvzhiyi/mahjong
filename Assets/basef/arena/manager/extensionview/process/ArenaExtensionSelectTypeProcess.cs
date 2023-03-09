using basef.player;
using cambrian.common;

namespace basef.arena
{
    public class ArenaExtensionSelectTypeProcess:MouseClickProcess
    {
        private ArenaExtensionView runview;

        public override void execute()
        {
            int index = GetComponent<ArenaRunTypesBar>().index_space;
            runview = transform.parent.parent.GetComponent<ArenaExtensionView>();

            runview.showTypes(index);

            

            if (index == 0)
            {
                runview.hideAllView();
                runview.selfExtensionView.refresh();
                runview.selfExtensionView.setVisible(true);
            }
            else if (index == 1)
            {
                CommandManager.addCommand(new GetArenaAgentDetailListCommand(), detailListback);
            }
            else if (index == 2)
            {
                CommandManager.addCommand(new GetArenaInfoCommand(Arena.arena.getId()), infoback);
            }
            else if (index == 3)
            {
                CommandManager.addCommand(new GetArenaMsgApplyContentListCommand(ArenaEvent.EVENT_TYPE_AGENT), msgBack);
            }
        }

        public void infoback(object obj)
        {
            if (obj == null)
            {
                Alert.show("你不是该休闲场成员");
                return;
            }
            CommandManager.addCommand(new GetArenaRadioByAgentCommand(Player.player.userid), radioBack);
        }

        public void radioBack(object obj)
        {
            if (obj == null) return;
            object[] list = (object[])obj;
            runview.hideAllView();
            runview.settingView.setData((RebateList)list[0]);
            runview.settingView.refresh();
            runview.settingView.setVisible(true);
        }

        public void detailListback(object obj)
        {
            if (obj == null) return;
            runview.hideAllView();
            object[] objs = (object[])obj;
            runview.nextView.setData(objs);
            runview.nextView.refresh();
            runview.nextView.setVisible(true);
        }

        public void msgBack(object obj)
        {
            if (obj == null) return;
            runview.hideAllView();
            ArrayList<ArenaEvent> list = (ArrayList<ArenaEvent>)obj;
            runview.applyView.setList(list);
            runview.applyView.refresh();
            runview.applyView.setVisible(true);
        }
    }
}
