using UnityEngine;

namespace basef.authname
{
    public class IDCardPanel : UnrealLuaPanel
    {
        /// <summary> 姓名 </summary>
        [HideInInspector] public UnrealInputTextField nickName;

        /// <summary> 身份证 </summary>
        [HideInInspector] public UnrealInputTextField idcard;


        protected override void xinit()
        {
            base.xinit();
            this.nickName = this.transform.Find("Canvas").Find("content").Find("name").Find("text").GetComponent<UnrealInputTextField>();
            this.idcard = this.transform.Find("Canvas").Find("content").Find("rolenumber").Find("text").GetComponent<UnrealInputTextField>();
        }

        protected override void xrefresh()
        {
            this.nickName.text = "";
            this.idcard.text = "";
        }
    }

}