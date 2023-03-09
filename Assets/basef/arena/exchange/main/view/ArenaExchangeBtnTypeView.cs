using UnityEngine;

namespace basef.arena
{
    /// <summary> 按钮显示 </summary>
    public class ArenaExchangeBtnTypeView : UnrealLuaSpaceObject
    {
        GameObject matchbtn;   //比赛奖品 按钮

        GameObject physicalbtn;//实物兑换 按钮

        GameObject custombtn;

        int index = ArenaExchange.Prize_Match;

        protected override void xinit()
        {
            matchbtn = transform.Find("Prize_match").gameObject;

            physicalbtn = transform.Find("Prize_physical").gameObject;

            custombtn = transform.Find("Prize_custom").gameObject;
        }

        protected override void xrefresh()
        {
            hideAll();
            switch (index)
            {
                case ArenaExchange.Prize_Match:
                    matchbtn.SetActive(false);
                    custombtn.SetActive(true);
                    break;
                case ArenaExchange.Prize_Physical:
                    physicalbtn.SetActive(true);
                    break;
                default:break;
            }
        }

        private void hideAll()
        {
            matchbtn.SetActive(false);
            physicalbtn.SetActive(false);
            custombtn.SetActive(false);
        }

        public void setIndex(int index)
        {
            this.index = index;
        }
    }
}
