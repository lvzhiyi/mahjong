using cambrian.common;

namespace basef.arena
{
    /// <summary>
    /// 进入赛场
    /// </summary>
    public class EnterArenaProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaListBar bar = this.transform.parent.GetComponent<ArenaListBar>();
            CommandManager.addCommand(new GetArenaInfoCommand(bar.arena.getId()), back);    
        }

        private void back(object obj)
        {
            if (obj == null) return;
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            panel.setVisible(true);
            root.setVisible(false);
            Arena arena = Arena.arena;
            ArenaMember member = arena.getMember();
            if (member.isWarning)
                Alert.show("积分低于预警值，请尽快联系上级。");
            else if(member.lowerFufeng)
            {
                Alert.show("你的赛场【" + arena.getName() + "(ID:" + arena.getId() + ")】 积分低于设置值，名下成员被禁止游戏，请尽快联系上级。");
            }
        }
    }
}
