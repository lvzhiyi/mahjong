using basef.player;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    public class ArenaChangTableBar : ScrollBar
    {
        UnrealTableContainer container;

        Text rulename;

        Text jushu;

        ArenaRoom room;

        protected override void xinit()
        {
            container = this.transform.Find("joinhouse").GetComponent<UnrealTableContainer>();
            container.init();
            rulename = this.transform.Find("rulename").Find("title").GetComponent<Text>();
            jushu = this.transform.Find("rulename").Find("jushu").GetComponent<Text>();
        }

        public void setData(ArenaRoom room)
        {
            this.room = room;
        }

        public ArenaRoom getRoom()
        {
            return room;
        }

        protected override void xrefresh()
        {
            rulename.text = room.rule.name;
            jushu.text = room.rule.rule.matchCount + "局";
            int num = room.rule.rule.playerCount;
            container.resize(num);
            SimplePlayer[] players = room.players;
            ArenaChangTableUserBar bar;
            for (int i = 0; i < players.Length; i++)
            {
                bar = (ArenaChangTableUserBar)container.bars[i];
                bar.setData(players[i]);
                bar.refresh();
            }
            container.resizeDelta();
            container.relayout();
        }
    }
}
