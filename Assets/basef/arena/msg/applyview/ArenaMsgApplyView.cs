using cambrian.common;
using cambrian.unreal.scroll;
using UnityEngine.UI;

namespace basef.arena
{
    /// <summary> 赛场消息 赛场审核 界面 </summary>
    public class ArenaMsgApplyView : UnrealLuaSpaceObject
    {
        ArrayList<ArenaEvent> list;
        /// <summary>
        /// 总的事件
        /// </summary>
        private ArrayList<ArenaEvent> allList;

        /// <summary> 类型选择 </summary>
        public Dropdown type;

        public ScrollContainer container;

        int typeindex = 0;

        string[] typename = new string[]
        { "全部","赛场报名","奖章兑换" };

        protected override void xinit()
        {
            container = this.transform.Find("container").GetComponent<ScrollContainer>();
            container.init();
            type = this.transform.Find("type").GetComponent<Dropdown>();
        }

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
            type.value = 0;
        }

        /// <summary> 设置数据 紧跟刷新 </summary>
        public void setData(ArrayList<ArenaEvent> list)
        {
            allList = list;
            this.list = list;
            xrefresh();
        }

        public int getType()
        {
            switch (typeindex)
            {
                case 0: return ArenaEvent.EVENT_TYPE_MEMBER;
                case 1: return ArenaEvent.TYPE_APPLY_MATCH;
                case 2: return ArenaEvent.TYPE_APPLY_EXCHANGE_MEDAL;
                default: return -1;
            }
        }

        /// <summary> 更改玩家选择的下标 </summary>
        public void onStartSelect()
        {
            typeindex = type.value;
            int type1 = getType();
            resetList(type1);
        }

        private void resetList(int type)
        {
            ArrayList<ArenaEvent> ll = new ArrayList<ArenaEvent>();
            ArenaEvent aEvent = null;
            for (int i = 0; i < allList.count; i++)
            {
                aEvent = allList.get(i);
                if (type == ArenaEvent.EVENT_TYPE_MEMBER)
                    ll.add(aEvent);
                else if (aEvent.type == type)
                    ll.add(aEvent);
            }

            list = ll;
            container.resize(list.count);
        }

        public void remove(ArenaEvent arenaEvent)
        {
            list.remove(arenaEvent);
            allList.remove(arenaEvent);
        }


        protected override void xrefresh()
        {
            typeInit();
            container.updateData(updateScrollData);
            container.resize(list.count);
        }

        protected void updateScrollData(ScrollBar bars,int index)
        {
            ArenaMsgApplyContentBar bar = (ArenaMsgApplyContentBar)bars;
            bar.setData(list.get(index));
            bar.index_space = index;
            bar.refresh();
        }
    }
}
