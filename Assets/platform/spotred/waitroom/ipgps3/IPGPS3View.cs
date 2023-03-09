using basef.player;
using cambrian.common;

namespace platform.spotred.waitroom
{
    public class IPGPS3View:UnrealLuaSpaceObject
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
        /// 右
        /// </summary>
        private IpMatchPlayerBar bar_r;
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
        /// 右到上
        /// </summary>
        private GPSBar RToT;
        /// <summary>
        /// 右到左
        /// </summary>
        private GPSBar RToL;
        /// <summary>
        /// 上到左
        /// </summary>
        private GPSBar tTol;

        protected override void xinit()
        {
            this.bar_b = this.transform.Find("bar_b").GetComponent<IpMatchPlayerBar>();
            this.bar_b.init();
            this.bar_r = this.transform.Find("bar_r").GetComponent<IpMatchPlayerBar>();
            this.bar_r.init();
            this.bar_t = this.transform.Find("bar_t").GetComponent<IpMatchPlayerBar>();
            this.bar_t.init();

            this.RToT = this.transform.Find("rtot").GetComponent<GPSBar>();
            this.RToT.init();
            this.RToL = this.transform.Find("rtol").GetComponent<GPSBar>();
            this.RToL.init();
            this.tTol = this.transform.Find("ttol").GetComponent<GPSBar>();
            this.tTol.init();
        }


        public void setMatchPlayers(MatchPlayer[] players)
        {
            this.players = players;
        }

        protected override void xrefresh()
        {
            RToT.hideText();
            RToL.hideText();
            tTol.hideText();


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
                    case LOC_RIGHT:
                        if (players[index] != null)
                        {
                            bar_r.setMatchPlayer(players[index]);
                            bar_r.refresh();
                            bar_r.setVisible(true);
                            sortplayers[1] = players[index];
                        }
                        else
                        {
                            bar_r.setVisible(false);
                        }
                        break;
                    case LOC_TOP:
                        if (players[index] != null)
                        {
                            bar_t.setMatchPlayer(players[index]);
                            bar_t.refresh();
                            bar_t.setVisible(true);
                            sortplayers[2] = players[index];
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

            for (int i = 0; i < sortplayers.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (sortplayers[1] != null)
                        {
                            distance = getDistance(sortplayers[0], sortplayers[1]);
                            this.RToL.setDistance(distance);
                        }

                        if (sortplayers[2] != null)
                        {
                            distance = getDistance(sortplayers[0], sortplayers[2]);
                            this.tTol.setDistance(distance);
                        }
                        break;
                    case 1:
                        if (sortplayers[1] != null)
                        {
                            if (sortplayers[2] != null)
                            {
                                distance = getDistance(sortplayers[1], sortplayers[2]);
                                this.RToT.setDistance(distance);
                            }
                        }
                        break;
                }
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
