using basef.arena;
using basef.player;
using cambrian.common;
using UnityEngine.UI;

namespace platform.spotred.room
{
    /// <summary>
    /// 解散房间界面中的玩家
    /// </summary>
    public class DisbanderBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 是否是发起者
        /// </summary>
        private bool sponsor;

        private int voterStatus;

        private MatchPlayer player;

        /// <summary>
        /// 昵称
        /// </summary>
        Text nickname;
        /// <summary>
        /// 用户ID
        /// </summary>
        Text userid;
        /// <summary>
        /// 状态（是否同意）
        /// </summary>
        UnrealTextField zhuangtai;
        /// <summary>
        /// 头像
        /// </summary>
        private PlayerCircleHeadView headView;

        protected override void xinit()
        {
            nickname = this.transform.Find("name").GetComponent<Text>();
            userid = this.transform.Find("id").GetComponent<Text>();
            headView = this.transform.Find("player").GetComponent<PlayerCircleHeadView>();
            headView.init();
            zhuangtai = this.transform.Find("zhuangtai").GetComponent<UnrealTextField>();
        }

        protected override void xrefresh()
        {
            nickname.text = this.player.player.name;
            userid.text = this.player.player.userid.ToString();
            if (sponsor)
            {
                zhuangtai.text = "发起者";
            }
            else if (voterStatus == Voting.VOTE_AGREED)
            {
                zhuangtai.text = "同意申请";
            }
            else if (voterStatus == Voting.VOTE_REFUSED)
            {
                zhuangtai.text = "拒接申请";
            }
            else
            {
                zhuangtai.text = "等待中";
            }

            headView.setData(player.player.head, player.userid);
            headView.refresh();

            var room = Room.room;
            if (room.isType(Room.ARENA) && room.getBind() > 0)
            {
                CommandManager.addCommand(new GetArenaInfoCommand(room.getBind()), back);
            }
        }

        public void back(object obj)
        {
            if (obj == null)
                return;
            var arena = (Arena)obj;
            var member = arena.getMember();
            var ismaster = arena.isMaster(member.userid);
            var isfumaster = arena.getMaster() == member.master;
            var userid = player.player.userid + "";
            if (!ismaster && !(member.hasStatus(524288) && isfumaster))
            {
                var len = userid.Length;
                var start = userid.Substring(0, 2);
                var endstr = userid.Substring(len - 3, 2);
                this.userid.text = "ID:" + start + "****" + endstr;
            }
            else
                this.userid.text = userid;
        }

        public void setSponsor(bool sponsor)
        {
            this.sponsor = sponsor;
        }

        public void setVoterStatus(int status)
        {
            this.voterStatus = status;
        }

        public void setMatchPlayer(MatchPlayer player)
        {
            this.player = player;
        }
    }
}
