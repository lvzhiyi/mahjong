using basef.player;
using cambrian.common;
using UnityEngine.UI;

namespace platform.spotred.waitroom
{
    public class IPGPS4View:UnrealLuaSpaceObject
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
        /// <summary>
        /// 左
        /// </summary>
        private IpMatchPlayerBar bar_l;

        private MatchPlayer[] players;
        /// <summary>
        /// 排好序的玩家(下,右，上，左)
        /// </summary>
        private MatchPlayer[] sortplayers;
        /// <summary>
        /// 下到右
        /// </summary>
        private GPSBar bToR;
        /// <summary>
        /// 下到左
        /// </summary>
        private GPSBar bToL;
        /// <summary>
        /// 下到上
        /// </summary>
        private GPSBar bToT;
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
        /// <summary>
        /// 底部头像背景
        /// </summary>
        private Image bHeadBg;
        /// <summary>
        /// 右侧头像背景
        /// </summary>
        private Image rHeadBg;
        /// <summary>
        /// 左侧头像背景
        /// </summary>
        private Image lHeadBg;
        /// <summary>
        /// 顶部头像背景
        /// </summary>
        private Image tHeadBg;

        protected override void xinit()
        {
            bar_b = this.transform.Find("bar_b").GetComponent<IpMatchPlayerBar>();
            bar_b.init();
            bar_r = this.transform.Find("bar_r").GetComponent<IpMatchPlayerBar>();
            bar_r.init();
            bar_t = this.transform.Find("bar_t").GetComponent<IpMatchPlayerBar>();
            bar_t.init();
            bar_l = this.transform.Find("bar_l").GetComponent<IpMatchPlayerBar>();
            bar_l.init();

            bToR = this.transform.Find("btor").GetComponent<GPSBar>();
            bToR.init();
            bToL = this.transform.Find("btol").GetComponent<GPSBar>();
            bToL.init();
            bToT = this.transform.Find("btot").GetComponent<GPSBar>();
            bToT.init();
            RToT = this.transform.Find("rtot").GetComponent<GPSBar>();
            RToT.init();
            RToL = this.transform.Find("rtol").GetComponent<GPSBar>();
            RToL.init();
            tTol = this.transform.Find("ttol").GetComponent<GPSBar>();
            tTol.init();
            bHeadBg = this.transform.Find("head_bg_b").GetComponent<Image>();
            rHeadBg = this.transform.Find("head_bg_r").GetComponent<Image>();
            lHeadBg = this.transform.Find("head_bg_l").GetComponent<Image>();
            tHeadBg = this.transform.Find("head_bg_t").GetComponent<Image>();
        }


        public void setMatchPlayers(MatchPlayer[] players)
        {
            this.players = players;
        }

        protected override void xrefresh()
        {
            bToR.hideText();
            bToL.hideText();
            bToT.hideText();
            RToT.hideText();
            RToL.hideText();
            tTol.hideText();

            bHeadBg.gameObject.SetActive(true);
            rHeadBg.gameObject.SetActive(true);
            lHeadBg.gameObject.SetActive(true);
            tHeadBg.gameObject.SetActive(true);

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
                            if (sortplayers.Length == 2)
                                sortplayers[1] = players[index];
                            else
                                sortplayers[2] = players[index];
                        }
                        else
                        {
                            bar_t.setVisible(false);
                        }
                        break;
                    case LOC_LEFT:
                        if (players[index] != null)
                        {
                            bar_l.setMatchPlayer(players[index]);
                            bar_l.refresh();
                            bar_l.setVisible(true);
                            sortplayers[3] = players[index];
                        }
                        else
                        {
                            bar_l.setVisible(false);
                        }
                        break;
                }
                index = getIndex(index);
            }

            string distance = "未知";


            if (sortplayers.Length == 2)
            {
                this.bToR.gameObject.SetActive(false);
                this.bToL.gameObject.SetActive(false);
                this.RToT.gameObject.SetActive(false);
                this.RToL.gameObject.SetActive(false);
                this.tTol.gameObject.SetActive(false);

                this.rHeadBg.gameObject.SetActive(false);
                this.lHeadBg.gameObject.SetActive(false);

                this.bar_r.gameObject.SetActive(false);
                this.bar_l.gameObject.SetActive(false);

                distance = getDistance(sortplayers[0], sortplayers[1]);
                this.bToT.setDistance(distance);
                return;
            }

            for (int i = 0; i < sortplayers.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (sortplayers[1] != null)
                        {
                            distance = getDistance(sortplayers[0], sortplayers[1]);
                            this.bToR.setDistance(distance);
                        }

                        if (sortplayers[2] != null)
                        {
                            distance = getDistance(sortplayers[0], sortplayers[2]);
                            this.bToT.setDistance(distance);
                        }

                        if (sortplayers.Length == 4 && sortplayers[3] != null)
                        {
                            distance = getDistance(sortplayers[0], sortplayers[3]);
                            this.bToL.setDistance(distance);
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

                            if (sortplayers.Length == 4 && sortplayers[3] != null)
                            {
                                distance = getDistance(sortplayers[1], sortplayers[3]);
                                this.RToL.setDistance(distance);
                            }
                        }
                        break;
                    case 2:
                        if (sortplayers[2] != null)
                        {
                            if (sortplayers.Length == 4 && sortplayers[3] != null)
                            {
                                distance = getDistance(sortplayers[2], sortplayers[3]);
                                this.tTol.setDistance(distance);
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
