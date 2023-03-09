using cambrian.common;
using System;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary>
    /// 合伙人修改积分面板
    /// </summary>
    public class ArenaExtensionChangePanel:UnrealLuaPanel
    {
        /// <summary>
        /// 输入的推广任务
        /// </summary>
        public UnrealInputTextField inputText;

        private UnrealTextField selfScore;

        private UnrealTextField totalScore;

        private Text totalTitle;
        /// <summary>
        /// 目标id
        /// </summary>
        public long dest;

        long score;

        long total;

        public Action<long, long> callback;

        protected override void xinit()
        {
            base.xinit();
            inputText = content.Find("name").GetComponent<UnrealInputTextField>();
            inputText.init();
            selfScore = content.Find("score").GetComponent<UnrealTextField>();
            selfScore.init();
            totalScore = content.Find("total").GetComponent<UnrealTextField>();
            totalScore.init();
            totalTitle = totalScore.transform.Find("text_info").GetComponent<Text>();
        }


        public void setData(long dest, long score, long total, Action<long, long> callback)
        {
            this.dest = dest;
            this.score = score;
            this.total = total;
            this.callback = callback;
        }


        protected override void xrefresh()
        {
            inputText.text = "";
            selfScore.text = MathKit.getRound2Long(score).ToString();
            totalScore.text = MathKit.getRound2Long(total + score).ToString();
            Arena arena = Arena.arena;
            ArenaMember member = arena.getMember();
            if (arena.isMaster(member.userid) || (arena.isMaster(member.master) && member.hasStatus(ArenaMember.STATUS_ADMIN)))
                totalTitle.text = "休闲场总积分:";
            else
                totalTitle.text = "合伙人总积分:";
        }
    }
}
