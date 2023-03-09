namespace platform.spotred.waitroom
{
    public class IpGPS3Panel:UnrealLuaPanel
    {
        private MatchPlayer[] players;

        private IPGPS2View gps2View;

        private IPGPS3View gps3View;

        private IPGPS4View gps4View;


        protected override void xinit()
        {
            base.xinit();
            gps2View = content.Find("content").Find("ip2").GetComponent<IPGPS2View>();
            gps2View.init();
            gps2View.setVisible(false);
            gps3View = content.Find("content").Find("ip3").GetComponent<IPGPS3View>();
            gps3View.init();
            gps3View.setVisible(false);
            gps4View = content.Find("content").Find("ip4").GetComponent<IPGPS4View>();
            gps4View.init();
            gps4View.setVisible(false);
        }


        public void setMatchPlayers(MatchPlayer[] players)
        {
            this.players = players;
        }

        protected override void xrefresh()
        {
            gps3View.setVisible(false);
            gps2View.setVisible(false);
            gps4View.setVisible(false);
            if (players.Length==3)
            {
                gps3View.setMatchPlayers(players);
                gps3View.refresh();
                gps3View.setVisible(true);
            }
            else if(players.Length==2)
            {
                gps2View.setMatchPlayers(players);
                gps2View.refresh();
                gps2View.setVisible(true);
            }
            else if (players.Length==4)
            {
                gps4View.setMatchPlayers(players);
                gps4View.refresh();
                gps4View.setVisible(true);
            }
        }
    }
}
