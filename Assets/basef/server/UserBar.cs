using cambrian.game;
using UnityEngine;
using UnityEngine.UI;

namespace basef.server
{
    public class UserBar : UnrealLuaSpaceObject
    {
        /// <summary>
        /// 账号
        /// </summary>
        [HideInInspector]
        public User user;

        /// <summary>
        /// 名字
        /// </summary>
        Text text;

        protected override void xinit()
        {
            base.xinit();
            this.text = this.transform.Find("text").GetComponent<Text>();
        }

        public void setUser(User user)
        {
            this.user = user;
        }
        protected override void xrefresh()
        {
            base.xrefresh();
            if (this.user == null)
                this.text.text = "该账号数据异常";
            else
                this.text.text = this.user.nickname;
        }
    }
}
