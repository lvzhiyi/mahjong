using cambrian.common;
using cambrian.unreal.scroll;

namespace basef.arena
{
    /// <summary> 金豆明细 内容 bar </summary>
    public class ArenaAccountsGoldContentBar : ScrollBar
    {
        ArenaAccountsGoldContentData data;

        /// <summary> 金豆来源 </summary>
        UnrealTextField value;

        /// <summary> 剩余金豆 </summary>
        UnrealTextField arenagold;

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

            arenagold = this.transform.Find("surplus").GetComponent<UnrealTextField>();
            arenagold.init();

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
            setGoldRoot();
            setTradeType();
            setTime();

            arenagold.text = data.arenagold.ToString();
            remark.text = data.info.ToString();
            if (data.cause != 0)
            {
                if (data.type == 301)
                {
                    remark.text = "修改积分\n"+ data.causeName + "\n(ID:"+ data.cause + ")";
                }
                else
                    remark.text += "\n"+data.causeName + "(ID:" + data.cause + ")";
            }
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
                case ArenaAccountsGold.TYPE_APPLY_MATCH:
                    type.text = "休闲场报名";
                    break;
                case ArenaAccountsGold.TYPE_MATCH:
                    type.text = "修改积分";
                    break;
                case ArenaAccountsGold.TYPE_DECR_SUB_TICKETS:
                    type.text = "扣除保底奖励";
                    break;
                case ArenaAccountsGold.TYPE_DECR_TICKETS:
                    type.text = "奖励扣除";
                    break;
                case ArenaAccountsGold.TYPE_INCR_MEDAL:
                    type.text = "奖章兑换";
                    break;
                case ArenaAccountsGold.TYPE_INCR_TICKETS:
                    type.text = "奖励";
                    break;
                case ArenaAccountsGold.TYPE_INCR_TICKETS_BACK:
                    type.text = "奖励退回";
                    break;
                case ArenaAccountsGold.TYPE_MATCH_WIN:
                case ArenaAccountsGold.TYPE_MATCH_LOSE:
                    type.text = "对局胜负";
                    break;
                case ArenaAccountsGold.TYPE_KICK_MEMBER_ADD:
                case ArenaAccountsGold.TYPE_KICK_MEMBER_DECR:
                    type.text = "踢出成员";
                    break;
                case ArenaAccountsGold.TYPE_KICK_AGENT_ADD:
                case ArenaAccountsGold.TYPE_KICK_AGENT_DECR:
                    type.text = "踢出合伙人";
                    break;
                default:
                    type.text = "未知数据";
                    break;
            }
        }

        /// <summary> 显示金豆来源数量 </summary>
        private void setGoldRoot()
        {
            if (data.value >= 0)
            {
                value.text = "+" + data.value.ToString();
                value.color = ColorKit.getColor(47,182,38,255);
            }
            else
            {
                value.text = data.value.ToString();
                value.color = ColorKit.getColor(238,1,1,255);
            }
        }

        public void setData(ArenaAccountsGoldContentData data)
        {
            this.data = data;
        }
    }
}
