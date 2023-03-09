using basef.im;
using cambrian.common;
using cambrian.game;
using platform.mahjong.guizhou;
using platform.spotred;
using platform.spotred.room;

namespace platform.poker
{
    /// <summary> 扑克 房间 </summary>
    public class PKRoomPanel : UnrealLuaPanel
    {
        /// <summary> 顶部显示 </summary>
        public PKTopView topView { get; protected set; }

        /// <summary> 底部显示 </summary>
        public PKDownView downView { get; protected set; }

        /// <summary> 游戏中房间显示 </summary>
        public PKGameView gameView { get; protected set; }

        /// <summary> 玩家信息 </summary>
        public PKHeadView headView { get; protected set; }

        /// <summary> 等待中房间显示 </summary>
        public PKWaitView waitView { get; protected set; }

        /// <summary> 道具视图 </summary>
        public PropView propView { get; protected set; }

        /// <summary> 录音视图 </summary>
        public VoiceView vview { get; protected set; }

        /// <summary> 交流视图 </summary>
        public ExpressionView expression { get; protected set; }

        /// <summary> 托管 </summary>
        public SelfRobotView selfRobotView { get; protected set; }
        /// <summary>
        /// 防暂离视图
        /// </summary>
        public FZLView fzlview { get; set; }


        public RuleView ruleView;

        /// <summary> 操作 </summary>
        public int operate { get; set; }

        protected PKRecvOperateTimer recvtimer;

        protected override void xinit()
        {
            base.xinit();

            if (content.Find("propview") != null)
            {
                propView = content.Find("propview").GetComponent<PropView>();
                propView.init();
            }

            if (content.Find("voiceview") != null)
            {
                vview = content.Find("voiceview").GetComponent<VoiceView>();
                vview.init();
                vview.setVisible(true);
            }

            if (content.Find("expression") != null)
            {
                expression = content.Find("expression").GetComponent<ExpressionView>();
                expression.init();
            }

            if (content.Find("robotview") != null)
            {
                selfRobotView = content.Find("robotview").GetComponent<SelfRobotView>();
                expression.init();
                selfRobotView.setVisible(false);
            }

            if (content.Find("fzl") != null)
            {
                fzlview = content.Find("fzl").GetComponent<FZLView>();
                fzlview.init();
            }

            if (content.Find("ruleview") != null)
            {
                ruleView = content.Find("ruleview").GetComponent<RuleView>();
                ruleView.init();
                ruleView.setVisible(false);
            }

            recvtimer = GetComponent<PKRecvOperateTimer>();
        }


        /// <summary>
        /// 获取gameview
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T getGameView<T>() where T : PKGameView
        {
            return (T)gameView;
        }


        protected override void xrefresh()
        {
            base.xrefresh();

            if (expression != null)
            {
                expression.refresh();
            }

            if (selfRobotView != null)
            {
                bool b = Room.room.getSelfMatchPlayer().isTrustee();
                selfRobotView.setVisible(b);
            }

            if (fzlview != null)
            {
                fzlview.setVisible(false);
            }
        }

        /// <summary> 设置托管 </summary>
        public virtual void setTrustee(int index, bool trustee)
        {
            headView.setTrustee(index, trustee);
            if (Room.room.indexOf() == index)
            {
                selfRobotView.setVisible(trustee);
            }
        }

        /// <summary> 播放声音 </summary>
        public virtual void playVoice(long userid, bool b)
        {
            if (headView.down.getPlayer().userid == userid)
            {
                expression.playVoice(PKGAME.DOWN, b);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expression.playVoice(PKGAME.RIGHT, b);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expression.playVoice(PKGAME.LEFT, b);
            }
        }

        /// <summary> 播放快捷聊天或表情 </summary>
        public virtual void playIMQuickMsg(IMQuickMsg msg)
        {
            long userid = msg.userid;
            if (headView.down.getPlayer().userid == userid)
            {
                expression.playQuickMsg(PKGAME.DOWN, msg);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expression.playQuickMsg(PKGAME.RIGHT, msg);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expression.playQuickMsg(PKGAME.LEFT, msg);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expression.playQuickMsg(PKGAME.TOP, msg);
            }
        }

        public void playIMTextMsg(IMTextMsg msg)
        {
            long userid = msg.userid;
            if (headView.down.getPlayer().userid == userid)
            {
                expression.setIMTextMsg(PKGAME.DOWN, msg);
            }
            else if (headView.right.getPlayer() != null && headView.right.getPlayer().userid == userid)
            {
                expression.setIMTextMsg(PKGAME.RIGHT, msg);
            }
            else if (headView.left.getPlayer() != null && headView.left.getPlayer().userid == userid)
            {
                expression.setIMTextMsg(PKGAME.LEFT, msg);
            }
            else if (headView.top.getPlayer() != null && headView.top.getPlayer().userid == userid)
            {
                expression.setIMTextMsg(PKGAME.TOP, msg);
            }
        }

