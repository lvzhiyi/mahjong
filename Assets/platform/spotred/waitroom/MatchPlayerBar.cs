using basef.im;
using basef.player;
using cambrian.common;
using cambrian.uui;
using UnityEngine;
using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    public class MatchPlayerBar:PlayerBar
    {
        protected bool ready;

        protected UnrealLuaSpaceObject readied;
        /// <summary>
        /// 提醒文案
        /// </summary>
        [HideInInspector]
        public string gpsInfo;
        /// <summary>
        /// 快捷聊天
        /// </summary>
        protected MatchIMQuickMSgBar quickmsg;
        /// <summary>
        /// 表情
        /// </summary>
        protected MatchIMQuickMSgBar emoji;
        /// <summary>
        /// 性别
        /// </summary>
        protected int sex;

        ///福数
        protected UnrealTextField fushu;
        /// <summary>
        /// 离线
        /// </summary>
        protected Image offline;
        /// <summary>
        /// 离线时间显示
        /// </summary>
        protected Text offlinetime;
        /// <summary>
        /// 离线时间
        /// </summary>
        protected long time;

        protected long temp;
        /// <summary>
        /// 机器人 状态
        /// </summary>
        protected Image robotstauts;
        /// <summary>
        /// 飘图标
        /// </summary>
        protected Image piao;

        [HideInInspector] public MatchPlayer matchplayer;

        protected SpritesList point_icon;

        protected override void xinit()
        {
            base.xinit();
            if (this.transform.Find("readied") != null)
                this.readied = this.transform.Find("readied").GetComponent<UnrealLuaSpaceObject>();
            if (this.transform.Find("quickmsg") != null)
            {
                this.quickmsg = this.transform.Find("quickmsg").GetComponent<MatchIMQuickMSgBar>();
                if (quickmsg != null)
                    this.quickmsg.init();
            }

            if (this.transform.Find("emoji") != null)
            {
                this.emoji = this.transform.Find("emoji").GetComponent<MatchIMQuickMSgBar>();
                if (this.emoji != null)
                    this.emoji.init();
            }

            if (this.transform.Find("fanshu") != null)
            {
                this.fushu = this.transform.Find("fanshu").GetComponent<UnrealTextField>();
            }

            if (this.transform.Find("offline") != null)
            {
                this.offline = this.transform.Find("offline").GetComponent<Image>();
            }

            if (this.transform.Find("robotstatus") != null)
            {
                this.robotstauts = this.transform.Find("robotstatus").GetComponent<Image>();
                this.robotstauts.gameObject.SetActive(false);
            }

            if (this.transform.Find("point_icon") != null)
            {
                this.point_icon = this.transform.Find("point_icon").GetComponent<SpritesList>();
            }

            if (transform.Find("piao") != null)
            {
                this.piao = transform.Find("piao").GetComponent<Image>();
            }
            if (this.transform.Find("offlinetime") != null)
            {
                this.offlinetime = this.transform.Find("offlinetime").GetComponent<Text>();
            }
        }


        public virtual void setPlayer(MatchPlayer player)
        {
            this.ready = player.isReady();
            this.sex = player.player.sex;
            long score = 0;
            if (Room.room != null)
            {
                SpotRedCount count = Room.room.getSpotRedCount(player.userid);

                if (count != null)
                    score = count.score;
            }

            if (Room.room != null && Room.room.isType(Room.ARENA))
            {
                score = Room.room.getSpotRedCount(player.userid).score;
            }
            else
            {
                point.setVisible(true);
                if (point_icon != null)
                    point_icon.setVisible(true);
            }

            this.setPlayer(player.player, score);
            this.matchplayer = player;

            if (offline != null)
            {
                if (!player.isTrustee())
                    offline.gameObject.SetActive(!player.player.hasStatus(SimplePlayer.STATUS_ONLINE));
            }

            if (point_icon != null)
            {
                point_icon.ShowIndex(0);
                if (Room.room != null && (Room.room.isType(Room.JBC) || Room.room.isType(Room.ARENA)
                                          ))
                    point_icon.ShowIndex(1);
            }

            if (this.robotstauts != null) this.robotstauts.gameObject.SetActive(player.isTrustee());
        }

        /// <summary>
        /// 是否显示托管状态
        /// </summary>
        /// <param name="b"></param>
        public virtual void isShowRobotstauts(bool b)
        {
            robotstauts.gameObject.SetActive(b);
        }

        /// <summary>
        /// 是否显示飘
        /// </summary>
        /// <param name="b"></param>
        public void isShowPiao(bool b)
        {
            this.piao.gameObject.SetActive(b);
        }

        public void setFushu(int fushu)
        {
            if(this.fushu!=null)
                this.fushu.text ="福数:"+fushu;
        }

        public virtual void setIMQuickMsg(IMQuickMsg msg)
        {
            msg.sex = this.sex;
            if (msg.type == IMQuickMsg.TYPE_TEXT)
            {
                quickmsg.setIMQuickMsg(msg);
                quickmsg.refresh();
                quickmsg.setVisible(true);
            }
            else
            {
                emoji.setIMQuickMsg(msg);
                emoji.refresh();
                emoji.setVisible(true);
            }
        }

        public virtual void setIMTextMsg(IMTextMsg msg)
        {
            this.quickmsg.showIMTextMsg(msg);
            this.quickmsg.setVisible(true);
        }

        protected bool isoffLine = false;

        protected override void xrefresh()
        {
            base.xrefresh();
            if (this.readied != null)
                this.readied.setVisible(this.ready);
            if (this.quickmsg != null)
                this.quickmsg.setVisible(false);
            if (this.emoji != null)
                this.emoji.setVisible(false);

            if (offlinetime != null)
            {
                offlinetime.gameObject.SetActive(!matchplayer.player.hasStatus(1));
            }

           
            if (offlinetime != null)
            {
                if (!matchplayer.player.hasStatus(1))
                {
                        isoffLine = true;
                }
                else
                {
                    isoffLine = false;
                    matchplayer.offlinetime = 0;
                    offlinetime.text = "";
                }
            }
        }

        
        public virtual void setOffline(bool off)
        {
            offline.gameObject.SetActive(off);
            offlinetime.gameObject.SetActive(off);

           
            if (offlinetime != null )
            {
                
                if (!matchplayer.player.hasStatus(1))
                {
                        isoffLine = true;
                }
                else
                {
                    isoffLine = false;
                    matchplayer.offlinetime = 0;
                    offlinetime.text = "";
                }
            }
        }
        protected long hour, minute, second;
        string str;
        protected void offlinetimeback()
        {
            if (offlinetime != null)
            {

                time = matchplayer.offlinetime;
                temp = TimeKit.currentTimeMillis - time;
                hour = MathKit.floor(temp / 3600000);
                minute = MathKit.floor((temp % 3600000) / 60000);
                second = MathKit.floor((temp % 3600000 % 60000) / 1000);
                //offlinetime.text=TimeKit.getCountdown(temp);

                if (second >= 60)
                {
                    second = 0;
                    minute = minute + 1;
                }
                if (minute >= 60)
                {
                    minute = 0;
                    hour = hour + 1;
                }
                if (hour < 10)
                    str = "0" + hour + ":";
                else
                    str = hour + ":";
                if (minute < 10)
                    str = str + "0" + minute+":";
                else
                    str = str + minute + ":";
                if (second < 10)
                    str = str + "0" + second;
                else
                    str = str + second;
                offlinetime.text = str;
                
            }
        }


        protected override void xupdate()
        {
            if(isoffLine)
                offlinetimeback();

        }
    }
}
