using System.Collections.Generic;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 消息界面 </summary>
    public class ArenaMsgPanel : UnrealLuaPanel
    {
        /// <summary> 加入申请 </summary>
        public const int Msg_JoinApply = 3;

        /// <summary> 房间超时 </summary>
        public const int Msg_RoomTimeOut = 2;

        /// <summary> 变动更改 </summary>
        public const int Msg_ChangeApply = 1;

        /// <summary> 赛场审核 </summary>
        public const int Msg_ArenaApply = 0;

        /// <summary> 辅分 </summary>
        public const int Msg_ArenaAuxiliaryScore = 4;

        /// <summary> 个人日志 </summary>
        public const int Msg_ArenaPersonalJournal = 6;

        /* "加入申请","变动记录","房间超时","赛场审核" ,"变动记录"*/
        string[] nametype = new string[] { "赛场审核","变动记录","房间超时","辅分" };

        ArrayList<int> msgtype = new ArrayList<int>();

        ScrollContainer container;

        [HideInInspector] public ArenaMsgTypeView managerView;

        public int selectType;

        protected override void xinit()
        {
            base.xinit();
            container = content.Find("content").Find("typeshow").GetComponent<ScrollContainer>();
            container.init();

            managerView = content.Find("content").Find("view").GetComponent<ArenaMsgTypeView>();
            managerView.init();

            msgtypeInit();
        }

        void msgtypeInit()
        {
            msgtype.clear();
            if (!Arena.arena.getMember().isMaster())
            {
                msgtypeAdd(Msg_ArenaApply,Msg_ArenaAuxiliaryScore);
                nametype = new string[] { "赛场审核","辅分"};
            }
            else
            {
                msgtypeAdd(Msg_ArenaApply,Msg_RoomTimeOut,Msg_ChangeApply, Msg_ArenaAuxiliaryScore);
                nametype = new string[] { "赛场审核","房间超时","变动记录", "辅分" };
            }
            selectType = msgtype.get(0);
        }

        void msgtypeAdd(params int[] types)
        {
            for (int i = 0; i < types.Length; i++)
            {
                msgtype.add(types[i]);
            }
        }

        protected override void xrefresh()
        {
            msgtypeInit();
            container.updateData(callback);
            container.resize(msgtype.count);
            container.resizeDelta();

            managerView.refresh();
        }

        public void callback(ScrollBar bar,int index)
        {
            ArenaMsgTypeBar managerbar = (ArenaMsgTypeBar)bar;
            managerbar.index_space = index;
            managerbar.setData(nametype[index],msgtype.get(index),selectType);
            managerbar.refresh();
        }

        public void updateBars(int type)
        {
            selectType = type;
            for (int i = 0; i < this.container.bars.count; i++)
            {
                ArenaMsgTypeBar bar = (ArenaMsgTypeBar)this.container.bars.get(i);
                bar.setSelected(type);
                bar.refresh();
            }
        }
    }
}
