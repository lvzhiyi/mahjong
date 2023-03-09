using basef.player;
using cambrian.common;

namespace platform.spotred.waitroom
{
    /// <summary>
    /// 两人显示
    /// </summary>
    public class IPGPS2View:UnrealLuaSpaceObject
    {
        /// <summary>
        /// 下,右，上,左
        /// </summary>
        public const int LOC_DOWN = 0, LOC_RIGHT = 1, LOC_TOP = 2, LOC_LEFT = 3;

        /// <summary>
        /// 下
        /// </summary>
        private IpMatchPlayerBar bar_b;
        /// <summary>
        /// 上
        /// </summary>
        private IpMatchPlayerBar bar_t;


        private MatchPlayer[] players;
        /// <summary>
        /// 排好序的玩家(下,右，上，左)
        /// </summary>
        private MatchPlayer[] sortplayers;
        /// <summary>
        /// 上到左
        /// </summary>
        private GPSBar tTol;

        protected override void xinit()
        {
            this.bar_b = transform.Find("bar_b").GetComponent<IpMatchPlayerBar>();
            this.bar_b.init();
            this.bar_t = transform.Find("bar_t").GetComponent<IpMatchPlayerBar>();
            this.bar_t.init();
            this.tTol = transform.Find("ttol").GetComponent<GPSBar>();
            this.tTol.init();
        }


        public void setMatchPlayers(MatchPlayer[] players)
        {
            this.players = players;
        }

        protected override void xrefresh()
        {
            this.tTol.hideText();

            sortplayers = new MatchPlayer[players.Length];

            int index = getMindex();

            for (int i = 0; i < players.Length; i++)
            {

                switch (getPlayerIndex(index))
                {
                    case LOC_DOWN:
                        if (players[index] != null)
                        {
                            bar_b.setMatchPlayer(players[index]);
                            bar_b.refresh();
                            bar_b.setVisible(true);
                            sortplayers[0] = players[index];
                        }
                        else
                        {
                            bar_b.setVisible(false);
                        }
                        break;
                    case LOC_TOP:
                        if (players[index] != null)
                        {
                            bar_t.setMatchPlayer(players[index]);
                            bar_t.refresh();
                            bar_t.setVisible(true);
                            sortplayers[1] = players[index];
                        }
                        else
                        {
                            bar_t.setVisible(false);
                        }
                        break;
                }
                index = getIndex(index);
            }

            string distance = "未知";

            if (sortplayers[0] != null && sortplayers[1] != null)
            {
                distance = getDistance(sortplayers[0], sortplayers[1]);
                this.tTol.setDistance(distance);
            }
        }

        public string getDistance(MatchPlayer scr, MatchPlayer dest)
        {
            if (scr.gps_latitude == 0 || dest.gps_latitude == 0) return "未知";

            double igpsns = scr.gps_longitude * 0.000001d;
            double igpswe = scr.gps_latitude * 0.000001d;
            double jgpsns = dest.gps_longitude * 0.000001d;
            double jgpswe = dest.gps_latitude * 0.000001d;

            double dis = MathKit.GetGPSDistance(igpswe, igpsns, jgpswe, jgpsns);
            return "相距" + MathKit.getDistance(dis);
        }


        private int getIndex(int index)
        {

            if (index == (players.Length - 1))
                index = 0;
            else
            {
                index++;
            }
            return index;
        }

        private int getPlayerIndex(int index)
        {
            int len = players.Length;

            int pos = ((index + len) - getMindex()) % len;

            switch (len)
            {
                case 4:
                    break;
                case 3:
                    break;
                case 2:
                    if (pos == 1)
                        pos = LOC_TOP;
                    break;
            }

            return pos;
        }

        private int getMindex()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null && players[i].userid == Player.player.userid)
                    return i;
            }

            return 0;
        }
    }
}
