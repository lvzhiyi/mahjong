using basef.player;
using cambrian.common;
using platform.spotred;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 玩家信息 </summary>
    public class ArenaRecordContentUserBar : UnrealLuaSpaceObject
    {
        /// <summary> 玩家名 </summary>
        UnrealTextField username;

        /// <summary> 玩家分数 </summary>
        UnrealTextField score;

        /// <summary> 玩家id</summary>
        UnrealTextField usernameid;

        /// <summary> 所属 </summary>
        UnrealTextField industry;
        /// <summary>
        /// 头像
        /// </summary>
        PlayerCircleHeadView headview;


        string names;

        long socres;

        string mastername;

        long playerid;

        string head;
        /// <summary>
        /// 上级id
        /// </summary>
        long masterid;

        protected override void xinit()
        {
            username = transform.Find("username").GetComponent<UnrealTextField>();
            username.init();

            score = transform.Find("score").GetComponent<UnrealTextField>();
            score.init();

            usernameid= transform.Find("usernameid").GetComponent<UnrealTextField>();
            usernameid.init();

            industry = transform.Find("industry").GetComponent<UnrealTextField>();
            industry.init();

            headview = transform.Find("head").GetComponent<PlayerCircleHeadView>();
            headview.init();
        }

        protected override void xrefresh()
        {
            username.text = this.names;
            score.text = MathKit.getRound2LongStr(socres);
            usernameid.text = "("+this.playerid.ToString()+")";
            
            headview.setData(head,playerid);
            headview.refresh();
            if (socres > 0)
            {
                score.color = new Color(0.3f, 0.7f, 0.1f, 1); //"<color =#4AB125>" + MathKit.getRound2LongStr(socres) + "</color>";
            }
            else
            {
                score.color = new Color(0.9f, 0.4f, 0.2f,1); //"<color =#F27A3F>"+ MathKit.getRound2LongStr(socres) + "</color>";            
            }


            var arena = Arena.arena;
            var statusKit = new StatusAble();
            statusKit.setStatus(arena.getMember().status);

            if (statusKit.hasStatus(524288) && arena.getMember().master == arena.getMaster() || arena.getMember().isMaster())
            {
                if (masterid != 0)
                {
                    industry.text = masterid + "(" + this.mastername + ")";
                }
                else
                {
                    industry.text = mastername;
                }
            }
            else
            {
                industry.text = "****";
            }

            
        }

        public void setData(long socres,string names,string head,long playerid, string mastername,long masterid)
        {
            this.names = names;
            this.socres = socres;
            this.head = head;
            this.playerid = playerid;
            this.mastername = mastername;
            this.masterid = masterid;
        }
    }
}
