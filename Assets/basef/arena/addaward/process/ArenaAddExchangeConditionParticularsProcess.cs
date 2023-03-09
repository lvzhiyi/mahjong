namespace basef.arena
{
    /// <summary> 兑换条件详情 </summary>
    public class ArenaAddExchangeConditionParticularsProcess : MouseClickProcess
    {
        public override void execute()
        {
            Alert.show("1.奖章赠送比例为系统默认，你只要输入兑换的积分数量！\n 2.兑换比例为100个积分兑换1个奖章");
        }
    }
}
