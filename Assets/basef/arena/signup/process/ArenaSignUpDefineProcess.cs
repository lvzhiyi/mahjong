namespace basef.arena
{
    using cambrian.common;
    public class ArenaSignUpDefineProcess : MouseClickProcess
    {
        public override void execute()
        {
            UnrealInputTextField bar = transform.parent.Find("info").GetComponent<UnrealInputTextField>();
            int gold = StringKit.parseInt(bar.text);
            if (gold <= 0)
            {
                Alert.show("请输入大于0的金豆，报名！");
                return;
            }

            CommandManager.addCommand(new ArenaApplyUserAttendCommand(gold),back);
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
