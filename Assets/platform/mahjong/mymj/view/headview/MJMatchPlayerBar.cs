using basef.player;
using cambrian.common;
using cambrian.uui;
using platform.spotred;
using platform.spotred.waitroom;
using UnityEngine.UI;

namespace platform.mahjong
{
    public class MJMatchPlayerBar : MatchPlayerBar
    {
        protected override void xinit()
        {
            if (transform.Find("text") != null)
                this.text = this.transform.Find("text").GetComponent<UnrealTextField>();
            if (this.transform.Find("quickmsg") != null)
            {
                this.quickmsg = this.transform.Find("quickmsg").GetComponent<MatchIMQuickMSgBar>();
                if (quickmsg != null)
                    this.quickmsg.init();
            }

            this.banker = this.transform.Find("banker");

            if (this.transform.Find("emoji") != null)
            {
                this.emoji = this.transform.Find("emoji").GetComponent<MatchIMQuickMSgBar>();
                if (this.emoji != null)
                    this.emoji.init();
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

            if (this.transform.Find("point") != null)
                this.point = this.transform.Find("point").GetComponent<UnrealTextField>();
            if (this.transform.Find("info") != null)
            {
                this.playerHeadView = this.transform.Find("info").GetComponent<PlayerHeadView>();
                this.playerHeadView.init();
            }
            if (this.transform.Find("offlinetime") != null)
            {
                this.offlinetime = this.transform.Find("offlinetime").GetComponent<Text>();
            }
        }




        public override void setPlayer(MatchPlayer player)
        {
            this.sex = player.player.sex;
            long score = 0;
            if (Room.room != null)
            {
                SpotRedCount count = Room.room.getSpotRedCount(player.userid);
                if (count != null)
                    score = count.score;
            }

            if (Room.room != null && (Room.room.isType(Room.JBC)))
            {
                if (player.userid != Player.player.userid)
                    score = Room.room.getSpotRedCount(player.userid).score;
                else
                    score = Player.player.money;
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
                offline.gameObject.SetActive(!player.player.hasStatus(SimplePlayer.STATUS_ONLINE));
                if (!player.isTrustee())
                    offline.gameObject.SetActive(!player.player.hasStatus(SimplePlayer.STATUS_ONLINE));
            }

            if (point_icon != null)
            {
                point_icon.ShowIndex(0);
                if (Room.room != null && (Room.room.isType(Room.JBC) || Room.room.isType(Room.CLUB | Room.JBC)))
                    point_icon.ShowIndex(1);
            }

            if (this.robotstauts != null) this.robotstauts.gameObject.SetActive(player.isTrustee());

        }

        protected override void xrefresh()
        {
            if (this.quickmsg != null)
                this.quickmsg.setVisible(false);
            if (this.emoji != null)
                this.emoji.setVisible(false);

            if (text != null)
                this.text.text = this.player.name;
            if (this.id != null)
                this.id.text = "ID:" + this.player.userid;
            if (this.point != null)
            {
                this.point.text = MathKit.getRound2LongStr(score);
            }

            if (this.banker != null)
                this.banker.gameObject.SetActive(false);

            playerHeadView.setDatas(this.player.head, player.userid, player.sex);
            playerHeadView.refresh();

            if (offlinetime != null)
            {
                offlinetime.gameObject.SetActive(!matchplayer.player.hasStatus(1));
            }
            
            if (offlinetime != null)
            {
                //temp = TimeKit.currentTimeMillis - time;
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

        
        protected override void xupdate()
        {
            if (isoffLine)
                offlinetimeback();

        }
    }
}
