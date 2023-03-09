using cambrian.common;

namespace basef.arena
{
    /// <summary> 玩家点击 报名 </summary>
    public class ArenaSignUpProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaSignUpBar bar = transform.parent.GetComponent<ArenaSignUpBar>();
            CommandManager.addCommand(new ArenaApplyUserAttendCommand(bar.num),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            if ((bool)obj)
                Alert.show("报名成功!!!");
            else Alert.show("报名失败!!!");
        }
    }
}
