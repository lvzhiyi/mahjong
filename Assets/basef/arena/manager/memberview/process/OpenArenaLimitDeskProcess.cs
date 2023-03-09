using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace basef.arena
{   /// <summary>
/// 竞赛场 打开同桌限制界面按钮 擂主可见
/// </summary>
    public class OpenArenaLimitDeskProcess : MouseClickProcess
    {
        ArenaLimitPanel limit;
        public override void execute()
        {
            limit = UnrealRoot.root.getDisplayObject<ArenaLimitPanel>();
            limit.refresh();
            limit.setCVisible(true);
        }
    }
}