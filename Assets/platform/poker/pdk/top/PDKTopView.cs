using basef.rule;
using cambrian.common;
using cambrian.uui;

namespace platform.poker
{
    public class PDKTopView : PKTopView
    {
        protected SpritesList boomPoint;

        protected override void xinit()
        {
            base.xinit();
            boomPoint = transform.Find("multiple").Find("name").GetComponent<SpritesList>();
        }

        public override void showDianliang(string str)
        {
            base.showDianliang(str);
        }

        public override void showXinhao(string str)
        {
            base.showXinhao(str);
        }

        protected override void xupdate()
        {
            base.xupdate();
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            boomPoint.ShowIndex(Room.room.roomRule.rule.getIntAtribute("multipScore")==0 ? 0 : 1, true);
        }

        public override void hideShow(Rule rule)
        {
            base.hideShow(rule);
        }

        public override void setMuitplier(int Muitiplier)
        {
            base.setMuitplier(Muitiplier);
            bool b=Room.room.roomRule.rule.getIntAtribute("multipScore")==0;
            if (b)
            {
                multiple.text = "" + Muitiplier;
            }
            else
            {
                multiple.text = "" + MathKit.getRound2Int(Muitiplier);
            }
        }
    }
}
