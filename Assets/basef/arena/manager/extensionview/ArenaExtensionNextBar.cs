using basef.player;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaExtensionNextBar:ScrollBar
    {
        /// <summary>
        /// 总积分
        /// </summary>
        private UnrealTextField totalscore;
        /// <summary>
        /// 个人积分
        /// </summary>
        private UnrealTextField task;

        private Text nickname;

        private Text id;
        /// <summary>
        /// 游戏次数
        /// </summary>
        private Text games;
        /// <summary>
        /// 奖励
        /// </summary>
        private Text awards;
        /// <summary>
        /// 推广任务
        /// </summary>
        //private UnrealTextField task;
        /// <summary>
        /// 所属关系
        /// </summary>
        private UnrealTextField subordinate;
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView head;
        /// <summary>
        /// 合伙人对象
        /// </summary>
        public ArenaNextExtension extension;

        //private Image addtask;

        private UnrealScaleButton settingButton;

        protected override void xinit()
        {
            totalscore = transform.Find("totalscore").GetComponent<UnrealTextField>();
            task = transform.Find("selfscore").GetComponent<UnrealTextField>();
            nickname = transform.Find("nickname").GetComponent<Text>();
            id = transform.Find("id").GetComponent<Text>();
            head = transform.Find("head").GetComponent<PlayerCircleHeadView>();
            head.init();
            subordinate = transform.Find("subordinate").GetComponent<UnrealTextField>();
            //task = transform.Find("task").GetComponent<UnrealTextField>();
            //task.init();
            games = transform.Find("games").GetComponent<Text>();
            awards = transform.Find("award").GetComponent<Text>();
            //addtask = task.transform.Find("bg1").GetComponent<Image>();
            settingButton = transform.Find("operate").GetComponent<UnrealScaleButton>();
        }

        public void setData(ArenaNextExtension extension)
        {
            this.extension = extension;
        }

        public void callback(long task,long totalscore)
        {
            extension.setTask(task);
            extension.setTotalScore(totalscore);
            this.task.text = extension.getTask().ToString();
            this.totalscore.text = extension.getTotalScore().ToString();
        }

        protected override void xrefresh()
        {
            nickname.text = extension.name;
            id.text = "ID:"+extension.userid;
            head.setData(extension.head,extension.userid);
            head.refresh();
            task.text = extension.getTask().ToString();
            totalscore.text= extension.getTotalScore().ToString();// 新增总积分
            subordinate.text = extension.mastername;
            awards.text = extension.getTodayGolds() + "/" + extension.getYesterDayGolds() + "/" + extension.getWeekGolds() + "/" +
                          extension.getLastWeekGolds();
            games.text = extension.today_games + "/" + extension.yesterday_games + "/" + extension.week_games + "/" +
                          extension.lastweek_games;
            //addtask.gameObject.SetActive(extension.master==Player.player.userid);
            settingButton.setVisible(extension.master == Player.player.userid);
        }
    }
}
