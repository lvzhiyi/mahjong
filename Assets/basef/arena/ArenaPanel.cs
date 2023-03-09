using basef.lobby;
using cambrian.common;
using cambrian.game;
using scene.game;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场面板
    /// </summary>
    public class ArenaPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 上方视图
        /// </summary>
        [HideInInspector] public ArenaTopView topView;
        /// <summary>
        /// 下方视图
        /// </summary>
        private ArenaBottomView bottomView;

        /// <summary>
        /// 桌子
        /// </summary>
        public ArenaDeskView deskView;

        /// <summary>
        /// 休闲场快速开始界面
        /// </summary>
        public ArenaQuickGameView quickView;
        protected override void xinit()
        {
            base.xinit();
            topView = content.Find("tops").GetComponent<ArenaTopView>();
            topView.init();

            bottomView = content.Find("bottoms").GetComponent<ArenaBottomView>();
            bottomView.init();

            deskView = content.Find("desk").GetComponent<ArenaDeskView>();
            deskView.init();

            quickView = content.Find("fastgame").GetComponent<ArenaQuickGameView>();
            quickView.init();
        }

        protected override void xrefresh()
        {
            topView.refresh();
            bottomView.refresh();
            deskView.refresh();
            quickView.setData(loadSaveRuleUuid());
            quickView.refresh();
            //topView.showInvitationView(false);
           
        }

        private long curttime;

        protected override void xupdate()
        {
            if (curttime == 0 || !SpotRedRoot.roots.isLoadOk)
            {
                curttime = TimeKit.currentTimeMillis;
                return;
            }

            long time = TimeKit.currentTimeMillis;

            if (time - curttime > 5000)
            {
               CommandManager.addCommand(new GetArenaInfoCommand(Arena.arena.getId()),back);
                curttime = time;
            }
        }
        
        public void back(object obj)
        {
            if (obj == null)
            {
                Alert.show("你不是该休闲场的成员");
                ShowLobbyPanel.showLobbyPanel();
                return;
            }
            topView.refresh();
            bottomView.refresh();
            deskView.rerefresh();

            quickView.setData(loadSaveRuleUuid());
            quickView.refresh();
        }

        public ArenaLockRule loadSaveRuleUuid()
        {
            ByteBuffer data = FileCachesManager.loadPathData(CacheLocalPath.ARENA_QUICK_GAME_INFO_PATH, true);
            int uuid = 0;
            if (data != null)
            {
                uuid = data.readInt();
            }
            ArenaLockRule rule=Arena.arena.fkcSettings.getRenaLockRule(uuid);
            if (rule == null && Arena.arena.fkcSettings.getRuleCount() > 0)
                rule = Arena.arena.fkcSettings.getRules().get(0);

            return rule;
        }
    }
}


