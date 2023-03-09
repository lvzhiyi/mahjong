using cambrian.common;

namespace basef.arena
{
    public class UpdateArenaExtensionArenaRadioProcess:MouseClickProcess
    {
        private ArenaExtensionSettingView view;
        public override void execute()
        {
            view = transform.parent.GetComponent<ArenaExtensionSettingView>();
            CommandManager.addCommand(new UpdateArenaRadioCommand(view.list),back);
        }

        public void back(object obj)
        {
            if(obj==null) return;
            Alert.autoShow("保存成功！");
            view.setData((RebateList)obj);
            view.refresh();
        }
    }
}
