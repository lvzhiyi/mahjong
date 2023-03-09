using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 竞赛场合伙人界面
    /// </summary>
    public class ArenaAgentPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 合伙人0，奖励设置1
        /// </summary>
        public const int AGENT_DATA = 0, AGENG_SETTING = 1;


        string[] nametype = new string[]
        {
            "合伙人","合伙人设置"
        };

        private int selectType = 0;

        private ScrollContainer container;

        /// <summary>
        /// 下级推广员
        /// </summary>
        [HideInInspector] public ArenaExtensionNextView nextView;
        /// <summary>
        /// 推广设置
        /// </summary>
        ArenaExtensionSettingView settingView;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("leftType").GetComponent<ScrollContainer>();
            container.init();
            nextView = content.Find("content").Find("view").Find("promoters").GetComponent<ArenaExtensionNextView>();
            nextView.init();
            settingView = content.Find("content").Find("view").Find("extensionsetting").GetComponent<ArenaExtensionSettingView>();
            settingView.init();
        }

        protected override void xrefresh()
        {
            container.updateData(callback);
            container.resize(nametype.Length);
            container.resizeDelta();

            updateSelect(0);
        }

        public void callback(ScrollBar bar, int index)
        {
            ArenaLeftTypeBar temp = (ArenaLeftTypeBar)bar;
            temp.setData(nametype[index], selectType);
            temp.index_space = index;
            temp.refresh();
        }

        public void updateSelect(int type)
        {
            selectType = type;
            for (int i = 0; i < this.container.bars.count; i++)
            {
                ArenaLeftTypeBar bar = (ArenaLeftTypeBar)this.container.bars.get(i);
                bar.setSelected(type);
                bar.refresh();
            }
            nextView.gameObject.SetActive(false);
            settingView.gameObject.SetActive(false);
            if (type == AGENT_DATA)
            {
                CommandManager.addCommand(new GetArenaAgentDetailListCommand(), obj =>
                {
                    object[] objs = (object[])obj; 
                    nextView.setData(objs);
                    nextView.refresh();
                    nextView.setVisible(true);
                });
            }
            else
            {
                CommandManager.addCommand(new GetArenaRadioByAgentCommand(Player.player.userid), obj =>
                {
                    if (obj == null) return;
                    object[] list = (object[])obj;
                    settingView.setData((RebateList)list[0]);
                    settingView.refresh();
                    settingView.setVisible(true);
                });
            }
        }
    }
}
