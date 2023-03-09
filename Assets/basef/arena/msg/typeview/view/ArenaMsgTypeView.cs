using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 赛场消息 总管理视图 </summary>
    public class ArenaMsgTypeView : UnrealLuaSpaceObject
    {
        /// <summary> 赛场审核界面 </summary>
        [HideInInspector] public ArenaMsgApplyView arenaApplyView;

        /// <summary> 退出申请界面 </summary>
        [HideInInspector] public ArenaMsgChangeView changeView;

        /// <summary> 加入申请界面 </summary>
        [HideInInspector] public ArenaMsgJoinView joinView;

        /// <summary> 房间超时界面 </summary>
        [HideInInspector] public ArenaMsgTimeOutView timeoutView;

        /// <summary> 辅分 </summary>
        [HideInInspector] public ArenaMsgAuxiliaryScoreView auxiliaryscoreView;

        /// <summary> 个人日志 </summary>
        [HideInInspector] public ArenaPersonalJournalView personalJournalView;

        protected override void xinit()
        {
            arenaApplyView = transform.Find("arenaApplyView").GetComponent<ArenaMsgApplyView>();
            arenaApplyView.init();

            changeView = transform.Find("changeView").GetComponent<ArenaMsgChangeView>();
            changeView.init();

            joinView = transform.Find("joinView").GetComponent<ArenaMsgJoinView>();
            joinView.init();

            timeoutView = transform.Find("timeoutView").GetComponent<ArenaMsgTimeOutView>();
            timeoutView.init();

            auxiliaryscoreView = transform.Find("auxiliaryscoreView").GetComponent<ArenaMsgAuxiliaryScoreView>();
            auxiliaryscoreView.init();

            personalJournalView = transform.Find("personalJournalView").GetComponent<ArenaPersonalJournalView>();
            personalJournalView.init();
        }

        protected override void xrefresh()
        {
            //hideAll();
        }

        private void hideAll()
        {
            arenaApplyView.setVisible(false);
            changeView.setVisible(false);
            joinView.setVisible(false);
            timeoutView.setVisible(false);
            auxiliaryscoreView.setVisible(false);
            personalJournalView.setVisible(false);
        }

        public void showView(int index,object obj = null)
        {
            hideAll();
            switch (index)
            {
                case ArenaMsgPanel.Msg_ArenaApply://赛场审核
                    arenaApplyView.setData((ArrayList<ArenaEvent>)obj);
                    arenaApplyView.setVisible(true);
                    break;
                case ArenaMsgPanel.Msg_ChangeApply://变动更改
                    changeView.setData((ArenaMemberChangeRecord[])obj);
                    changeView.setVisible(true);
                    break;
                case ArenaMsgPanel.Msg_JoinApply://加入申请
                    joinView.setData((ArrayList<ArenaMsgJoinContentData>)obj);
                    joinView.setVisible(true);
                    break;
                case ArenaMsgPanel.Msg_RoomTimeOut://房间超时
                    timeoutView.setData((ArrayList<ArenaRoomEvent>)obj);
                    timeoutView.setVisible(true);
                    break;
                case ArenaMsgPanel.Msg_ArenaAuxiliaryScore://辅分
                    auxiliaryscoreView.setData((AuxiliaryScoreEventRecord[])obj);
                    auxiliaryscoreView.setVisible(true);
                    break;
                case ArenaMsgPanel.Msg_ArenaPersonalJournal://个人日志
                    personalJournalView.setData((PersonalJournalEventRecord[])obj);
                    personalJournalView.setVisible(true);
                    break;
            }
        }
    }
}
