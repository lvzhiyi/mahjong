using cambrian.common;
using UnityEngine;

namespace basef.arena
{
    /// <summary> 兑换比赛奖品 按钮 </summary>
    public class ArenaExchangeMatchGoodsProcess : MouseClickProcess
    {
        string color = "#FF0000";

        public override void execute()
        {
            long gold = transform.GetComponent<ArenaExchangeMatchContentBar>().getData().getValue();
            string str = string.Format("是否使用<color={0}>{1}积分</color>兑换<color={0}>{2}奖章</color>",color,gold,gold / ArenaAddAwardPanel.MEDAL_RATIO);
            Alert.confirmShow(str,back1);
        }

        void back1(Transform trans)
        {
            int uuid = transform.GetComponent<ArenaExchangeMatchContentBar>().getUuid();
            CommandManager.addCommand(new ArenaExchangePrizeCommand(uuid),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;

            if (Arena.arena.getMember().isAgent() && (bool)obj)
            {
                Alert.show("奖章兑换成功!!!");
            }
            else if ((bool)obj)
            {
                Alert.show("奖章兑换中,请等待上级审批同意!!!");
            }
            else
            {
                Alert.show("兑换比赛奖品失败!!!");
            }
        }
    }
}
