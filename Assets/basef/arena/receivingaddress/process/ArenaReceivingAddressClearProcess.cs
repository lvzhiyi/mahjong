using UnityEngine;

namespace basef.arena
{
    /// <summary> 清除收货地址内容 </summary>
    public class ArenaReceivingAddressClearProcess : MouseClickProcess
    {
        public override void execute()
        {
            Alert.show("已清除收货地址内容",back);
        }

        void back(Transform trans)
        {
            ArenaFillInTheReceivingAddressPanel panel = UnrealRoot.root.getDisplayObject<ArenaFillInTheReceivingAddressPanel>();
            panel.textClear();
        }
    }
}