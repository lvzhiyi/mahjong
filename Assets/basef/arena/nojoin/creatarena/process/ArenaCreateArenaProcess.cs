using cambrian.common;
using cambrian.game;
using scene.game;
using System.Text.RegularExpressions;

namespace basef.arena
{
    /// <summary> 创建竞赛场 按钮 </summary>
    public class ArenaCreateArenaProcess : MouseClickProcess
    {
        public override void execute()
        {
            ArenaCreateArenaPnael panel = this.getRoot<ArenaCreateArenaPnael>();

            string name = panel.arenaname.text;
            if (name == null || name.Length == 0)
            {
                Alert.show("请输入竞赛场名字");
                return;
            }


            if (!Regex.Match(name, "[\u4e00-\u9fa5_a-zA-Z0-9]{2,14}").Success)
            {
                Alert.show("竞赛场名字只能包含中文，数字，字母的组合。名字长度为2-12个字符");
                panel.arenaname.text = "";
                return;
            }

            name = MaskWord.replace(name);
            panel.arenaname.text = name;

            string notice = panel.notice.text;
            if (notice == null || notice.Length == 0)
            {
                notice = "适度游戏益脑,沉迷游戏伤身,合理安排时间,享受健康生活。";
            }

            if (!Regex.Match(notice, "[\u4e00-\u9fa5_a-zA-Z0-9]{4,30}").Success)
            {
                Alert.show("公告内容只能包含中文，数字，字母的组合。公告长度为4-30个字符");
                panel.notice.text = "";
                return;
            }

            notice = MaskWord.replace(notice);
            panel.notice.text = notice;
            CommandManager.addCommand(new CreateArenaCommand(name,notice,panel.rank,panel.closing),back);
        }

        private void back(object obj)
        {
            if (obj == null) return;
            ArenaPanel panel = UnrealRoot.root.getDisplayObject<ArenaPanel>();
            panel.refresh();
            UnrealRoot.root.showPanel<ArenaPanel>();

            ByteBuffer data = new ByteBuffer();
            data.writeLong(Arena.arena.getId());
            FileCachesManager.savePath(CacheLocalPath.APPLICATION_PATH + CacheLocalPath.LAST_ENTER_AREN_PATH, false, data);
        }
    }
}
