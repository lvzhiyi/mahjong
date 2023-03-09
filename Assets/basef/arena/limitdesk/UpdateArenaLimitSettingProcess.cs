using cambrian.common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场限制组确定按钮
    /// </summary>
    public class UpdateArenaLimitSettingProcess : MouseClickProcess
    {
        ArenaLimitPanel panel;
        ArenaMemberDetailView view;
        ArenaManagerPanel arena;
        public override void execute()
        {
            arena = UnrealRoot.root.getDisplayObject<ArenaManagerPanel>();
            view = arena.transform.Find("Canvas").Find("content").Find("view").Find("memberview").Find("operate").GetComponent<ArenaMemberDetailView>();
            panel = UnrealRoot.root.getDisplayObject<ArenaLimitPanel>();

            CommandManager.addCommand(new ArenaLimitUpdateSettingCommand(Arena.arena.getId(), view.member.userid, panel.limitindex), back);
        }

        private void back(object o)
        {
            if (o == null) return;
            if (panel.limitindex == 0)
                view.member.mutexStatus = 0;
            else if (panel.limitindex == 1)
                view.member.mutexStatus = 1;
            else if (panel.limitindex == 2)
                view.member.mutexStatus = 2;
            else if (panel.limitindex == 3)
                view.member.mutexStatus = 3;
            Alert.autoShow("修改成功!");
            panel.setVisible(false);
        }
    }
}