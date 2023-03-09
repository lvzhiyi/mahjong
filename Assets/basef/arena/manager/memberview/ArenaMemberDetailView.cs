using System;
using UnityEngine;

namespace basef.arena
{
    /// <summary>
    /// 玩家详细操作
    /// </summary>
    public class ArenaMemberDetailView : UnrealLuaSpaceObject
    {
        [HideInInspector] public ArenaMember member;

        public Action setCallBack;

        private UnrealTextScaleButton lahei;
        private UnrealScaleButton limitdesk;
        private UnrealScaleButton kickwf;



        ArenaMemberBar bar;


        protected override void xinit()
        {
            lahei = transform.Find("group").Find("lahei").GetComponent<UnrealTextScaleButton>();
            lahei.init();
            limitdesk = transform.Find("group").Find("limitdesk").GetComponent<UnrealScaleButton>();
            limitdesk.init();
            this.setVisible(false);
            kickwf = transform.Find("group").Find("kickwf").GetComponent<UnrealScaleButton>();
            kickwf.init();
        }

        public void setData(ArenaMember member)
        {
            this.member = member;
        }

        public void setArenaManagerBar(ArenaMemberBar bar)
        {
            this.bar = bar;
        }

        protected override void xrefresh()
        {
            //只有玩家才显示踢出玩家按钮
            kickwf.setVisible(false);
            lahei.setVisible(Arena.arena.getMember().isMaster());
            limitdesk.setVisible(Arena.arena.getMember().isMaster());
            if (!member.isAgent() && !member.isMaster())
            {
                kickwf.setVisible(true);
                limitdesk.setVisible(false);
            }

            if (member.hasStatus(ArenaMember.STATUS_ARENA_LIMIT))
            {
                lahei.text = "恢复娱乐";
            }
            else
            {
                lahei.text = "暂停娱乐";
            }
        }
    }
}
