using basef.player;
using cambrian.common;
using cambrian.uui;
using platform.mahjong;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 解散房间进度界面
    /// </summary>
    public class DisbandPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 同意解散按钮
        /// </summary>
        UnrealScaleButton ok;

        /// <summary>
        /// 拒绝解散按钮
        /// </summary>
        UnrealScaleButton cancel;

        /// <summary>
        /// 投票数据
        /// </summary>
        Voting voting;

        /// <summary>
        /// 界面中部
        /// </summary>
        Transform discenter;

        /// <summary>
        /// 玩家信息容器
        /// </summary>
        UnrealTableContainer container;

        /// <summary>
        /// 时
        /// </summary>
        UnrealTextField hour;

        /// <summary>
        /// 发起申请者
        /// </summary>
        //UnrealTextField disbander;

        private Text title;

        //private Text text;

        //private Text tips;

        protected override void xinit()
        {
            discenter = transform.Find("Canvas").transform.Find("centerview");
            container = discenter.transform.Find("container").GetComponent<UnrealTableContainer>();
            container.init();
            ok = transform.Find("Canvas").Find("ok").GetComponent<UnrealScaleButton>();
            cancel = transform.Find("Canvas").Find("cancel").GetComponent<UnrealScaleButton>();
            hour = discenter.transform.Find("time").GetComponent<UnrealTextField>();
            //disbander = transform.Find("Canvas").Find("name").GetComponent<UnrealTextField>();
            title = transform.Find("Canvas").Find("title").GetComponent<Text>();
            //text = transform.Find("Canvas").Find("text").GetComponent<Text>();
            //tips = discenter.Find("tishi").Find("text").GetComponent<Text>();
        }

        public void clear()
        {
            this.ok.gameObject.SetActive(true);
            this.cancel.gameObject.SetActive(true);
        }


        public void recvInfo(Voting voting)
        {
            this.voting = voting;
            if (voting.type == Voting.VOTE_DISBAND)
            {
                title.text = "申请解散";
                //tips.text = "(超过5分钟未做选择，则默认同意)";
                //text.gameObject.SetActive(true);
            }
            else
            {
                title.text = "快速开始";
                //tips.text = "(超过1分钟未做选择，则默认拒绝)";
                //text.gameObject.SetActive(false);
                UnrealRoot.root.getDisplayObject<MJSelectFSPanel>().setVisible(false);
            }

            clear();
        }

        public void hiddenBtn()
        {
            ok.gameObject.SetActive(false);
            cancel.gameObject.SetActive(false);
        }

        long lasttime;


        protected override void xupdate()
        {
            if (TimeKit.currentTimeMillis - this.lasttime >= 1000)
            {
                lasttime = TimeKit.currentTimeMillis;
                show();
            }
        }

        private string getMsg(string name)
        {
            CharBuffer buffer = new CharBuffer();
            buffer.append("玩家【").append(name).append("】申请").append(voting.justment.playerCount).append("人")
                .append(voting.justment.getCardTypeCount()).append("房,立即开局,请等待其他玩家确认");
            return buffer.getString();
        }

        private Room room;

        public void show()
        {
            string t = TimeKit.getCountdown(voting.getOvertime() - TimeKit.currentTimeMillis);
            hour.text = t+"后自动同意";
            room = Room.room;
            this.container.resize(room.getRoomRealPlayerCount());

            int index = 0;
            for (int i = 0; i < Room.room.players.Length; i++)
            {
                MatchPlayer player = Room.room.players[i];
                if (player == null) continue;
                DisbanderBar bar = (DisbanderBar) this.container.bars[index];
                index++;
                //if (i == voting.sponsor)
                //{
                //    string n = StringKit.substring(player.player.name, 0, 6);
                //    if (voting.type == Voting.VOTE_DISBAND)
                //        disbander.text = n;
                //    else
                //    {
                //        disbander.text = getMsg(n);
                //    }
                //}

                bar.setMatchPlayer(player);
                bar.setSponsor(i == voting.sponsor);
                bar.setVoterStatus(voting.voterStatus[i]);
                bar.refresh();

                if (voting.type == Voting.VOTE_ADJUST && voting.isRefused(i))
                {
                    clear();
                    setVisible(false);
                    Alert.autoShow("【"+StringKit.substring(player.player.name, 0, 6)+"】未同意立即开局,建议等等其他小伙伴再开局!");
                    break;
                }

                container.resizeDelta();
                container.relayout();

                if (!voting.isWaiting(i))
                {
                    if (player.userid == Player.player.userid)
                        hiddenBtn();
                }
            }
        }
    }
}
