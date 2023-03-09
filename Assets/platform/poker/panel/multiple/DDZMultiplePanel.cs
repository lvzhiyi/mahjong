using cambrian.unreal.scroll;
using UnityEngine;

namespace platform.poker
{
    public class DDZMultiplePanel : UnrealLuaPanel
    {
        UnrealTextField basebet;

        UnrealTextField mingpai;

        UnrealTextField grablord;

        UnrealTextField dipai;

        UnrealTextField boom;

        UnrealTextField chuntian;

        UnrealTextField maxPoint;

        ScrollContainer container;

        DDZMultipleBean bean;

        const string hint = "ㅡㅡ";

        protected override void xinit()
        {
            base.xinit();
            Transform contents = content.transform.Find("content");
            Transform pMultiple = contents.transform.Find("publicMultiple");
            Transform sMultiple = contents.transform.Find("singleMultiple");

            maxPoint = contents.Find("maxPoint").GetComponent<UnrealTextField>();
            maxPoint.init();

            basebet = pMultiple.Find("basebet").GetComponent<UnrealTextField>();
            basebet.init();

            mingpai = pMultiple.Find("mingpai").GetComponent<UnrealTextField>();
            mingpai.init();

            grablord = pMultiple.Find("grablandlord").GetComponent<UnrealTextField>();
            grablord.init();

            dipai = pMultiple.Find("dipai").GetComponent<UnrealTextField>();
            dipai.init();

            boom = pMultiple.Find("boom").GetComponent<UnrealTextField>();
            boom.init();

            chuntian = pMultiple.Find("chuntian").GetComponent<UnrealTextField>();
            chuntian.init();

            container = sMultiple.Find("container").GetComponent<ScrollContainer>();
            container.init();
        }

        protected override void xrefresh()
        {
            showMultiple();
            container.updateData(updateScrollData);
            container.resize(Room.room.getPlayerCount());
        }

        private void showMultiple()
        {
            if (bean != null)
            {
                basebet.text = bean.basePoint == 0 ? hint : bean.basePoint + "";
                mingpai.text = bean.mingPoint == 1 ? hint : bean.mingPoint + "";
                grablord.text = bean.qiangPoint == 1 ? hint : bean.qiangPoint + "";
                boom.text = bean.boomPoint == 1 ? hint : bean.boomPoint + ""; ;
                chuntian.text = bean.chunPoint == 1 ? hint : bean.chunPoint + "";
                maxPoint.text = bean.maxPoint + "";
            }
            else
            {
                basebet.text = hint;
                mingpai.text = hint;
                grablord.text = hint;
                boom.text = hint;
                chuntian.text = hint;
                maxPoint.text = Room.room.roomRule.rule.maxPoint + "";
            }
            dipai.text = hint;
        }
        
        private void updateScrollData(ScrollBar bars,int index)
        {
            resetBarData();
            var bar = (DDZMultipleInfoBar)bars;
            if (Room.room.players[index] != null)
            {
                uName = Room.room.players[index].player.name;
                isLord = (DDZMatch.match != null && DDZMatch.match.diZhuIndex == index);
            }
            if (bean != null)
            {
                publicP = bean.allPoint;
                boorP = bean.boorPoint[index];
                lordP = bean.landlordPoint;
                grossP = bean.getSinglePoint(index);
            }
            bar.setData(uName,lordP,boorP,publicP,grossP,isLord);
            bar.refresh();
        }

        int publicP;
        int grossP;
        int boorP;
        int lordP;
        bool isLord;
        string uName;

        private void resetBarData()
        {
            publicP = 0;
            grossP = 0;
            boorP = 0;
            lordP = 0;
            isLord = false;
            uName = "";
        }

        public void setData(DDZMultipleBean bean)
        {
            this.bean = bean;
        }
    }
}
