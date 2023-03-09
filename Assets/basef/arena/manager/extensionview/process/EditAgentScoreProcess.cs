using basef.player;
using System;

namespace basef.arena
{
    /// <summary>
    /// 给下级代理增加推广任务
    /// </summary>
    public class EditAgentScoreProcess : MouseClickProcess
    {
        /// <summary>
        /// 类型为0表示给下级编辑积分，1部主编辑自己积分
        /// </summary>
        public int type;

        public override void execute()
        {
            ArenaExtensionNextView nextView = getRoot<ArenaAgentPanel>().nextView;
            long userid=0;
            Action<long,long> callback = null;
            if (type == 0)
            {
                ArenaExtensionNextBar bar = nextView.operateAgent;
                //if (bar.extension.master != Player.player.userid) return;
                userid = bar.extension.userid;
                callback = bar.callback;
            }
            else
            {
                if (Arena.arena.getMaster() != Player.player.userid) return;
                userid = Player.player.userid;
                callback = nextView.callback;
            }

            ArenaExtensionChangePanel panel = UnrealRoot.root.getDisplayObject<ArenaExtensionChangePanel>();
            panel.setData(userid, nextView.score, nextView.total, callback);
            panel.refresh();
            panel.setCVisible(true);
        }
    }
}
