using UnityEngine;

namespace basef.arena
{
    /// <summary> 填写收货地址 </summary>
    public class ArenaFillInTheReceivingAddressPanel : UnrealLuaPanel
    {
        /// <summary> 收件人名称 </summary>
        [HideInInspector] public UnrealInputTextField username;

        /// <summary> 手机号码 </summary>
        [HideInInspector] public UnrealInputTextField tel;

        ///// <summary> 省 </summary>
        //[HideInInspector] public UnrealInputTextField province;

        ///// <summary> 市 </summary>
        //[HideInInspector] public UnrealInputTextField city;

        ///// <summary> 区 </summary>
        //[HideInInspector] public UnrealInputTextField region;

        /// <summary> 详细地址 </summary>
        [HideInInspector] public UnrealInputTextField address;

        /// <summary> 商品ID </summary>
        [HideInInspector] public int goodsid;

        protected override void xinit()
        {
            base.xinit();
            username = textInit("username");

            tel = textInit("tel");

            //province = textInit("province");

            //city = textInit("city");

            //region = textInit("region");

            address = textInit("address");
        }

        private UnrealInputTextField textInit(string path)
        {
            UnrealInputTextField textField = this.content.Find("content").Find(path).Find("text").GetComponent<UnrealInputTextField>();
            textField.init();
            return textField;
        }

        public void textClear()
        {
            tel.text = "";
            //province.text = "";
            //city.text = "";
            //region.text = "";
            address.text = "";
            username.text = "";
        }

        protected override void xrefresh()
        {
            textClear();
        }
    }
}