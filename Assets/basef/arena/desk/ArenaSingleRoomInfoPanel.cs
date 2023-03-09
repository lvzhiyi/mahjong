using basef.arena;
using cambrian.common;
using platform.spotred;
using UnityEngine;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaSingleRoomInfoPanel : UnrealLuaPanel
    {
        /// <summary>
        /// 单个房间玩家信息容器
        /// </summary>
        UnrealTableContainer container;
        /// <summary>
        /// 房间中的玩家
        /// </summary>
        MatchPlayer[] players;

        [HideInInspector] public ArenaRoom arenaRoom;

        /// <summary>
        /// 玩法
        /// </summary>
        private Text wanfa;

        public UnrealScaleButton disbandbtn;

        protected override void xinit()
        {
            base.xinit();
            this.container = this.transform.Find("Canvas").Find("content").Find("players").Find("container").GetComponent<UnrealTableContainer>();
            this.container.init();
            this.wanfa = this.transform.Find("Canvas").Find("content").Find("wanfa").GetComponent<Text>();
            this.disbandbtn = this.transform.Find("Canvas").Find("content").Find("disband").GetComponent<UnrealScaleButton>();
        }

        public void setDatas(ArenaRoom room, MatchPlayer[] players)
        {
            this.arenaRoom = room;
            this.players = players;
        }

        protected override void xrefresh()
        {
            this.container.resize(this.players.Length);
            for (int i = 0; i < this.players.Length; i++)
            {
                ArenaSinglePlayerInfoBar bar = (ArenaSinglePlayerInfoBar)this.container.bars[i];
                bar.setMatchPlayer(players[i]);
                bar.refresh();
            }
            this.container.resizeDelta();
            this.container.relayout();

            this.disbandbtn.setVisible(false);
            this.wanfa.text = arenaRoom.rule.rule.getPlayRule(true);

            Arena arena = Arena.arena;
            ArenaMember member = arena.getMember();
            bool ismaster = arena.isMaster(member.userid);
            bool isfumaster = arena.getMaster() == member.master;
            if (ismaster || (member.hasStatus(1024) && isfumaster)) disbandbtn.setVisible(true); //1024 为副擂主 的解散房间权限
        }
    }
}
