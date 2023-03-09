using cambrian.common;

namespace basef.arena
{
    /// <summary> 导入茶馆成员</summary>
    public class ArenaImprotTeahouseContentBarOKProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaImprotTeahouseContentBar bar = transform.parent.GetComponent<ArenaImprotTeahouseContentBar>();

            CommandManager.addCommand(new ArenaImprotMemberCommand(bar.getData().type,bar.getData().id),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            int[] data = (int[])obj;

            string hint = "<size=22>总人数:" + data[0] + "人,成功导入:" + data[1] + "人.</size>";
            Alert.show(hint);
        }
    }
}
