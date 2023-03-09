using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 推广管理
    /// </summary>
    public class ArenaExtensionView : UnrealLuaSpaceObject
    {
        [HideInInspector] public ArenaSelfExtensionView selfExtensionView;
        /// <summary>
        /// 下级推广员
        /// </summary>
        [HideInInspector] public ArenaExtensionNextView nextView;
        /// <summary>
        /// 推广设置
        /// </summary>
        [HideInInspector] public ArenaExtensionSettingView settingView;
        /// <summary>
        /// 任务申请
        /// </summary>
        [HideInInspector] public ArenaExtensionTaskApplyView applyView;

        [HideInInspector] public UnrealRadioList radioList;

        public string[] title = new string[]{"我的任务","下级合伙人","推广设置","任务申请"};


        protected override void xinit()
        {
            selfExtensionView = transform.Find("extensiontask").GetComponent<ArenaSelfExtensionView>();
            selfExtensionView.init();
            nextView = transform.Find("promoters").GetComponent<ArenaExtensionNextView>();
            nextView.init();
            settingView = transform.Find("extensionsetting").GetComponent<ArenaExtensionSettingView>();
            settingView.init();
            applyView = transform.Find("taskapply").GetComponent<ArenaExtensionTaskApplyView>();
            applyView.init();
            this.radioList = this.transform.Find("type").GetComponent<UnrealRadioList>();
            this.radioList.init();
        }

        public void showMore()
        {
            if(Arena.arena.maxLeve!=0&&(Arena.arena.getMember().level)!= Arena.arena.maxLeve)
                title = new string[] { "我的任务", "下级合伙人", "推广设置", "任务申请" };
            else
            {
                title = new string[] { "我的任务" };
            }
        }


        protected override void xrefresh()
        {
            showMore();
            hideAllView();

            selfExtensionView.refresh();
            selfExtensionView.setVisible(true);

            this.radioList.resize(title.Length);
            for (int i = 0; i < title.Length; i++)
            {
                ArenaRunTypesBar bar = (ArenaRunTypesBar) this.radioList.bars[i];
                bar.index_space = i;
                bar.setTitle(title[i]);
            }

            showTypes(0);
        }

        public void showTypes(int index)
        {
            radioList.selectedIndex(index);
        }

        public void hideAllView()
        {
            selfExtensionView.setVisible(false);
            nextView.setVisible(false);
            settingView.setVisible(false);
            applyView.setVisible(false);
        }
    }
}
