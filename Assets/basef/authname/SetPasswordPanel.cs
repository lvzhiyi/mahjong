using UnityEngine;

namespace basef.authname
{
    /// <summary> 设置密码 </summary>
    public class SetPasswordPanel : UnrealLuaPanel
    {
        /// <summary> 用户密码 </summary>
        [HideInInspector] public UnrealInputTextField password;

        /// <summary> 确认密码 </summary>
        [HideInInspector] public UnrealInputTextField check;

        protected override void xinit()
        {
            this.password = this.content.Find("password").Find("text").GetComponent<UnrealInputTextField>();
            this.check = this.content.Find("check").Find("text").GetComponent<UnrealInputTextField>();
        }

        protected override void xrefresh()
        {
            this.password.text = "";
            this.check.text = "";
        }
    }
}
