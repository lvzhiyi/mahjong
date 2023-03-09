using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    public class ArenaAccountsGold
    {
        /// <summary> 类型：比赛赢得 </summary>
        public const int TYPE_MATCH_WIN = 101;
        /// <summary> 类型：报名获得 </summary>
        public const int TYPE_APPLY_MATCH = 102;
        /// <summary> 类型：门票奖励 </summary>
        public const int TYPE_INCR_TICKETS = 103;
        /// <summary> 类型：门票退回 </summary>
        public const int TYPE_INCR_TICKETS_BACK = 104;

        /// <summary> 类型：踢出成员增加 </summary>
        public const int TYPE_KICK_MEMBER_ADD = 106;
        /// <summary> 类型：踢出合伙人增加 </summary>
        public const int TYPE_KICK_AGENT_ADD = 107;


        /// <summary> 类型：比赛输掉 </summary>
        public const int TYPE_MATCH_LOSE = 201;
        /// <summary> 类型：兑换奖章 </summary>
        public const int TYPE_INCR_MEDAL = 202;
        /// <summary> 类型：门票扣除 </summary>
        public const int TYPE_DECR_TICKETS = 203;
        /// <summary> 类型：扣除保底门票 </summary>
        public const int TYPE_DECR_SUB_TICKETS = 204;

        /// <summary> 类型：踢出成员减少 </summary>
        public const int TYPE_KICK_MEMBER_DECR = 206;
        /// <summary> 类型：踢出合伙人减少 </summary>
        public const int TYPE_KICK_AGENT_DECR = 207;


        /// <summary> 类型：全部（前台使用） </summary>
        public const int TYPE_ALL = 0;
        /// <summary> 类型:比赛输赢（前台使用） </summary>
        public const int TYPE_MATCH = 301;
    }

    /// <summary> 金豆明细 </summary>
    public class ArenaAccountsGoldPanel : UnrealLuaPanel
    {
        /// <summary> 顶部管理 </summary>
        ArenaAccountsGoldTopView topview;

        /// <summary> 内容管理 </summary>
        ArenaAccountsGoldContainerView contentview;

        bool checkgoldminus = false;

        [HideInInspector] public long userid;

        protected override void xinit()
        {
            base.xinit();
            topview = this.content.Find("centers").Find("top").GetComponent<ArenaAccountsGoldTopView>();
            topview.init();

            contentview = this.content.Find("centers").Find("container").GetComponent<ArenaAccountsGoldContainerView>();
            contentview.init();
        }

        protected override void xrefresh()
        {
            base.xrefresh();

            topview.refresh();
            contentview.refresh();
        }

        /// <summary> 设置是否查看金豆流水不足 </summary>
        public void setCheckGoldminus()
        {
            checkgoldminus = !checkgoldminus;
        }

        /// <summary> 获取是否查看金豆流水不足 </summary>
        public bool getCheckGoldminus()
        {
            return checkgoldminus;
        }

        public void setData(ArrayList<ArenaAccountsGoldContentData> obj, long userid = 0,double gold=0)
        {
            contentview.setData(obj);
            if (userid != 0)
                this.userid = userid;
            topview.setCheckGoldMinus(checkgoldminus);
        }

        public void setInitData(ArrayList<ArenaAccountsGoldContentData> obj, long userid = 0, double gold = 0)
        {
            contentview.setData(obj);
            if (userid != 0)
                this.userid = userid;
            topview.setData(gold);
            topview.setCheckGoldMinus(checkgoldminus);
        }

        public int getType()
        {
            switch (topview.typeindex)
            {
                case 1: return ArenaAccountsGold.TYPE_DECR_TICKETS;
                case 2: return ArenaAccountsGold.TYPE_MATCH;
                case 3: return ArenaAccountsGold.TYPE_APPLY_MATCH;
                case 4: return ArenaAccountsGold.TYPE_INCR_MEDAL;
                case 5: return ArenaAccountsGold.TYPE_INCR_TICKETS;
                case 6: return ArenaAccountsGold.TYPE_INCR_TICKETS_BACK;
                default: return ArenaAccountsGold.TYPE_ALL;
            }
        }
    }
}