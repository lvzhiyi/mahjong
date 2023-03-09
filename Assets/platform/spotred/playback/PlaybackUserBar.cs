using basef.player;
using cambrian.common;
using UnityEngine;

namespace platform.spotred.playback
{
    /// <summary> 玩家信息 </summary>
    public class PlaybackUserBar : UnrealLuaSpaceObject
    {
        /// <summary> 玩家名 </summary>
        UnrealTextField username;

        /// <summary> 玩家分数 </summary>
        UnrealTextField score;

        /// <summary> 玩家id</summary>
        //UnrealTextField usernameid;

        /// <summary>
        /// 头像
        /// </summary>
        PlayerCircleHeadView headview;


        SimplePlayer player;

        long socres;

        protected override void xinit()
        {
            username = transform.Find("username").GetComponent<UnrealTextField>();
            username.init();

            score = transform.Find("score").GetComponent<UnrealTextField>();
            score.init();

            //usernameid = transform.Find("usernameid").GetComponent<UnrealTextField>();
            //usernameid.init();

            headview = transform.Find("head").GetComponent<PlayerCircleHeadView>();
            headview.init();
        }

        protected override void xrefresh()
        {
            username.text = player.name;
            score.text = MathKit.getRound2LongStr(socres);
            //usernameid.text = "(" + player.userid.ToString() + ")";
            headview.setData(player.head, player.userid);
            headview.refresh();
            if (socres > 0)
            {
                score.color = new Color(0.3f, 0.7f, 0.1f, 1); //"<color =#4AB125>" + MathKit.getRound2LongStr(socres) + "</color>";
            }
            else
            {
                score.color = new Color(0.9f, 0.4f, 0.2f, 1); //"<color =#F27A3F>"+ MathKit.getRound2LongStr(socres) + "</color>";            
            }
        }

        public void setData(long socres, SimplePlayer player)
        {
            this.socres = socres;
            this.player = player;
        }
    }
}
