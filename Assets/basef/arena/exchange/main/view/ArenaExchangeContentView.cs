using cambrian.common;

namespace basef.arena
{
    /// <summary> 奖品内容 </summary>
    public class ArenaExchangeContentView : UnrealLuaSpaceObject
    {
        /// <summary> 比赛奖品 </summary>
        ArenaExchangeMatchGoodsView matchview;

        /// <summary> 实物奖品 </summary>
        ArenaExchangePhysicalGoodsView physicalview;

        int index = ArenaExchange.Prize_Match;

        protected override void xinit()
        {
            matchview = this.transform.Find("prize_matchview").GetComponent<ArenaExchangeMatchGoodsView>();
            matchview.init();

            physicalview = this.transform.Find("Prize_physicalview").GetComponent<ArenaExchangePhysicalGoodsView>();
            physicalview.init();
        }

        protected override void xrefresh()
        {
            hideAll();
            switch (index)
            {
                case ArenaExchange.Prize_Match:
                    matchview.refresh();
                    matchview.setVisible(true);
                    break;
                case ArenaExchange.Prize_Physical:
                    physicalview.refresh();
                    physicalview.setVisible(true);
                    break;
            }
        }

        public void setData(int index,object obj = null)
        {
            this.index = index;
            switch (index)
            {
                case ArenaExchange.Prize_Match:
                    break;
                case ArenaExchange.Prize_Physical:
                    physicalview.setData((ArrayList<ArenaExchangePhysicalGoodsData>)obj);
                    break;
            }
        }

        private void hideAll()
        {
            matchview.setVisible(false);
            physicalview.setVisible(false);
        }
    }
}
