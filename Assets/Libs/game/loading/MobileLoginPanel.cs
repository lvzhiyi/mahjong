using UnityEngine;

namespace cambrian.game
{
    public class MobileLoginPanel : UnrealLuaPanel
    {

        /// <summary> 用户ID </summary>
        [HideInInspector] public UnrealInputTextField userid;

        /// <summary> 用户密码 </summary>
        [HideInInspector] public UnrealInputTextField password;

        protected override void xinit()
        {
            this.userid = this.content.Find("userid").Find("text").GetComponent<UnrealInputTextField>();
            this.password = this.content.Find("password").Find("text").GetComponent<UnrealInputTextField>();
        }

        protected override void xrefresh()
        {
            this.userid.text = "";
            this.password.text = "";
        }
    }
}
