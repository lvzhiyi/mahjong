using basef.im;
using basef.rule;
using cambrian.common;
using cambrian.game;
using cambrian.uui;
using platform.mahjong.guizhou;
using platform.spotred;
using platform.spotred.room;
using System;
using UnityEngine;

namespace platform.mahjong
{
    /// <summary>
    /// 麻将房间面板
    /// </summary>
    public class MahJongPanel:UnrealLuaPanel
    {
        static MahJongPanel panel;

        public static MahJongPanel getPanel()
        {
            if (Room.room == null)
            {
                Alert.show("not init GamePanel");
                return null;
            }
            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);

            switch (gametype)
            {
                case GamePanelData.MY_GAME:
                    return UnrealRoot.root.getDisplayObject<MahjongRoomPanel>();
                case GamePanelData.AY_GAME:
                    return UnrealRoot.root.getDisplayObject<AYMJRoomPanel>();
               
            }
            return null;
        }

        public static MahJongPanel CheckPanel()
        {

            if (Room.room == null)
            {
                Alert.show("not init GamePanel");
                return null;
            }
            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);

            switch (gametype)
            {
              
                case GamePanelData.MY_GAME:
                    return UnrealRoot.root.checkDisplayObject<MahjongRoomPanel>();
                case GamePanelData.AY_GAME:
                    return UnrealRoot.root.checkDisplayObject<AYMJRoomPanel>();
            }

