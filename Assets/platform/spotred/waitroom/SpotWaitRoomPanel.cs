using basef.im;
using cambrian.common;
using cambrian.game;
using platform.mahjong.guizhou;
using platform.spotred.room;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 等待房间
    /// </summary>
    public class SpotWaitRoomPanel:UnrealLuaPanel
    {
        /// <summary>
        /// 房间编号
        /// </summary>
        UnrealTextField roomindex;
        /// <summary>
        /// 局数
        /// </summary>
        UnrealTextField jushu;

        /// <summary>
        /// 准备
        /// </summary>
        UnrealTextButton ready;
        /// <summary>
        /// 分享
        /// </summary>
        //UnrealScaleButton share;
        /// <summary>
        /// 邀请/换桌
        /// </summary>
        UnrealTextButton inviteOrjoin;
        /// <summary>
        /// 录音视图
        /// </summary>
        [HideInInspector] public VoiceView vview;
        /// <summary>
        /// 玩家显示对象(根据人数来显示位置的个数)
        /// </summary>
        MatchPlayerBar[] players;
        /// <summary>
        /// 预制件上4个位置
        /// </summary>
        private MatchPlayerBar[] all_players;
        /// <summary>
        /// 当前时间
        /// </summary>
        private UnrealTextField time;
        /// <summary>
        /// 电量
        /// </summary>
        UnrealProgressBar dian;
        /// <summary>
        /// 4G网络
        /// </summary>
        Transform xinhao;
        /// <summary>
        /// wifi网络
        /// </summary>
        Transform wifi;

        [HideInInspector] public RuleView ruleview;
        /// <summary>
        /// 桌面背景
        /// </summary>
        private Image deskbg;

        PropView propView;

        /// <summary>
        /// 防暂离
        /// </summary>
        public FZLView fzlView { get; set; }

        protected override void xinit()
        {
            base.xinit();
            this.deskbg = this.transform.Find("Canvas").Find("bg1").GetComponent<Image>();
            this.roomindex = this.top.Find("roomindex").GetComponent<UnrealTextField>();
            this.dian = this.top.Find("dianliang").GetComponent<UnrealProgressBar>();
            this.dian.init();
            this.xinhao = this.top.Find("xinhao");
            this.wifi = this.top.Find("wifi");
            this.jushu = this.top.Find("jueshu").GetComponent<UnrealTextField>();

            //this.share = content.Find("button").Find("sharen").GetComponent<UnrealScaleButton>();
            this.ready = content.transform.Find("ready").GetComponent<UnrealTextButton>();
            this.vview = content.Find("voiceview").GetComponent<VoiceView>();

            inviteOrjoin = content.Find("button").Find("inviteorjoin").GetComponent<UnrealTextButton>();

            this.vview.init();
            this.vview.setVisible(true);

            this.resizeDelta(new Margin());

            this.all_players = new MatchPlayerBar[4];
            all_players[0] = content.Find("bar_b").GetComponent<MatchPlayerBar>();
            all_players[0].init();
            all_players[1] = content.Find("bar_r").GetComponent<MatchPlayerBar>();
            all_players[1].init();
            all_players[2] = content.Find("bar_t").GetComponent<MatchPlayerBar>();
            all_players[2].init();
            all_players[3] = content.Find("bar_l").GetComponent<MatchPlayerBar>();
            all_players[3].init();

            this.time = content.Find("top").Find("time").GetComponent<UnrealTextField>();

            this.ruleview = content.Find("ruleview").GetComponent<RuleView>();
            this.ruleview.init();
            this.ruleview.setVisible(false);

            this.propView = this.content.Find("propview").GetComponent<PropView>();
            this.propView.init();

            if (content.Find("fzl") != null)
            {
                fzlView = content.Find("fzl").GetComponent<FZLView>();
                fzlView.init();
            }
        }


        long lasttime = 0;


        protected override void xupdate()
        {
            this.time.text = TimeKit.format(TimeKit.currentTimeMillis, "HH:mm:ss");

            if (TimeKit.currentTimeMillis - this.lasttime < 5 * 1000) return;
            this.lasttime = TimeKit.currentTimeMillis;

            WXManager.getInstance().getCurBattery("Root", "showDianliangR");
            WXManager.getInstance().getNetWorkStrength("Root", "showXinhaoR");
        }


        /// <summary>
        /// 设置比赛玩家状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="online"></param>
        public void setMathPlayerStatus(int index, bool online)
        {
            int pos = ((index + this.players.Length) - Room.room.indexOf()) % this.players.Length;
            this.players[pos].setOffline(!online);
        }


        /// <summary>
        /// 显示电量
        /// </summary>
        /// <param name="str"></param>
        public void showDianliang(string str)
        {
            int dian = StringKit.parseInt(str);
            this.dian.setValue((float)dian / 100);
        }

        /// <summary>
        /// 设置信号强度  1-4
        /// </summary>
        /// <param name="num"></param>
        public void showXinhao(string str)
        {
            int[] xinhaos = StringKit.parseInts(str);
            if (xinhaos[1] == -1)
                return;
            int num = xinhaos[1] + 1;
            if (num > 4)
                num = 4;

            Transform xh = null;


            if (xinhaos[0] == 1)//wifi
            {
                this.wifi.gameObject.SetActive(true);
                this.xinhao.gameObject.SetActive(false);
                xh = this.wifi;
            }
            else
            {
                this.wifi.gameObject.SetActive(false);
                this.xinhao.gameObject.SetActive(true);
                xh = this.xinhao;
            }

            for (int i = 4; i > 0; i--)
            {
                xh.Find("num" + i).gameObject.SetActive(false);
                if (i == num)
                {
                    xh.Find("num" + i).gameObject.SetActive(true);
                    break;
                }
            }
        }

        public void playVoice(long userid, bool b)
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                MatchPlayerBar bar = this.players[i];
                if (bar.getPlayer() != null && bar.getPlayer().userid == userid)
                {
                    bar.showVoice(b);
                }
            }
        }
        private MatchPlayerBar getMatchPlayerBar(long userid)
        {
            for (int i = 0; i < this.players.Length; i++)
            {
                MatchPlayerBar bar = this.players[i];
                if (bar.getPlayer() == null) continue;
                if (bar.getPlayer().userid == userid) return bar;
            }
            return null;
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
            propView.showPropAnimation(RoomPanel.getPlayerIndex(useIndex), RoomPanel.getPlayerIndex(destIndex), prop);
        }

        /// <summary>
        /// 播放快捷聊天或表情
        /// </summary>
        /// <param name="msg"></param>
        public void playIMQuickMsg(IMQuickMsg msg)
        {
            MatchPlayerBar bar = getMatchPlayerBar(msg.userid);
            if (bar == null) return;
            bar.setIMQuickMsg(msg);
        }

        public void playIMTextMsg(IMTextMsg msg)
        {
            MatchPlayerBar bar = getMatchPlayerBar(msg.userid);
            if (bar == null) return;
            bar.setIMTextMsg(msg);
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

        protected override void xrefresh()
        {
            Room room = Room.room;
            if(room.isType(Room.CLUB|Room.JBC))
            {
                this.jushu.text = "底分:" + room.roomRule.rule.getBet();
            }
            else
            {
                this.jushu.text = "第"+ room.roomRule.getNowPalyNum()+"局";
            }
            this.roomindex.text = "房间号:"+ room.getRoomIndex();

            this.wifi.gameObject.SetActive(false);
            this.xinhao.gameObject.SetActive(false);

            this.ready.setVisible(room.roomRule.getGameNum() == -1);

            int index = room.indexOf();

            this.ready.setVisible(!room.players[index].isReady());

            setShowPostion();

            for (int i = 0; i < this.players.Length; i++)
            {
                MatchPlayerBar bar = this.players[i];
                MatchPlayer player = room.players[(i + index) % this.players.Length];
                if (player == null)
                {
                    bar.setVisible(false);
                }
                else
                {
                    bar.setPlayer(player);
                    bar.refresh();
                    bar.setVisible(true);
                }
            }

            showInviteOrJoin();

            showFullTimeView();

            int leaveTimeOut = room.getRule().getIntAtribute("leaveTimeout");
            if (leaveTimeOut == 0 || room.roomRule.getGameNum() > 0) return;
            long fulltime = room.fulltime;
            if (fulltime == 0)
            {
                fzlView.setVisible(false);
                ready.setVisible(false);
            }
            else
            {
                ready.setVisible(!room.players[room.indexOf()].isReady());
            }
        }

        /// <summary>
        /// 显示邀请换桌按钮
        /// </summary>
        public void showInviteOrJoin()
        {
            if (Room.room.roomRule.getGameNum() == -1 && (Room.room.isType(Room.CLUB)|| Room.room.isType(Room.ARENA)))
            {
                inviteOrjoin.setVisible(true);
            }
            else
            {
                inviteOrjoin.setVisible(false);
            }
        }

          /// <summary>
        /// 显示防暂离
        /// </summary>
        public void showFullTimeView()
        {
            fzlView.setVisible(false);
            Room room = Room.room;
            if (!room.isType(Room.CLUB)&& !room.isType(Room.ARENA))
            {
                fzlView.setVisible(false);
                return;
            } 
            long fulltime = room.fulltime;
            if (room.roomRule.getGameNum()>-1||fulltime==0)
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

        /// <summary>
        /// 设置需要显示玩家位置
        /// </summary>
        private void setShowPostion()
        {
            for (int i = 0; i < all_players.Length; i++)
            {
                this.all_players[i].setVisible(false);
            }

            switch (Room.room.roomRule.rule.playerCount)
            {
                case 4:
                    this.players = this.all_players;
                    break;
                case 3:
                    this.players=new MatchPlayerBar[3];
                    this.players[0] = this.all_players[0];
                    this.players[1] = this.all_players[1];
                    this.players[2] = this.all_players[3];
                    break;
                case 2:
                    this.players = new MatchPlayerBar[2];
                    this.players[0] = this.all_players[0];
                    this.players[1] = this.all_players[2];
                    break;
            }
        }



        public void call_back_room_record(string content)
        {
            if (content == null || content == "") return;
            //解码
            byte[] data = System.Convert.FromBase64String(content);
        }

        public void call_back_room_record_error(string content)
        {

        }
    }
}
