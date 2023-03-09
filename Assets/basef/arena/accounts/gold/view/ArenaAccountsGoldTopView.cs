using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 顶部界面管理 </summary>
    public class ArenaAccountsGoldTopView : UnrealLuaSpaceObject
    {
        /// <summary> 类型选择 </summary>
        Dropdown type;

        /// <summary> 查询金豆负数 </summary>
        UnrealTextField goldminus;

        /// <summary> 剩余金豆 </summary>
        UnrealTextField goldsurplus;

        private bool checkgoldminus = false;

        public int typeindex = ArenaAccountsGold.TYPE_ALL;

        private double arenagold;

        protected override void xinit()
        {
            base.xinit();

            goldminus = this.transform.Find("minus").GetComponent<UnrealTextField>();
            goldminus.init();

            goldsurplus = this.transform.Find("surplus").GetComponent<UnrealTextField>();
            goldsurplus.init();

            type = this.transform.Find("type").GetComponent<Dropdown>();
            
        }

        private string[] typename;

        private void typeInit()
        {
            if(Arena.arena.getMember().isAgent())
                typename=new string[]{ "全部", "奖励扣减", "对局胜负", "赛场报名", "奖章兑换", "奖励" };
            else
                typename = new string[] { "全部", "奖励扣减", "对局胜负", "赛场报名", "奖章兑换"};
            type.options.Clear();
            Dropdown.OptionData data = null;
            for (int i = 0; i < typename.Length; i++)
            {
                data = new Dropdown.OptionData();
                data.text = typename[i];
                type.options.Add(data);
            }
            type.value = typeindex;
        }

        public void setData(double arenagold)
        {
            this.arenagold = arenagold;
        }

        protected override void xrefresh()
        {
            typeInit();

            goldminus.transform.Find("img").GetComponent<Image>().gameObject.SetActive(checkgoldminus);

            goldsurplus.text = this.arenagold.ToString();

            type.value = typeindex;
        }

        /// <summary> 更改玩家选择的下标 </summary>
        public void onStartSelect()
        {
            typeindex = type.value;
        }

        /// <summary> 设置金豆流水不足 </summary>
        public void setCheckGoldMinus(bool checkgoldminus)
        {
            this.checkgoldminus = checkgoldminus;
        }
    }
}
