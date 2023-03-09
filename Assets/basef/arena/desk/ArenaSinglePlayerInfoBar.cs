using platform.spotred;
using basef.player;
using UnityEngine.UI;
using cambrian.common;

namespace basef.arena
{
    public class ArenaSinglePlayerInfoBar : UnrealLuaSpaceObject
    {
        MatchPlayer player;
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView headView;
        /// <summary>
        /// 头像背景框
        /// </summary>
        private Image head_bg;
        /// <summary>
        /// 昵称
        /// </summary>
        Text nickname;
        /// <summary>
        /// 玩家ID
        /// </summary>
        private Text playerid;
        /// <summary>
        /// 分数
        /// </summary>
        private Text score;

        protected override void xinit()
        {
            this.headView = transform.GetComponent<PlayerCircleHeadView>();
            this.headView.init();
            this.nickname = transform.Find("name").GetComponent<Text>();
            this.head_bg = transform.Find("head_kuang").GetComponent<Image>();
            this.playerid = transform.Find("id").GetComponent<Text>();
            this.score = transform.Find("score").GetComponent<Text>();
        }

        public void setMatchPlayer(MatchPlayer player)
        {
            this.player = player;
        }

        protected override void xrefresh()
        {
            if (player == null)
            {
                nickname.text = "";
                playerid.text = "";
                score.text = "";
            }
            else
            {
                nickname.text = player.player.name;
                
                score.text = MathKit.getRound2LongStr(player.score);
                var arena = Arena.arena;
                var member = arena.getMember();
                var ismaster = arena.isMaster(member.userid);
                var isfumaster = arena.getMaster() == member.master;
                if (!ismaster && !(member.hasStatus(524288) && isfumaster))
                {
                    var id = player.player.userid.ToString();
                    var len = id.Length;
                    var start = id.Substring(0, 2);
                    var endstr = id.Substring(len - 3, 2);
                    playerid.text = "ID:" + start + "****" + endstr;
                }
                else
                {
                    playerid.text = "ID:" + player.player.userid;
                }
            }

            head_bg.gameObject.SetActive(true);

            if (player == null)
            {
                head_bg.gameObject.SetActive(false);
                headView.setVisible(false);
            }
            else
            {
                

                headView.setVisible(true);
                headView.setData(player.player.head, player.userid);
                headView.refresh();
            }
        }
    }
}