        /// <summary>
        /// 显示防暂离
        /// </summary>
        public void showFullTimeView()
        {
            Room room = Room.room;
            if (!room.isType(Room.CLUB)&&!room.isType(Room.ARENA)) return;
            long fulltime = room.fulltime;
            if (room.roomRule.getGameNum() > -1)
            {
                fzlview.setVisible(false);
                return;

            }
            if (fulltime != 0)
            {
                int leaveTimeout = room.getRule().getIntAtribute("leaveTimeout");
                if (leaveTimeout == 0) return;
                fzlview.refresh();
                fzlview.setVisible(true);
                WXManager.getInstance().vibrator(1000);
            }
        }

        public override void setVisible(bool b)
        {
            base.setVisible(b);
            if (b)
            {
                IMQuickMsgManager.game_type = GamePlatform.PK_GAME;
                IMQuickMsgManager.quickback = playIMQuickMsg;
                IMQuickMsgManager.textback = playIMTextMsg;
                IMQuickMsgManager.textinfo = PKQuickIMPanel.textInfo;
                SoundManager.voiceBack = playVoice;
            }
        }

        /// <summary> 刷新准备时的显示 </summary>
        public virtual void refreshWaitView() { }

        /// <summary> 刷新游戏时时的显示 </summary>
        public virtual void refreshGameView() { }

        /// <summary> 增加操作 </summary>
        public virtual void addRecvOperate(PKBaseOperate operate) { }

        /// <summary> 清空接收操作队列 </summary>
        public virtual void clearBaseOperate() { }

        /// <summary> 清除玩家选择手牌 </summary>
        public virtual void claerSelectHands() { }

        /// <summary> 移除桌面缓存的牌 </summary>
        public virtual void removeDeskCard(int pos) { }

        /// <summary> 设置操作 </summary>
        public virtual void setOperateTimer(PKRecvOperateTimer.ActionBack action, ByteBuffer data)
        {
            recvtimer.setAction(action, data);
        }

        /// <summary> 显示动画</summary>
        public virtual void showPropAnimation(long useid, int destIndex, int prop)
        {
            int useIndex = Room.room.getPlayerIndex(useid);
            propView.showPropAnimation(PKGAME.GetIndex(useIndex), PKGAME.GetIndex(destIndex), prop);
        }

        protected virtual T GetComponent<T>(string path) where T : UnrealLuaSpaceObject
        {
            T obj = null;
            if (content.Find(path))
            {
                obj = content.Find(path).GetComponent<T>();
                obj.init();
            }
            return obj;
        }

        public static T GetPanel<T>() where T : PKRoomPanel
        {
            return UnrealRoot.root.getDisplayObject<T>();
        }

        public static PKRoomPanel Panel
        {
            get
            {
                return getPanel();
            }
            private set { }
        }

        public int[] tips
        {
            get
            {
                return getTips();
            }
            private set { }
        }

        public ArrayList<int> selectCard
        {
            get
            {
                return getSelectCard();
            }
            private set { }
        }

        private ArrayList<int> getSelectCard()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ: return DDZMatch.match.selectCard;
                case PKGAME.PDK: return PDKMatch.match.selectCard;
                case PKGAME.PDK_10: return PDKTenMatch.match.selectCard;
                case PKGAME.PDK_ANYUE: return PDKAnYueMatch.match.selectCard;
            }
            return new ArrayList<int>(0);
        }

        private int[] getTips()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ: return DDZTipsSeacher.getCardsByTip(selectCard.toArray());
                case PKGAME.PDK: return PDKTipsSeacher.getCardsByTip(selectCard.toArray());
                case PKGAME.PDK_10: return PDKTenTipsSeacher.getCardsByTip(selectCard.toArray());
                case PKGAME.PDK_ANYUE: return PDKAnYueTipsSeacher.getCardsByTip(selectCard.toArray());
            }
            return new int[0];
        }

        private static PKRoomPanel getPanel()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ: return UnrealRoot.root.getDisplayObject<PDDZRoomPanel>();
                case PKGAME.PDK: return UnrealRoot.root.getDisplayObject<PPDKRoomPanel>();
                case PKGAME.PDK_10: return UnrealRoot.root.getDisplayObject<PDKTenRoomPanel>();
                case PKGAME.PDK_ANYUE: return UnrealRoot.root.getDisplayObject<PDKAnYueRoomPanel>();
            }
            return null;
        }

        public static PKRoomPanel CheckPanel()
        {
            switch (PKGAME.RULESID)
            {
                case PKGAME.DDZ: return UnrealRoot.root.checkDisplayObject<PDDZRoomPanel>();
                case PKGAME.PDK: return UnrealRoot.root.checkDisplayObject<PPDKRoomPanel>();
                case PKGAME.PDK_10: return UnrealRoot.root.checkDisplayObject<PDKTenRoomPanel>();
                case PKGAME.PDK_ANYUE: return UnrealRoot.root.checkDisplayObject<PDKAnYueRoomPanel>();
                default: return null;
            }
        }
    }
}
