using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine;

namespace platform.spotred.playback
{
    public class ReplayBar: ScrollBar
    {
      //  Replay replay;
        int index;
        SimplePlayer[] players;
        long[] scores;
        public long id;


        UnrealTextField indextxt;
        UnrealTextField player1;
        UnrealTextField point1;
        UnrealTextField player2;
        UnrealTextField point2;
        UnrealTextField player3;
        UnrealTextField point3;
        UnrealTextField player4;
        UnrealTextField point4;

        public void setInfo(int index, SimplePlayer[] players, long[] scores, long id)
        {
            this.index = index;
            this.players = players;
            this.scores = scores;
            this.id = id;
        }
        protected override void xrefresh()
        {
            this.indextxt.text = (this.index + 1) + "";

            this.player1.text = this.players[0].name;
            this.point1.text = MathKit.getRound2LongStr(scores[0]);

            if (this.scores[0] >= 0)
            {
                this.point1.color = new Color32(74, 177, 37, 255);
            }
            else
            {
                this.point1.color = new Color32(242, 122, 63, 255);
            }


            this.player2.text = this.players[1].name;
            this.point2.text = MathKit.getRound2LongStr(this.scores[1]);

            if (this.scores[1] >= 0)
            {
                this.point2.color = new Color32(74, 177, 37, 255);
            }
            else
            {
                this.point2.color = new Color32(242, 122, 63, 255);
            }

            if (this.players.Length == 2)
            {
                this.player3.setVisible(false);
                this.point3.setVisible(false);
                this.player4.setVisible(false);
                this.point4.setVisible(false);
                return;
            }
            else
            {
                this.player3.setVisible(true);
                this.point3.setVisible(true);
                this.player4.setVisible(true);
                this.point4.setVisible(true);
            }

            this.player3.text = this.players[2].name;
            this.point3.text = MathKit.getRound2LongStr(scores[2]);


            if (this.scores[2] >= 0)
            {
                this.point3.color = new Color32(74, 177, 37, 255);
            }
            else
            {
                this.point3.color = new Color32(242, 122, 63, 255);
            }


            if (this.players.Length == 3)
            {
                this.player4.setVisible(false);
                this.point4.setVisible(false);
                return;
            }
            else
            {
                this.player4.setVisible(true);
                this.point4.setVisible(true);
            }

            this.player4.text = this.players[3].name;
            this.point4.text = MathKit.getRound2LongStr(scores[3]);


            if (this.scores[3] >= 0)
            {
                this.point4.color = new Color32(74, 177, 37, 255);
            }
            else
            {
                this.point4.color = new Color32(242, 122, 63, 255);
            }
        }

        protected override void xinit()
        {
            this.indextxt = this.transform.Find("id").GetComponent<UnrealTextField>();
            this.indextxt.init();
            this.player1 = this.transform.Find("player1").GetComponent<UnrealTextField>();
            this.player1.init();
            this.point1 = this.transform.Find("point1").GetComponent<UnrealTextField>();
            this.point1.init();
            this.player2 = this.transform.Find("player2").GetComponent<UnrealTextField>();
            this.player2.init();
            this.point2 = this.transform.Find("point2").GetComponent<UnrealTextField>();
            this.point2.init();
            this.player3 = this.transform.Find("player3").GetComponent<UnrealTextField>();
            this.player3.init();
            this.point3 = this.transform.Find("point3").GetComponent<UnrealTextField>();
            this.point3.init();
            this.player4 = this.transform.Find("player4").GetComponent<UnrealTextField>();
            this.player4.init();
            this.point4 = this.transform.Find("point4").GetComponent<UnrealTextField>();
            this.point4.init();
        }
    }
}
