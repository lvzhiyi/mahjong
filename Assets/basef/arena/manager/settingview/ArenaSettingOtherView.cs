using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 其他设置视图
    /// </summary>
    public class ArenaSettingOtherView : UnrealLuaSpaceObject
    {
        private ArenaSettingOtherBar rank;
        private ArenaSettingOtherBar close;
        private ArenaSettingOtherBar showmoney;
        private ArenaSettingOtherBar showroomname;

        /// <summary>
        /// 排行榜
        /// </summary>
        [HideInInspector] public bool rankValue;

        /// <summary>
        /// 打烊
        /// </summary>
        [HideInInspector] public bool suspendValue;

        /// <summary>
        /// 显示积分值
        /// </summary>
        [HideInInspector] public int showmoneyValue;

        /// <summary>
        /// 显示等待房间
        /// </summary>
        [HideInInspector] public int showroomnameValue;
        

        protected override void xinit()
        {
            rank = transform.Find("rank").GetComponent<ArenaSettingOtherBar>();
            rank.init();
            close = transform.Find("close").GetComponent<ArenaSettingOtherBar>();
            close.init();
            showmoney = transform.Find("showmoney").GetComponent<ArenaSettingOtherBar>();
            showmoney.init();
            showroomname = transform.Find("showroomname").GetComponent<ArenaSettingOtherBar>();
            showroomname.init();
        }

        public void setData()
        {
            rankValue = Arena.arena.hasStatus(Arena.STATE_NO_SHOW_RANK);
            suspendValue = Arena.arena.hasStatus(Arena.STATE_SUSPEND);
        }

        protected override void xrefresh()
        {
            rank.setData("排行榜", rankValue);
            close.setData("打烊", suspendValue);
            showmoney.setData("对局积分值", showmoneyValue == 1);
            showroomname.setData("不显示等待房间", showroomnameValue == 1);
        }
    }
}
