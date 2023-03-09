using cambrian.common;
using System.Text.RegularExpressions;

namespace basef.arena
{
    /// <summary>
    /// 更新基础设置
    /// </summary>
    public class UPdateArenaBaseSettingProcess:MouseClickProcess
    {
        private ArenaSettingView view;
        public override void execute()
        {
            view= transform.parent.GetComponent<ArenaSettingView>();
            string name = view.areanname.text;
            if (name == null || name.Length == 0)
            {
                Alert.show("请输入休闲场名字");
                return;
            }

            if (!Regex.Match(name, "[\u4e00-\u9fa5_a-zA-Z0-9]{2,14}").Success)
            {
                Alert.show("休闲场名字只能包含中文，数字，字母的组合。名字长度为2-14个字符");
                view.areanname.text = "";
                return;
            }

            name = MaskWord.replace(name);

            string notice = view.notice.text;
            if (notice == null || notice.Length == 0)
            {
                Alert.show("请输入公告内容");
                return;
            }

            if (!Regex.Match(notice, "[\u4e00-\u9fa5_a-zA-Z0-9]{4,30}").Success)
            {
                Alert.show("公告内容只能包含中文，数字，字母的组合。公告长度为4-30个字符");
                view.notice.text = "";
                return;
            }

            notice = MaskWord.replace(notice);

            CommandManager.addCommand(new UpdateArenaBaseSettingCommand(Arena.arena.getId(),name,notice,view.other.rankValue,view.other.suspendValue), back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            int status = (int)obj;
            Arena arena= Arena.arena;
            arena.status = status;
            arena.setNotice(view.notice.text);
            arena.setName(view.areanname.text);
            Alert.autoShow("修改成功!");
        }
    }
}
