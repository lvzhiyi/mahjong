using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 赛场管理视图
    /// </summary>
    public class ArenaRunView:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 选择类型
        /// </summary>
        [HideInInspector] public UnrealRadioList types;
        /// <summary>
        /// 奖励视图
        /// </summary>
        [HideInInspector] public ArenaAwardView awardView;

        /// <summary>
        /// 赛场报表视图
        /// </summary>
        [HideInInspector] public ArenaReportformView formView;

        /// <summary>
        /// 标题名称
        /// </summary>
        private string[] title_name = new string[] { "赛场奖励", "赛场报表" };

        protected override void xinit()
        {
            types = transform.Find("type").GetComponent<UnrealRadioList>();
            types.init();
            awardView = transform.Find("award").GetComponent<ArenaAwardView>();
            awardView.init();
            formView = transform.Find("reportform").GetComponent<ArenaReportformView>();
            formView.init();
        }

        protected override void xrefresh()
        {
            types.resize(title_name.Length);
            for (int i = 0; i < title_name.Length; i++)
            {
                ArenaRunTypesBar bar = (ArenaRunTypesBar)this.types.bars[i];
                bar.index_space = i;
                bar.setTitle(title_name[i]);
            }
            showTypes(0);
            awardView.refresh();
            awardView.setVisible(true);
            formView.setVisible(false);
        }

        public void showTypes(int index)
        {
            types.selectedIndex(index);
        }
    }
}
