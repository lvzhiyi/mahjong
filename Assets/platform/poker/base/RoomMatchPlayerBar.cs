using basef.player;
using cambrian.common;
using platform.spotred;
using UnityEngine.UI;

namespace platform.poker
{
    public class RoomMatchPlayerBar : UnrealLuaSpaceObject
    {
        protected MatchPlayer player;

        protected Image uOnLine;

        protected Image uReady;

        protected UnrealTextField uName;

        protected UnrealTextField uPoint;

        protected PlayerHeadView uHead;

        //protected PKRobotAnimation uRobot;

        protected Image uRobot;


        /// <summary>
        /// 显示离线时间
        /// </summary>
        protected Text offlinetime;
        bool isoffLine;  //是否离线

        /// <summary>
        /// 离线时间
        /// </summary>
        protected long time;
        /// <summary>
        /// 玩家离线多久
        /// </summary>
        protected long temp;
        protected override void xinit()
        {
            if (transform.Find("uName") != null)
            {
                uName = transform.Find("uName").GetComponent<UnrealTextField>();
                uName.init();
            }
     
            if (transform.Find("uPoint") != null)
            {
                uPoint = transform.Find("uPoint").GetComponent<UnrealTextField>();
                uPoint.init();
            }

            if (transform.Find("uHead") != null)
            {
                uHead = transform.Find("uHead").GetComponent<PlayerHeadView>();
                uHead.init();
            }

            if (transform.Find("uReady") != null)
            {
                uReady = transform.Find("uReady").GetComponent<Image>();
            }

            if (transform.Find("uOnLine") != null)
            {
                uOnLine = transform.Find("uOnLine").GetComponent<Image>();
            }

            //if (transform.Find("uRobot") != null)
            //{
            //    uRobot = transform.Find("uRobot").GetComponent<PKRobotAnimation>();
            //}
            if (transform.Find("uRobot") != null)
            {
                uRobot = transform.Find("uRobot").GetComponent<Image>();
            }

            if (this.transform.Find("offlinetime") != null)
            {
                this.offlinetime = this.transform.Find("offlinetime").GetComponent<Text>();
            }
        }

        protected override void xrefresh()
        {
            setVisible(player.player != null);
            if (player.player == null) return;

            uReady.gameObject.SetActive(player.isReady());

            uHead.setData(player.player.head, player.userid);
            uHead.refresh();
            uHead.setVisible(true);

            updateUserName();
            updateOnline();
            updateSocre();
            updateRobot(player.isTrustee());

            updateOfflineTime();
        }
        protected virtual void updateOfflineTime()
        {
            if (offlinetime != null)
                offlinetime.gameObject.SetActive(!player.player.hasStatus(1));


            if (offlinetime != null)
            {
                //temp = TimeKit.currentTimeMillis - time;
                if (!player.player.hasStatus(1))
                {
                        isoffLine = true;
                    
                }
                else
                {
                    isoffLine = false;
                    player.offlinetime = 0;
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

                time = player.offlinetime;
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
                    str = str + "0" + minute + ":";
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
            if (isoffLine)
                offlinetimeback();

        }
        protected virtual void updateUserName()
        {
            if (!uName.gameObject.activeSelf)
                uName.setVisible(true);
            uName.text = player.player.name;
        }

        public virtual void updateRobot(bool isShow)
        {
            if (uRobot) uRobot.gameObject.SetActive(isShow);
        }

        protected virtual void updateOnline()
        {
            if (player.player.userid == Player.player.userid)
            {
                uOnLine.gameObject.SetActive(false);
            }
            else uOnLine.gameObject.SetActive(!player.player.hasStatus(SimplePlayer.STATUS_ONLINE));
        }

        public virtual void updateSocre()
        {
            long score = 0;
            if (Room.room != null)
            {
                var count = Room.room.getSpotRedCount(player.userid);
                if (count != null) score = count.score;
                if (Room.room.isType(Room.JBC))
                {
                    score = (player.userid != Player.player.userid) ? count.score : Player.player.money;
                }
            }

            uPoint.setVisible(true);
            uPoint.text = MathKit.getRound2LongStr(score);
        }
    }
}
