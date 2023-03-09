using UnityEngine;

namespace basef.authname
{
    /// <summary> 实名认证面板 </summary>
    public class UpdatePasswordPanel : UnrealLuaPanel
    {
 
        /// <summary> 用户密码 </summary>
        [HideInInspector] public UnrealInputTextField password;
   
        /// <summary> 确认密码 </summary>
        [HideInInspector] public UnrealInputTextField check;

        UnrealScaleButton submit;

        protected override void xinit()
        {
            this.password = this.transform.Find("Canvas").Find("content").Find("password").Find("text").GetComponent<UnrealInputTextField>();
            this.check = this.transform.Find("Canvas").Find("content").Find("check").Find("text").GetComponent<UnrealInputTextField>();
            this.submit = this.transform.Find("Canvas").Find("content").Find("submit").GetComponent<UnrealScaleButton>();
        }

        protected override void xrefresh()
        {
            this.password.text = "";
            this.check.text = "";
        }
    }
}
