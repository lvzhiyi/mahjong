using cambrian.common;

namespace basef.arena
{
    public class OpenArenaStopGameProcess : MouseClickProcess
    {
        ArenaMemberDetailView view;
        Arena arena;
        public override void execute()
        {
            Alert.confirmShow("终止玩家比赛后，玩家金豆清零。", ok);
        }
        public void ok(object obj)
        {
            if (obj == null) return;
            view = transform.parent.parent.GetComponent<ArenaMemberDetailView>();
            CommandManager.addCommand(new GetArenaStopGameCommand(view.member.userid),back);
        }
        private void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
            {
                Alert.show("操作成功!");
                view.member.setArenaGold(0);
                view.setCallBack();
            }
            view.refresh();
            view.setVisible(false);
        }

    }
}