            return null;
        }



        public static void showPanel()
        {
            int gametype = GamePanelData.getInstance().getGamePanel(Room.room.getRule().sid);

            switch (gametype)
            {
                case GamePanelData.MY_GAME:
                    UnrealRoot.root.showPanel<MahjongRoomPanel>();
                    break;
                case GamePanelData.AY_GAME:
                    UnrealRoot.root.showPanel<AYMJRoomPanel>();
                    break;
            }
        }

        /// <summary>
        /// 下,右,上,左
        /// </summary>
        public const int LOC_DOWN = 0, LOC_RIGHT = 1, LOC_TOP = 2, LOC_LEFT = 3;
        /// <summary>
        /// 顶部的视图
        /// </summary>
        [HideInInspector] public MJTopBaseInfoView topView;
        /// <summary>
        /// 录音视图
        /// </summary>
        VoiceView vview;
        /// <summary>
        /// 道具效果视图
        /// </summary>
        PropView propView { set; get; }
        /// <summary>
        /// 短语，表情，文字视图
        /// </summary>
        ExpressionView expressionView;
        /// <summary>
        /// 头像信息
        /// </summary>
        protected MJHeadView headView;
        /// <summary>
        /// 托管界面
        /// </summary>
        protected SelfRobotView selfRobotView;
        /// <summary>
        /// 房间id
        /// </summary>
        protected UnrealTextField roomindex;
        /// <summary>
        /// 桌面背景
        /// </summary>
        private SpritesList deskbg;
        /// <summary>
        /// 消息执行队列
        /// </summary>
        MJBaseRecvOperateTimer recvtimer;
        /// <summary>
        /// 自己的操作
        /// </summary>
        [HideInInspector] int selfOperateStatus;
        /// <summary>
        /// 等待中视图
        /// </summary>
        MJBaseWaitView waitView;
        /// <summary>
        /// 动画视图
        /// </summary>
        MJMovieClipView movieClipView;
        /// <summary>
        /// 游戏中
        /// </summary>
        [HideInInspector] public MJBaseGameView gameView;
        /// <summary>
        /// 显示最后一个人打牌的坐标
        /// </summary>
        protected Transform lastSendCard;

        /// <summary>
        /// 防暂离视图
        /// </summary>
        public FZLView fzlView { get; set; }
        /// <summary>
        /// 详细规则
        /// </summary>
        public RuleView ruleview;



        protected override void xinit()
        {
            base.xinit();
            if (content.Find("voiceview") != null)
            {
                vview = this.content.Find("voiceview").GetComponent<VoiceView>();
                vview.init();
                vview.setVisible(true);
            }

            if (content.Find("propview") != null)
            {
                propView = this.content.Find("propview").GetComponent<PropView>();
                propView.init();
            }

            if (content.Find("expressionview") != null)
            {
                expressionView = this.content.Find("expressionview").GetComponent<ExpressionView>();
                expressionView.init();
            }

            if (content.Find("robotview") != null)
            {
                selfRobotView = content.Find("robotview").GetComponent<SelfRobotView>();
                selfRobotView.init();
                selfRobotView.setVisible(false);
            }

            if (content.Find("ruleview") != null)
            {
                ruleview = content.Find("ruleview").GetComponent<RuleView>();
                ruleview.init();
                ruleview.setVisible(false);
            }
            

            deskbg = content.Find("bg").GetComponent<SpritesList>();

            headView = content.transform.Find("heads").GetComponent<MJHeadView>();
            headView.init();

            topView = content.transform.Find("top").GetComponent<MJTopBaseInfoView>();
            topView.init();
            roomindex = topView.transform.Find("roomindex").GetComponent<UnrealTextField>();
            roomindex.init();

            recvtimer = GetComponent<MJBaseRecvOperateTimer>();

            waitView = content.transform.Find("wait").GetComponent<MJBaseWaitView>();
            waitView.init();
            gameView = content.transform.Find("game").GetComponent<MJBaseGameView>();
            gameView.init();
            
            movieClipView = content.transform.Find("movieclip").GetComponent<MJMovieClipView>();
            movieClipView.init();

            lastSendCard = content.Find("game").Find("lastSendCard");

            if (content.Find("fzl") != null)
            {
                fzlView = content.Find("fzl").GetComponent<FZLView>();
                fzlView.init();
            }
        }

        protected override void xrefresh()
        {
            panel = this;
            headView.refresh();
            topView.refresh();
            roomindex.text = Room.room.getRoomIndex().ToString();
            if (expressionView != null)
                expressionView.refresh();

            if (selfRobotView != null)
            {
                bool b = Room.room.getSelfMatchPlayer().isTrustee();
                selfRobotView.setVisible(b);
            }
            lastSendCard.gameObject.SetActive(false);

            if (fzlView != null)
                fzlView.setVisible(false);
        }

        /// <summary>
        /// 获取gameview
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T getGameView<T>() where T : MJBaseGameView
        {
            return (T)gameView;
        }

        public virtual void refreshSingleDingQue(int index)
        {
            gameView.getDQView().refreshSingleStatus(getPlayerIndex(index));
        }

        public virtual void showHuanOrDing(int index)
        {
            gameView.getMjIndexCenterView().showHuanOrDing(getPlayerIndex(index));
        }

        /// <summary>
        /// 刷新玩家的手牌(排序好)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public virtual void refreshHandCard(int index, TingCardsInfo[] ting, bool isMoveMoCard, bool isAll = false)
        {
           
        }

        /// <summary>
        /// 刷新换三张
        /// </summary>
        /// <param name="index"></param>
        public virtual void refreshHSZHandCard(int index,int[] hsz, Action action)
        {

        }

        /// <summary>
        /// 刷新单个人的手牌(只是刷新，不会有定缺的牌，置灰显示这些)
        /// </summary>
        /// <param name="index"></param>
        public virtual void refreshSingleHandCard(int index, int[] cards = null)
        {
           
        }

        /// <summary>
        /// 刷新定缺状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public virtual void refreshDingQue(int index, bool isAll = false)
        {
           
        }

        public virtual void refreshDisAbleView(int index, bool isAll = false)
        {
           
        }

        /// <summary>
        /// 刷新玩家的固定牌区
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isAll">是否是全部刷新</param>
        public virtual void refreshFixedCard(int index, bool isAll = false)
        {
            
        }


        /// <summary>
        /// 刷新桌布
        /// </summary>
        public virtual void refreshDeskTop()
        {
            int index = MJSettingManager.getDeskTop();
            deskbg.ShowIndex(index, false);
        }

        public void setTrustee(int index, bool trustee)
        {
            headView.setTrustee(getPlayerIndex(index), trustee);
            if (Room.room.indexOf() == index)
            {
                selfRobotView.setVisible(trustee);
            }
        }

        public virtual void showIPView()
        {
            
        }

        /// <summary>
        /// 获取头像视图
        /// </summary>
        /// <returns></returns>
        public MJHeadView getHeadView()
        {
            return headView;
        }

        /// <summary>
        /// 增加操作
        /// </summary>
        /// <param name="operate"></param>
        public void addRecvOperate(MJBaseOperate operate)
        {
            recvtimer.addOperate(operate);
        }

        public void setOperateTimer(MJBaseRecvOperateTimer.ActionBack action, ByteBuffer data)
        {
            recvtimer.setAction(action, data);
        }

        /// <summary>
        /// 清空接收操作队列
        /// </summary>
        public void clearBaseOperate()
        {
            if (this.recvtimer != null)
                this.recvtimer.clearBaseOperate();
        }

        public virtual void setOperate(int selfOperateStatus)
        {
            this.selfOperateStatus = selfOperateStatus;
        }

        public int getOperate()
        {
            return selfOperateStatus;
        }

        #region 等待中视图
        /// <summary>
        /// 显示等待中的视图
        /// </summary>
        public void showWaitView()
        {
            headView.waitViewPostion();
            headView.showMatchPlayers(-1);
            movieClipView.setVisible(false);
            waitView.refresh();
            waitView.setVisible(true);
            gameView.setVisible(false);
            

            showFullTimeView();
        }

        /// <summary>
        /// 显示防暂离
        /// </summary>
        public void showFullTimeView()
        {
            Room room = Room.room;
            if (!room.isType(Room.CLUB)&& !room.isType(Room.ARENA)) return;
            long fulltime = room.fulltime;
            if (room.roomRule.getGameNum()>-1)
            {
                fzlView.setVisible(false);
                return;
            }
            if (fulltime != 0)
            {
                int leaveTimeout = room.getRule().getIntAtribute("leaveTimeout");
                if (leaveTimeout == 0) return;
                fzlView.refresh();
                fzlView.setVisible(true);
                WXManager.getInstance().vibrator(1000);
            }
        }

        public MJBaseWaitView getWaitView()
        {
            return waitView;
        }

        public void setPlayerReady(int index)
        {
            waitView.setReady(getPlayerIndex(index), true);
        }

        #endregion

        #region 比赛视图
        /// <summary>
        /// 显示游戏中视图
        /// </summary>
        public virtual void showGameView()
        {
            headView.gameViewPostion();
            waitView.setVisible(false);
            if (selfRobotView != null)
            {
                bool b = false;
                if (Room.room.getSelfMatchPlayer() != null)
                    b = Room.room.getSelfMatchPlayer().isTrustee();
                selfRobotView.setVisible(b);
            }
        }

        /// <summary>
        /// 显示打的牌（这里打的牌会慢慢的消息）
        /// </summary>
        /// <param name="card"></param>
        /// <param name="index"></param>
        public void showPlayerCard(int card, int index)
        {
            gameView.showPlayerCardView(card, getPlayerIndex(index));
        }

        /// <summary>
        /// 显示最后一个人打牌的坐标
        /// </summary>
        /// <param name="index"></param>
        public void showPlayerLastCard(int index)
        {
            MJBaseDisAbleBar card = gameView.getDisAbleView().getLastBar(getPlayerIndex(index));
            if (card == null)
            {
                this.lastSendCard.gameObject.SetActive(false);
                return;
            }
            Vector3 p = card.transform.position;
            p.z = this.lastSendCard.position.z;
            p.y = card.transform.position.y + card.getHeight() / 100.0f;

            this.lastSendCard.position = p;
            this.lastSendCard.gameObject.SetActive(true);
        }

        /// <summary>
        /// 刷新每个玩家换牌的方向显示
        /// </summary>
        public virtual void refreshHSZOrDQDerection(int status)
        {
            int len = Room.room.getPlayerCount();
            MJPlayerCards playerCard;
            for (int i = 0; i < len; i++)
            {
                playerCard = MJMatch.match.getPlayerCardIndex<MJPlayerCards>(i);
                if (StatusKit.hasStatus(playerCard.status, status))
                {
                    showHuanOrDing(i);
                }
            }
        }

        /// <summary>
        /// 刷新牌的数量
        /// </summary>
        public virtual void refreshCardNum()
        {
            
        }

        public void hideLastSendCard()
        {
            lastSendCard.gameObject.SetActive(false);
        }

        public void refreshSelectCard(int card)
        {
            int len = Room.room.players.Length;
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                gameView.getDisAbleView().refreshExistCard(getPlayerIndex(index), card);
                index = getIndex(index);
            }
        }
        /// <summary>
        /// 显示倒计时
        /// </summary>
        int time = 30;
        public void showCountTime(int index)
        {
            if (Room.room != null)
            {
                Rule rule = Room.room.roomRule.rule;
                if (rule.getTrusteeship() == 1)//代表托管
                {
                    time = rule.getTrusteeTime() / 1000;
                }
            }
            gameView.getMjIndexCenterView().showRound(getPlayerIndex(index), time);
        }

        #endregion

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        public void playAnimation(int index, int type)
        {
            this.movieClipView.setVisible(true);
            switch (getPlayerIndex(index))
            {
                case LOC_DOWN:
                    this.movieClipView.playMovieClip(type, LOC_DOWN);
                    break;
                case LOC_RIGHT:
                    this.movieClipView.playMovieClip(type, LOC_RIGHT);
                    break;
                case LOC_TOP:
                    this.movieClipView.playMovieClip(type, LOC_TOP);
                    break;
                case LOC_LEFT:
                    this.movieClipView.playMovieClip(type, LOC_LEFT);
                    break;
            }
        }

        public void playIMTextMsg(IMTextMsg msg)
        {
            long userid = msg.userid;
            if (headView.bottom.getPlayer().userid == userid)
            {
                expressionView.setMJIMTextMsg(LOC_DOWN, msg,headView.bottom.transform.localPosition);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expressionView.setMJIMTextMsg(LOC_TOP, msg, headView.top.transform.localPosition);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expressionView.setMJIMTextMsg(LOC_RIGHT, msg, headView.right.transform.localPosition);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expressionView.setMJIMTextMsg(LOC_LEFT, msg, headView.left.transform.localPosition);
            }
        }

        /// <summary>
        /// 使用道具
        /// </summary>
        /// <param name="useid">使用者id</param>
        /// <param name="destIndex">目标索引</param>
        /// <param name="prop">道具id</param>
        public void showPropAnimation(long useid, int destIndex, int prop)
        {
            int useIndex = Room.room.getPlayerIndex(useid);
            propView.showPropAnimation(getPlayerIndex(useIndex), getPlayerIndex(destIndex), prop);
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
                expressionView.playMJQuickMsg(LOC_DOWN, msg,headView.bottom.transform.localPosition);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expressionView.playMJQuickMsg(LOC_TOP, msg, headView.top.transform.localPosition);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expressionView.playMJQuickMsg(LOC_RIGHT, msg, headView.right.transform.localPosition);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expressionView.playMJQuickMsg(LOC_LEFT, msg, headView.left.transform.localPosition);
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

        /// <summary>
        /// 获取相对于自己位置玩家的索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int getPlayerIndex(int index)
        {
            int len = Room.room.roomRule.rule.playerCount;
            int pos = ((index + len) - Room.room.indexOf()) % len;
            switch (len)
            {
                case 4:
                    break;
                case 3:
                    if (pos == 2)
                        pos = LOC_LEFT;
                    break;
                case 2:
                    if (pos == 1)
                        pos = LOC_TOP;
                    break;
            }
            return pos;
        }

        protected int getIndex(int index)
        {
            if (index == (Room.room.getPlayerCount() - 1))
                index = 0;
            else
                index++;
            return index;
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (b)
            {
                IMQuickMsgManager.game_type = GamePlatform.MJ_GAME;
                IMQuickMsgManager.quickback = playIMQuickMsg;
                IMQuickMsgManager.textback = playIMTextMsg;
                SoundManager.voiceBack = playVoice;
                IMQuickMsgManager.textinfo = MJQuickIMPanel.textInfo;
            }
        }
    }
}
