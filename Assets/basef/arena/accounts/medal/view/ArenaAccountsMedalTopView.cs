using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 顶部管理 </summary>
    public class ArenaAccountsMedalTopView : UnrealLuaSpaceObject
    {
        /// <summary> 类型选择 </summary>
        Dropdown type;

        /// <summary> 剩余奖章 </summary>
        UnrealTextField medalsurplus;

        /// <summary> 类型下标 </summary>
        public int typeindex = GetArenaAccountsMedalListCommand.TYPE_ALL;

        private long medal;

        protected override void xinit()
        {
            base.xinit();

            medalsurplus = this.transform.Find("surplus").GetComponent<UnrealTextField>();
            medalsurplus.init();

            type = this.transform.Find("type").GetComponent<Dropdown>();
            typeInit();
        }

        string[] typename = new string[]
        { "全部","积分兑换"};

        private void typeInit()
        {
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

        public void setData(int medal)
        {
            this.medal = medal;
        }

        protected override void xrefresh()
        {
            base.xrefresh();
            medalsurplus.text = this.medal.ToString();
            type.value = typeindex;
        }

        /// <summary> 更改玩家选择的下标 </summary>
        public void onStartSelect()
        {
            typeindex = type.value;
        }
    }
}
