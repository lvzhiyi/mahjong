using System.Runtime.InteropServices;
using basef.player;
using cambrian.common;
using cambrian.unreal.scroll;
using cambrian.uui;

namespace basef.arena
{
    /// <summary> 赛场审核 内容 bar </summary>
    public class ArenaMsgApplyContentBar : ScrollBar
    {
        ArenaEvent data;

        /// <summary> 类型 </summary>
        private SpritesList type;

        /// <summary> 时间 </summary>
        private UnrealTextField time;

        /// <summary> 数量 </summary>
        private UnrealTextField value;

        /// <summary> 头像 </summary>
        private PlayerHeadView head;

        /// <summary> 用户名 </summary>
        private UnrealTextField username;

        /// <summary> 用户ID </summary>
        private UnrealTextField usersid;

        private UnrealTextField typename;
        /// <summary>
        /// 兑换条件
        /// </summary>
        private UnrealTextField exchangeinfo;

        protected override void xinit()
        {
            head = transform.Find("head").GetComponent<PlayerHeadView>();
            head.init();

            time = transform.Find("time").GetComponent<UnrealTextField>();
            time.init();

            value = transform.Find("num").GetComponent<UnrealTextField>();
            value.init();

            username = transform.Find("name").GetComponent<UnrealTextField>();
            username.init();

            usersid = transform.Find("usersid").GetComponent<UnrealTextField>();
            usersid.init();

            typename = transform.Find("typeinfo").GetComponent<UnrealTextField>();
            typename.init();

            exchangeinfo = transform.Find("exchangeinfo").GetComponent<UnrealTextField>();
            exchangeinfo.init();

            type = transform.Find("status").GetComponent<SpritesList>();
        }

        protected override void xrefresh()
        {
            username.text = data.name;
            usersid.text = data.getUserid().ToString();

            head.setData(data.head, data.getUserid());
            head.refresh();

            typename.text = data.getTypeName();

            exchangeinfo.text = data.getValue() + "金豆";

            this.time.text = TimeKit.format(data.time, "yyyy-MM-dd HH:mm:ss");
            setNum();
        }

        /// <summary> 设置数量 </summary>
        private void setNum()
        {
            switch (data.type)
            {
                case ArenaEvent.TYPE_APPLY_MATCH:
                    value.color = ColorKit.getColor(247, 26, 43, 255);
                    exchangeinfo.color = ColorKit.getColor(247, 26, 43, 255);
                    type.ShowIndex(0);
                    break;
                case ArenaEvent.TYPE_APPLY_EXCHANGE_MEDAL:
                    
                    value.color = ColorKit.getColor(74, 177, 37, 255);
                    exchangeinfo.color = ColorKit.getColor(74, 177, 37, 255);
                    type.ShowIndex(1);
                    break;
            }
            value.text = data.getValue().ToString();
        }

        public void setData(object obj)
        {
            data = (ArenaEvent)obj;
        }

        public ArenaEvent getData()
        {
            return data;
        }
    }
}
