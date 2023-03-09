using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaReportformView : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 赛场金豆
        /// </summary>
        private Text arenaGold;

        private Text selfGold;

        private Text selfTask;

        private Text otherGold;

        private Text otherTask;

        /// <summary>
        /// 负金豆
        /// </summary>
        private UnrealTextField fuGold;

        private Text info;

        protected override void xinit()
        {
            info = transform.Find("info").GetComponent<Text>();
            arenaGold = transform.Find("num").GetComponent<Text>();
            selfGold = transform.Find("num1").GetComponent<Text>();
            selfTask = transform.Find("num2").GetComponent<Text>();
            otherGold = transform.Find("num3").GetComponent<Text>();
            otherTask = transform.Find("num4").GetComponent<Text>();
            fuGold = transform.Find("fugold").GetComponent<UnrealTextField>();
        }

        /// <summary>
        /// 赛场报表
        /// </summary>
        private ArenaForm form;

        public void setData(ArenaForm form)
        {
            this.form = form;
        }

        protected override void xrefresh()
        {
            if (Arena.arena.maxLeve == 0)
                info.text = " 赛场积分 = 我的积分 +  我的任务 +  其他会员积分";
            else
                info.text = " 赛场积分 = 我的积分 +  我的任务 +  其他会员积分 +  其他推广员任务";
            arenaGold.text = form.getTotalArenaGold().ToString();
            selfGold.text = form.getArenaGold().ToString();
            selfTask.text = form.getTask().ToString();
            otherGold.text = form.getTotalGold().ToString();
            fuGold.text = form.getOweGold().ToString();
            otherTask.gameObject.SetActive(Arena.arena.maxLeve != 0);
            otherTask.text = form.getNextTotalTask().ToString();
        }
    }
}
