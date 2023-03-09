using basef.im;
using cambrian.game;
using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace platform.spotred.room
{
    /// <summary>
    /// 房间(战局)
    /// </summary>
    [Hotfix]
    public class SpotRoomPanel : RoomPanel
    {
        /// <summary>
        /// 操作区
        /// </summary>
        public OperateView operateView;
        /// <summary>
        /// 房间号
        /// </summary>
        public Text room_index;
        /// <summary>
        /// 局数
        /// </summary>
        public Text jushu;
        /// <summary>
        /// gps图标
        /// </summary>
        Image gpsIcon;
        /// <summary>
        /// gps位置
        /// </summary>
        Vector3 gpspos;
        /// <summary>
        /// 机器人图标位置
        /// </summary>
        Vector3 robotpos;

        private SelfRobotView selfRobotView;

        /// <summary>
        /// 道具效果视图
        /// </summary>
        [HideInInspector] public PropView propView { set; get; }

        Image robotimg;

        private ExpressionView expressionView;
        /// <summary>
        /// 主要是用于正常比赛的时候，不清空队列中的消息。重连的时候会清空
        /// </summary>
        public static bool isStartMatch;

        protected override void xinit()
        {
            base.xinit();
            Transform hand = this.content.Find("hand");

            if (this.content.Find("operate") != null)
            {
                this.operateView = this.content.Find("operate").GetComponent<OperateView>();
                this.operateView.init();
            }

            this.room_index = top.Find("roomindex").Find("text").GetComponent<Text>();
            this.jushu = top.Find("jueshu").Find("text").GetComponent<Text>();

            this.headView = this.content.Find("heads").GetComponent<HeadView>();
            this.headView.init();
            this.allHandView = this.content.Find("hand").GetComponent<AllHandView>();
            this.allHandView.init();

            if (this.content.Find("robotview") != null)
            {
                this.selfRobotView = this.content.Find("robotview").GetComponent<SelfRobotView>();
                this.selfRobotView.init();
                this.selfRobotView.setVisible(false);
            }

            if (this.content.Find("propview") != null)
            {
                this.propView = this.content.Find("propview").GetComponent<PropView>();
                this.propView.init();
            }
            if (this.content.Find("top").Find("robot") != null)
            {
                robotimg = this.content.Find("top").Find("robot").GetComponent<Image>();
                robotpos = robotimg.transform.localPosition;
            }

            if (this.content.Find("top").Find("dwbtn") != null)
            {
                this.gpsIcon = this.content.Find("top").Find("dwbtn").GetComponent<Image>();
                this.gpspos = gpsIcon.transform.localPosition;
            }

            recvtimer = GetComponent<RecvOperateTimer>();
    
            if (this.content.Find("expressionview") != null)
            {
                this.expressionView = this.content.Find("expressionview").GetComponent<ExpressionView>();
                this.expressionView.init();
            }
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            headView.refresh();
            if (!isStartMatch && this.recvtimer != null)
                this.recvtimer.clearBaseOperate();
            else
                isStartMatch = false;

            this.allHandView.refresh();
            for (int i = 0; i < Room.room.getPlayerCount(); i++)
            {
                refreshDisCard(i);
            }

            refreshFixed(0);
            if (this.operateView != null)
            {
                this.operateView.rule = Room.room.roomRule.rule;
                this.operateView.refresh();
            }

            this.room_index.text = "房间号:" + Room.room.getRoomIndex();

            if (this.robotimg != null)
            {
                this.robotimg.gameObject.SetActive(Room.room.isType(Room.JBC));
            }
            if (Room.room.isType(Room.CLUB | Room.JBC)||Room.room.isType(Room.JBC))
            {
                this.jushu.text = "底分："+Room.room.roomRule.rule.getBet();
            }
            else
            {
                this.jushu.text = "局数:" + Room.room.roomRule.getNowPalyNum();
            }

            if (this.selfRobotView != null)
            {
                bool b = Room.room.getSelfMatchPlayer().isTrustee();
                this.selfRobotView.setVisible(b);
            }
            
            refreshFuShu();

            if (expressionView != null)
            {
                this.expressionView.refresh();
            }

            hideGPs();
        }

        public void hideGPs()
        {
            if (gpsIcon != null)
            {
                gpsIcon.gameObject.SetActive(true);
                if (Room.room.isType(Room.JBC) && Room.room.roomRule.rule.getBet() < 10000)
                {
                    gpsIcon.gameObject.SetActive(false);
                    robotimg.transform.localPosition = gpspos;
                }
                else
                {
                    if (robotimg != null)
                    {
                        robotimg.transform.localPosition = robotpos;
                    }
                    gpsIcon.transform.localPosition = gpspos;
                }
            }
        }

        /// <summary>
        /// 播放快捷聊天或表情
        /// </summary>
        /// <param name="msg"></param>
        public void playIMQuickMsg(IMQuickMsg msg)
        {
            long userid = msg.userid;
            if (headView.bottom.getPlayer().userid == userid)
            {
                expressionView.playQuickMsg(LOC_DOWN, msg);
            }
            else if (headView.top.getPlayer()!=null&&headView.top.getPlayer().userid == userid)
            {
                expressionView.playQuickMsg(LOC_TOP, msg);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expressionView.playQuickMsg(LOC_RIGHT, msg);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expressionView.playQuickMsg(LOC_LEFT, msg);
            }
        }

        public void playIMTextMsg(IMTextMsg msg)
        {
            long userid = msg.userid;
            if (headView.bottom.getPlayer().userid == userid)
            {
                expressionView.setIMTextMsg(LOC_DOWN, msg);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expressionView.setIMTextMsg(LOC_TOP, msg);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expressionView.setIMTextMsg(LOC_RIGHT, msg);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expressionView.setIMTextMsg(LOC_LEFT, msg);
            }
        }

        /// <summary>
        /// 播放声音 (这里没有判断有比赛玩家人数，顺序是下,上,左，右，这样来执行)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="b"></param>
        public void playVoice(long userid, bool b)
        {
            if (headView.bottom.getPlayer().userid == userid)
            {
                expressionView.playVoice(LOC_DOWN, b);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expressionView.playVoice(LOC_TOP, b);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expressionView.playVoice(LOC_RIGHT, b);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expressionView.playVoice(LOC_LEFT, b);
            }
        }

        public void setTrustee(int index, bool trustee)
        {
            headView.setTrustee(getPlayerIndex(index), trustee);
            if (Room.room.indexOf() == index)
            {
                selfRobotView.setVisible(trustee);
                this.robotimg.gameObject.SetActive(!trustee&& Room.room.isType(Room.JBC));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useid">使用者id</param>
        /// <param name="destIndex">目标索引</param>
        /// <param name="prop">道具id</param>
        public void showPropAnimation(long useid, int destIndex, int prop)
        {
            int useIndex = Room.room.getPlayerIndex(useid);
            propView.showPropAnimation(getPlayerIndex(useIndex), getPlayerIndex(destIndex), prop);
        }

        private Action obj;


        public void waitSceond(Action obj, float time)
        {
            this.obj = obj;
            Invoke("callBack", time);
        }

        public void cancelWaitSconed()
        {
            CancelInvoke("callBack");
        }


        public void callBack()
        {
            if (obj != null)
            {
                obj();
                obj = null;
            }

            cancelWaitSconed();
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (b)
            {
                IMQuickMsgManager.game_type = GamePlatform.CP_GAME;
                IMQuickMsgManager.quickback = playIMQuickMsg;
                IMQuickMsgManager.textback = playIMTextMsg;
                SoundManager.voiceBack = playVoice;
                IMQuickMsgManager.textinfo = QuickIMPanel.textInfo;
            }
        }

        public void call_back_record(string content)
        {
            if (content == null || content == "") return;
        }

        public void call_back_record_error(string content)
        {

        }
    }
}