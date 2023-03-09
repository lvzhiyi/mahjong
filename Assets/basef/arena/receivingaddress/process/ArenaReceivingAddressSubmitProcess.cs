using cambrian.common;
namespace basef.arena
{
    /// <summary> 提交订单 </summary>
    public class ArenaReceivingAddressSubmitProcess : MouseClickProcess
    {
        string color = "#FF0000";

        public override void execute()
        {
            ArenaFillInTheReceivingAddressPanel panel = UnrealRoot.root.getDisplayObject<ArenaFillInTheReceivingAddressPanel>();
            string info = "";
            if (panel.username.text == "")
            {
                Alert.show("请输入收件人名称"); return;
            }
            if (panel.tel.text == "" || panel.tel.text.Length != 11)
            {
                Alert.show("请输入收件人手机号码"); return;
            }

            //if (panel.province.text == "")
            //{

            //}
            //if (panel.city.text == "")
            //{
            //    Alert.show("请输入收件人所在城市"); return;
            //}
            //if (panel.region.text == "")
            //{
            //    Alert.show("请输入收件人所在城市区域"); return;
            //}
            if (panel.address.text == "")
            {
                Alert.show("请输入收件人详细地址"); return;
            }
            info = string.Format(/*panel.province.text + panel.city.text + panel.region.text + */panel.address.text);
            CommandManager.addCommand(new ArenaExchangePhysicalCommand(panel.goodsid,info,panel.tel.text,panel.username.text),back);
        }

        public void back(object obj)
        {
            if (obj == null) return;
            string str = "";

            if ((bool)obj)
            {
                str = string.Format("<color={0}>下单成功!请联系客服! \n 客服微信号:HGX20180101 </color>",color);
            }
            else str = "下单失败!";
            Alert.show(str);
            UnrealRoot.root.getDisplayObject<ArenaFillInTheReceivingAddressPanel>().setVisible(false);
        }
    }
}