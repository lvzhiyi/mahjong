using basef.player;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 兑换实物奖品 按钮 </summary>
    public class ArenaExchangePhysicalGoodsProcess : MouseClickProcess
    {
        string color = "#FF0000";

        public override void execute()
        {
            ArenaExchangePhysicalContentBar bar = transform.GetComponent<ArenaExchangePhysicalContentBar>();

            if (Player.player.medal < bar.data.price2)
            {
                Alert.autoShow("奖章数量不足!");
                return;
            }


            string str = string.Format("是否使用<color={0}>{1}</color>兑换<color={0}>{2}</color>",color,bar.getPrice(),bar.getTitleName());
            Alert.confirmShow(str,back1);
        }

        void back1(Transform trans)
        {
            ArenaFillInTheReceivingAddressPanel panel = UnrealRoot.root.getDisplayObject<ArenaFillInTheReceivingAddressPanel>();
            //panel.refresh();
            panel.goodsid = transform.GetComponent<ArenaExchangePhysicalContentBar>().getSid();
            panel.setCVisible(true);
        }
    }
}
