using cambrian.unreal.scroll;
using UnityEngine;
using UnityEngine.UI;

namespace basef.rank
{
    /// <summary>
    /// 茶馆排行榜
    /// </summary>
    public class TeaRankListPanel:UnrealLuaPanel
    {
        private ScrollContainer container;

        private TeaRank[] ranks;

        private Text infoText;

        /// <summary>
        /// 文档地址：0活动规则文档，1奖励说明文档
        /// </summary>
        [HideInInspector] public static string[] NAMES = { "/cgzb.txt", "/phjl.txt" };

        [HideInInspector] public string info = "本活动仅记录金豆娱乐场在活动期间产生的局数。活动时间:2019.08.17 11:00:00 -- 2019.09.01 23:59:59";

        protected override void xinit()
        {
            base.xinit();
            this.container = content.Find("container").GetComponent<ScrollContainer>();
            this.container.init();
            this.infoText = content.Find("info").GetComponent<Text>();
        }


        public void setData(TeaRank[] ranks)
        {
            this.ranks = ranks;
        }


        protected override void xrefresh()
        {
            base.xrefresh();
            this.container.updateData(updateBarData);
            this.container.resize(this.ranks.Length);
            this.infoText.text = info;
        }

        public void updateBarData(ScrollBar bar, int index)
        {
            ((TeaRankBar)bar).setData(index,ranks[index]);
        }
    }
}
