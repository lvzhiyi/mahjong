using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 奖章明细 内容 bar </summary>
    public class ArenaAccountsMedalContentBar: ScrollBar
    {
        ArenaMedalBill data;

        /// <summary> 奖章去向 </summary>
        UnrealTextField value;

        /// <summary> 剩余奖章 </summary>
        UnrealTextField surplus;

        /// <summary> 交易分类 </summary>
        UnrealTextField type;

        /// <summary> 备注 </summary>
        UnrealTextField remark;

        /// <summary> 时间 </summary>
        UnrealTextField time;

        protected override void xinit()
        {
            value = this.transform.Find("rootnum").GetComponent<UnrealTextField>();
            value.init();

            surplus = this.transform.Find("surplus").GetComponent<UnrealTextField>();
            surplus.init();

            type = this.transform.Find("tradetype").GetComponent<UnrealTextField>();
            type.init();

            remark = this.transform.Find("remark").GetComponent<UnrealTextField>();
            remark.init();

            time = this.transform.Find("time").GetComponent<UnrealTextField>();
            time.init();
        }

        protected override void xrefresh()
        {
            if (data == null) return;
            setMedalRoot();
            setTradeType();
            setTime();

            surplus.text = data.medal.ToString();
            remark.text = data.info;
        }

        /// <summary> 显示交易时间 </summary>
        private void setTime()
        {
            time.text = TimeKit.format(data.time, "yyyy-MM-dd HH:mm");
        }

        /// <summary> 显示交易分类 </summary>
        private void setTradeType()
        {
            switch (data.type)
            {
                case ArenaMedalBill.TYPE_ARENA_EXCHANGE:
                    type.text = "积分兑换";
                    break;
                default:
                    type.text = "";
                    break;
            }
        }

        /// <summary> 显示金豆来源数量 </summary>
        private void setMedalRoot()
        {
            if (data.getValue() >= 0)
            {
                value.text = "+" + data.getValue().ToString();
                value.color = ColorKit.getColor(47,182,38,255);

            }
            else
            {
                value.color = ColorKit.getColor(238,1,1,255);
            }
        }

        public void setData(ArenaMedalBill data)
        {
            this.data = data;
        }

    }
}
