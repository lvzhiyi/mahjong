using UnityEngine.UI;
using UnityEngine;

namespace basef.arena
{
    public class ArenaRuleTicketTypeBar:UnrealLuaSpaceObject
    {

        Image select;

        Text text;

        public int selectType;

        [HideInInspector] public bool isSelect;

        protected override void xinit()
        {
            select = transform.Find("normal").GetComponent<Image>();
            text = transform.Find("text").GetComponent<Text>();
        }


        public void setData(int selectType)
        {
            this.selectType = selectType;
        }


        protected override void xrefresh()
        {
            string info = index_space == 0 ? "大赢家支付" : "AA支付";
            text.text = info;
            text.color = new Color(0.4f, 0.29f, 0.1f, 1f);
            select.gameObject.SetActive(false);
            isSelect = false;
            if (index_space == 0 && selectType == ArenaLockRule.TYPE_STEP||(index_space == 1 && selectType == ArenaLockRule.TYPE_AA))
            {
                select.gameObject.SetActive(true);
                text.color = new Color(0.76f, 0.29f, 0.17f, 1f);
                isSelect = true;
            }
        }
    }
}
