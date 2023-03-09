using cambrian.common;
using UnityEngine.UI;

namespace platform.spotred.over
{
    public class AllOverScoreBar:UnrealLuaSpaceObject
    {
        private Text t_num;

        private Text t_score;

        private int num;

        private long socre;


        protected override void xinit()
        {
            this.t_num = this.transform.Find("num").GetComponent<Text>();
            this.t_score = this.transform.Find("score").GetComponent<Text>();
        }

        public void setData(int num,long socre)
        {
            this.num = num;
            this.socre = socre;
        }

        protected override void xrefresh()
        {
            this.t_num.text ="第"+ getNum(num)+ "局";
            string scoretext = MathKit.getRound2LongStr(socre);
            this.t_score.text = scoretext + "";
            if (socre < 0)
            {
                this.t_score.text = "<color=#b96d0f>" + scoretext + "</color>";
            }
            else if (socre == 0)
            {
                this.t_score.text = "<color=#b96d0f>" + scoretext + "</color>";
            }
            else
            {
                this.t_score.text = "<color=#438639>" + scoretext + "</color>";
            }
        }

        public void re()
        {
            this.t_num.text = "第" + getNum(num) + "局";
            this.t_score.text ="无";
        }

        public string getNum(int index)
        {
            switch (index)
            {
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
                case 7:
                    return "七";
                case 8:
                    return "八";
                case 9:
                    return "九";
                case 10:
                    return "十";
                case 11:
                    return "十一";
                case 12:
                    return "十二";
                case 13:
                    return "十三";
                case 14:
                    return "十四";
                case 15:
                    return "十五";
                case 16:
                    return "十六";
            }

            return "0";
        }


    }
}
